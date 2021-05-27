

namespace SmartZone.DataObjects
{
    public class UserDTO : BaseDTOEntity
    {
        public string Guid { get; set; }
        public string UserName { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
        public string? Policies { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
