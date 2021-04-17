

namespace SmartZone.DataObjects
{
    class SmartZoneUserDTO
    {
        public string Guid { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? Policies { get; set; }
        public bool IsDeleted { get; set; }
    }
}
