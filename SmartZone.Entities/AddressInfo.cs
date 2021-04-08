using System;

namespace SmartZone.Entities
{
    public class AddressInfo : BaseEntity
    {
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int Seats { get; set; }
        public int Rooms { get; set; }
        public string? StreetAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? Photos { get; set; }
        public string? Wifis { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
    }
}
