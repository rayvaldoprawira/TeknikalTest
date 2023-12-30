using TeknikalTest.Models;

namespace TeknikalTest.DTOs.Vendors
{
    public class VendorDtoGet
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoProfile { get; set; }
        public string Sector { get; set; }
        public string Type { get; set; }
        public bool IsAdminApprove { get; set; }
        public bool IsManagerApprove { get; set; }

        public static implicit operator Vendor(VendorDtoGet vendorDtoGet)
        {
            return new()
            {
                Guid = vendorDtoGet.Guid,
                Name = vendorDtoGet.Name,
                Email = vendorDtoGet.Email,
                PhoneNumber = vendorDtoGet.PhoneNumber,
                PhotoProfile = vendorDtoGet.PhotoProfile,
                Sector = vendorDtoGet.Sector,
                Type = vendorDtoGet.Type,

            };
        }

        public static explicit operator VendorDtoGet(Vendor vendor)
        {
            return new()
            {
                Guid = vendor.Guid,
                Name = vendor.Name,
                Email = vendor.Email,
                PhoneNumber = vendor.PhoneNumber,
                PhotoProfile = vendor.PhotoProfile,
                Sector = vendor.Sector,
                Type = vendor.Type,
                
            };
        }
    }
}
