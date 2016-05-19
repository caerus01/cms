using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class DynamicResponseDataModel
    {
        public DynamicResponseDataModel()
        {
            Fields = new Dictionary<string, object>();
        }
        public int OwningEntityType { get; set; }


        public Dictionary<string, dynamic> Fields { get; set; } 
    }
}
