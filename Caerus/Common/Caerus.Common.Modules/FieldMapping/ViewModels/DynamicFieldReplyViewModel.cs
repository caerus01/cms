using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
       public int OwningEntityType { get; set; }
       public long OwningEntityRef { get; set; }
    }
  
}
