using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Configuration.Entities;
using Caerus.Common.Modules.Configuration.Interfaces;
using Caerus.Modules.Configuration.Service.Repository.Context;

namespace Caerus.Modules.Configuration.Service.Repository
{


    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly CaerusContext _context;
        private const string _settingsKey = "ModuleSettings";
        private const string _configKey = "";
        public ConfigurationRepository()
        {
            _context = new CaerusContext();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public bool SetModuleSetting(ModuleTypes moduleId, int settingId, string value, string userId)
        {
            _context.ClearCacheItem(_context.GetKeyByCustom(_settingsKey, null));
            if (value == null)
                value = "";
            return _context.SetModuleSetting(moduleId, settingId, value, userId);
        }

        public string GetModuleSetting(ModuleTypes moduleId, int settingId)
        {
            var key = _context.GetKeyByCustom(_settingsKey, null);
            if (_context.HasCacheItemByType<List<ModuleSetting>>(key))
            {
                var setting = _context.GetCachedItem<List<ModuleSetting>>(key).FirstOrDefault(c => c.ModuleId == (int)moduleId && c.SettingId == settingId);
                if (setting != null)
                    return setting.SettingValue;
                return "";
            }
            else
            {
                var list = _context.ModuleSettings.ToList();
                _context.AddCacheItem(key, list);
                var setting = list.FirstOrDefault(c => c.ModuleId == (int)moduleId && c.SettingId == settingId);
                if (setting != null)
                    return setting.SettingValue;
                return "";
            }
        }

        public void AddModuleConfiguration(ModuleConfiguration item)
        {
            _context.ClearCacheItem(_context.GetKeyByCustom(_configKey, null));
            _context.ModuleConfigurations.Add(item);
        }
        public int GetServiceModuleInUse(ModuleTypes moduleType)
        {
            var key = _context.GetKeyByCustom(_configKey, null);
            if (_context.HasCacheItemByType<List<ModuleConfiguration>>(key))
            {
                var setting = _context.GetCachedItem<List<ModuleConfiguration>>(key).FirstOrDefault(c => c.ModuleId == (int)moduleType);
                if (setting != null)
                    return setting.ServiceTypeId;
                return 0;
            }
            else
            {
                var list = _context.ModuleConfigurations.ToList();
                _context.AddCacheItem(key, list);
                var setting = list.FirstOrDefault(c => c.ModuleId == (int)moduleType);
                if (setting != null)
                    return setting.ServiceTypeId;
                return 0;
            }
        }
    }
}
