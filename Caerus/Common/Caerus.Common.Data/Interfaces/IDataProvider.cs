using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Caerus.Common.Enums;

namespace Caerus.Common.Data.Interfaces
{
    public interface IDataProvider
    {

        ModuleTypes ModuleId { get; }
        event EventHandler<Type> HandleTableChange;
        void ClearCacheItem(string cacheKey);
        bool HasCacheItem(string cacheKey);
        T GetCachedItem<T>(string cacheKey);
        void AddCacheItem<T>(string cacheKey, T data);
        void BulkInsert<T>(IEnumerable<T> items, string destinationTable);
        void RefreshEntry(object entry);
        string GetConnectionString();
        int SaveChanges();
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        void Dispose();
    
    }
}
