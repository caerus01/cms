using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Client.ViewModels
{
   public class AddressViewModel : ReplyObject
   {
       public long ClientRefId { get; set; }
       public string AddressLine { get; set; }
       public string Suburb { get; set; }
       public string City { get; set; }
       public long Country { get; set; }
       public string Zip { get; set; }
       public string Longitude { get; set; }
       public string Latitude { get; set; }
       public long ProvinceId { get; set; }
       public string ProvinceName { get; set; }
       public bool IsPrimary { get; set; }
    }
}
