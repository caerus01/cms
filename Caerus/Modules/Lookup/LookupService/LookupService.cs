using System;
using System.Collections.Generic;
using System.Linq;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.Client.Enums;
using Caerus.Common.Modules.Lookup.Enums;
using Caerus.Common.Modules.Lookup.Interfaces;
using Caerus.Common.Modules.Lookup.ViewModels;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Modules.Lookup.Service.Repository;

namespace Caerus.Modules.Lookup.Service
{
    public class LookupService : ILookupService
    {
        private readonly ICaerusSession _session;
        private readonly ILookupRepository _repository;
        public LookupService(ICaerusSession session, ILookupRepository repository = null)
        {
            _session = session;
            _repository = repository ?? new LookupRepository();
        }

        private List<LookupViewModel> GetEnumList<T>()
        {
            return (from dynamic item in EnumExtensions.GetMeAlistOf<T>()
                    select new LookupViewModel()
                    {
                        Key = item.AsInt(),
                        Value = item.ToEnumerationDescription()
                    }).ToList();
        }

        public LookupListViewModel GetLookupList(LookupTypes lookupType, string filter = "", int recordCount = 0, long? key = null)
        {
            var result = new LookupListViewModel() { LookupType = lookupType };
            try
            {
                //enum reservation
                if (lookupType.AsInt() < 1000)
                {
                    switch (lookupType)
                    {
                        case LookupTypes.ClientTypes:
                            {
                                result.LookupList = GetEnumList<ClientEntityTypes>();
                                break;
                            }
                    }
                }

                //db reservation
                if (lookupType.AsInt() >= 1000 && lookupType.AsInt() < 2000)
                {
                }

                //service reservation
                if (lookupType.AsInt() >= 2000 && lookupType.AsInt() < 3000)
                {
                }

                //remote reservation
                if (lookupType.AsInt() >= 3000 && lookupType.AsInt() < 4000)
                {
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;

        }
    }
}
