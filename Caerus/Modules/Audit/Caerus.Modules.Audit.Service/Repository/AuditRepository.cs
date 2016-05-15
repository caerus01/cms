using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Audit.Entities;
using Caerus.Common.Modules.Audit.Interfaces;
using Caerus.Modules.Audit.Service.Repository.Context;

namespace Caerus.Modules.Audit.Service.Repository
{
  public  class AuditRepository : IAuditRepository
    {
        private readonly CaerusContext _context;

        public AuditRepository()
        {
            _context = new CaerusContext();
        }


        public void AddRequestLog(HttpRequestLog item)
        {
            _context.HttpRequestLogs.Add(item);
            _context.SaveChanges();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
