using System;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Common.Modules.Configuration.Interfaces;
using Caerus.Common.Modules.Configuration.ViewModels;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.ViewModels;
using Caerus.Modules.Configuration.Service.Repository;

namespace Caerus.Modules.Configuration.Service
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ICaerusSession _session;
        private readonly IConfigurationRepository _repository;
        public ConfigurationService(ICaerusSession session, IConfigurationRepository repository = null)
        {
            _session = session;
            _repository = repository ?? new ConfigurationRepository();
        }

        public bool IsModuleInUse(ModuleTypes module, int type)
        {
            return _repository.GetServiceModuleInUse(module) == type;
        }

        public bool SetModuleSetting(ModuleTypes moduleId, int settingId, string value)
        {
            return _repository.SetModuleSetting(moduleId, settingId, value, _session.CurrentUserRef);
        }

        public T GetModuleSetting<T>(ModuleTypes type, int settingId)
        {
            var t = typeof(T);
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Boolean:
                    {
                        var settingValue = GetModuleSetting(type, settingId).ToLower();
                        return (T)(object)(settingValue == "true" || settingValue == "1");
                    }
                case TypeCode.DateTime:
                    {
                        var settingValue = GetModuleSetting(type, settingId);
                        var settingAsDate = settingValue.AsDate();
                        return (T)(object)settingAsDate;
                    }
                case TypeCode.Decimal:
                    {
                        var settingValue = GetModuleSetting(type, settingId);
                        var settingAsDecimal = settingValue.AsDecimal();
                        return (T)(object)settingAsDecimal;
                    }
                case TypeCode.Int16:
                case TypeCode.Int32:
                    {
                        var settingValue = GetModuleSetting(type, settingId);
                        var settingAsInt = settingValue.AsInt();
                        return (T)(object)settingAsInt;
                    }
                case TypeCode.Int64:
                    {
                        var settingValue = GetModuleSetting(type, settingId);
                        var settingAsInt = settingValue.AsLong();
                        return (T)(object)settingAsInt;
                    }
            }
            return (T)(object)null;
        }
    
        public ReplyObject SaveModuleSetting(ModuleSettingViewModel model)
        {
            var result = new ReplyObject();
            try
            {
                if (!string.IsNullOrEmpty(model.SettingValue))
                    if (model.SettingValue.ToUpper() == "TRUE" || model.SettingValue.ToUpper() == "FALSE")
                    {
                        model.SettingValue = model.SettingBoolValue ? "TRUE" : "FALSE";
                    }

                var saveResult = _repository.SetModuleSetting((ModuleTypes)model.ModuleId, model.SettingId, model.SettingValue, _session.CurrentUserRef);

                if (!saveResult)
                {
                    result.ReplyMessage = "Fail to save setting";
                    result.ReplyStatus = ReplyStatus.Error;
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        private string GetModuleSetting(ModuleTypes type, int settingId)
        {
            return _repository.GetModuleSetting(type, settingId).Trim();
        }

    }
}
