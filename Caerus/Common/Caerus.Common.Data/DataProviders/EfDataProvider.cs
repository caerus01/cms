using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using Caerus.Common.Data.Interfaces;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Interfaces;

namespace Caerus.Common.Data.DataProviders
{
    public abstract class EfDataProvider : DbContext, IDataProvider
    {
        public abstract ModuleTypes ModuleId
        {
            get;
        }

        public CacheItemPolicy _dataCachePolicy;
        public ObjectCache _cacheProvider;

        protected readonly ICaerusUser _currentUser;

        public event EventHandler<Type> HandleTableChange;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 5));
            base.OnModelCreating(modelBuilder);
        }

        protected EfDataProvider(bool lazyloading = true, ObjectCache provider = null)
        {
            this.Configuration.LazyLoadingEnabled = lazyloading;
            SetupCacheProvider(provider);
        }

        protected EfDataProvider(ICaerusUser currentUser, bool lazyloading = true, ObjectCache provider = null)
        {
            if (currentUser == null)
                throw new ArgumentNullException("No user specified for use this module");
            _currentUser = currentUser;
            this.Configuration.LazyLoadingEnabled = lazyloading;
            
            SetupCacheProvider(provider);
        }

        protected EfDataProvider(ICaerusUser currentUser, string connection, bool lazyloading = true, ObjectCache provider = null)
            : base(connection)
        {
            if (currentUser == null)
                throw new ArgumentNullException("No user specified for use this module");
            _currentUser = currentUser;
            this.Configuration.LazyLoadingEnabled = lazyloading;

            SetupCacheProvider(provider);
        }

        protected EfDataProvider(ICaerusUser currentUser, DbConnection connection, bool lazyloading = true, bool createsOwnConnetion = false, ObjectCache provider = null)
            : base(connection, createsOwnConnetion)
        {
            if (currentUser == null)
                throw new ArgumentNullException("No user specified for use this module");
            _currentUser = currentUser;
            this.Configuration.LazyLoadingEnabled = lazyloading;

            SetupCacheProvider(provider);
        }


        private void SetupCacheProvider(ObjectCache provider = null)
        {
            var cacheTime = new TimeSpan(0, 8, 0, 0);
            if (ConfigurationManager.AppSettings.Get("appCachePolicy") != null)
                TimeSpan.TryParse(ConfigurationManager.AppSettings.Get("appCachePolicy"), out cacheTime);
            _dataCachePolicy = new CacheItemPolicy() { SlidingExpiration = cacheTime };
            _cacheProvider = provider ?? MemoryCache.Default;
        }

        public void ClearCacheItem(string cacheKey)
        {
            if (HasCacheItem(cacheKey))
                _cacheProvider.Remove(cacheKey);
        }

        public bool HasCacheItem(string cacheKey)
        {
            if (this._cacheProvider.Contains(cacheKey))
                return true;
            return false;
        }

        public T GetCachedItem<T>(string cacheKey)
        {
            return (T)_cacheProvider.Get(cacheKey);
        }

        public void AddCacheItem<T>(string cacheKey, T data)
        {
            _cacheProvider.Add(cacheKey, data, _dataCachePolicy);
        }


        public void BulkInsert<T>(IEnumerable<T> items, string destinationTable)
        {
            AddBulkEntities(items, (SqlConnection)this.Database.Connection, destinationTable, "");
        }

        private void AddBulkEntities<T>(IEnumerable<T> items, SqlConnection conn, string destinationTable, string defaultNameSpace)
        {
            try
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                using (var sqlBc = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, trans))
                {
                    sqlBc.DestinationTableName = destinationTable;
                    var dt = ConvertToDataTable(items, defaultNameSpace);
                    dt.TableName = destinationTable;
                    sqlBc.ColumnMappings.Clear();
                    foreach (DataColumn col in dt.Columns)
                    {
                        sqlBc.ColumnMappings.Add(new SqlBulkCopyColumnMapping(col.ColumnName, col.ColumnName));
                    }
                    sqlBc.WriteToServer(dt);
                }
                trans.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert entities. Please review inner exception", ex);
            }
        }

        private static DataTable ConvertToDataTable<T>(IEnumerable<T> data, string dataModelNameSpace)
        {
            var properties =
               TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.PropertyType.FullName != null && (string.IsNullOrEmpty(dataModelNameSpace) || !prop.PropertyType.FullName.Contains(dataModelNameSpace)))
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.PropertyType.FullName != null && (string.IsNullOrEmpty(dataModelNameSpace) || !prop.PropertyType.FullName.Contains(dataModelNameSpace)))
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                }
                table.Rows.Add(row);
            }
            return table;

        }

        public void RefreshEntry(object entry)
        {
            this.Entry(entry).Reload();
        }

        public string GetConnectionString()
        {
            return this.Database.Connection.ConnectionString;
        }

        private string GetTableName(string tableNameSpace)
        {
            string result = "";
            result = tableNameSpace.Substring(tableNameSpace.LastIndexOf('.') + 1, tableNameSpace.Length - (tableNameSpace.LastIndexOf('.') + 1));
            return result;
        }

        private void WriteModified(object myObject)
        {
            if (ContainsProperty<DateTime>("DateModified", myObject))
                myObject.GetType().GetProperty("DateModified").SetValue(myObject, DateTime.Now, null);
            if (_currentUser != null)
                if (ContainsProperty<DateTime>("UserModified", myObject) && _currentUser != null)
                    myObject.GetType().GetProperty("UserModified").SetValue(myObject, _currentUser.RefId, null);
        }

        private void WriteCreated(object myObject)
        {
            if (ContainsProperty<DateTime>("DateCreated", myObject))
                myObject.GetType().GetProperty("DateCreated").SetValue(myObject, DateTime.Now, null);
            if (_currentUser != null)
                if (ContainsProperty<DateTime>("UserCreated", myObject) && _currentUser != null)
                    myObject.GetType().GetProperty("UserCreated").SetValue(myObject, _currentUser.RefId, null);
        }

        private void WriteId(object myObject)
        {
            if (ContainsProperty<Guid>("Id", myObject))
                myObject.GetType().GetProperty("Id").SetValue(myObject, Guid.NewGuid(), null);
        }

        private bool ContainsProperty<T>(String propertyName, object myObject)
        {
            var result = myObject.GetType().GetProperties().Any(info => info.Name == propertyName && info.PropertyType == typeof(T));
            if (!result)
            {
                var type = myObject.GetType().GetProperties().FirstOrDefault(info => info.Name == propertyName);
                if (type != null)
                {
                    var underlyingtype = Nullable.GetUnderlyingType(type.PropertyType);
                    if (underlyingtype == typeof(T))
                        return true;
                }
            }


            return result;
        }
        public override int SaveChanges()
        {
            var changes = this.ChangeTracker.Entries().Where(c => c.State == EntityState.Modified || c.State == EntityState.Added || c.State == EntityState.Deleted).ToList();
            var updates = changes.Where(c => c.State == EntityState.Modified).ToList();
            foreach (var item in updates)
            {
                try
                {
                    WriteModified(item.Entity);
                }
                catch (Exception ex)
                {
                    Debug.Assert(true, ex.Message);
                    break; //End audit attempt
                }
            }
            var inserts = changes.Where(c => c.State == EntityState.Added).ToList();
            foreach (var item in inserts)
            {
                try
                {
                    WriteId(item.Entity);
                    WriteCreated(item.Entity);
                    WriteModified(item.Entity);
                }
                catch (Exception ex)
                {
                    Debug.Assert(true, ex.Message);
                    break; //End audit attempt
                }
            }
            var deletes = changes.Where(c => c.State == EntityState.Deleted).ToList();
            foreach (var item in deletes)
            {
                try
                {
                    //string tablename = GetTableName(item.EntitySet.ElementType.ToString());
                }
                catch (Exception ex)
                {
                    Debug.Assert(true, ex.Message);
                    break; //End audit attempt
                }
            }
            var result = base.SaveChanges();
            if (HandleTableChange != null)
            {
                if (changes.Any())
                {
                    var items = changes.Select(c => c.Entity).Distinct();
                    foreach (var item in items)
                    {
                        HandleTableChange(this, item.GetType());
                    }
                }
            }
            return result;
        }


        public string GetKeyByCustom(string item, List<object> items)
        {
            var result = string.Format("cache_{0}_{1}", ModuleId.AsInt(), item);
            if (items != null)
                return items.Aggregate(result, (current, val) => current + string.Format("_{0}", val.ToString()));
            return result;
        }
    }
}
