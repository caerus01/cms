using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class DynamicEntityViewModel
    {
        public int OwningEntityType { get; set; }
        public dynamic EntityObject { get; set; }
        public Type EntityType { get; set; }
    }
}
