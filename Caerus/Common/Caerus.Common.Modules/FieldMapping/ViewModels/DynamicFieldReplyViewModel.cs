using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class DynamicFieldReplyViewModel : ReplyObject
    {
       public DynamicFieldReplyViewModel()
       {
           Fields = new List<FieldItemModel>();
       }

       public List<FieldItemModel> Fields { get; set; }
       public OwningTypes OwningType { get; set; }
       public long OwningEntityRef { get; set; }
    }
  
}
