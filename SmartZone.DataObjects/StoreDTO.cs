using System;
using System.Collections.Generic;

namespace SmartZone.DataObjects
{
    public class StoreDTO : BaseDTOEntity<int>
    {
        public int SmartZoneId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int Seats { get; set; }
        public int Rooms { get; set; }
        public string? Notation { get; set; }
        public string? StreetAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? Photos { get; set; }
        public string? Wifis { get; set; }
        public int OneStarRating { get; set; } = 0;
        public int TwoStarRating { get; set; } = 0;
        public int ThreeStarRating { get; set; } = 0;
        public int FourStarRating { get; set; } = 0;
        public int FiveStarRating { get; set; } = 0;
        public DateTime? OpenTime { get; set; } = null;
        public DateTime? CloseTime { get; set; } = null;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<EmployeeDTO> EmployeeDTOs { get; set; } = Array.Empty<EmployeeDTO>();
        public virtual ICollection<FoodDTO> FoodDTOs { get; set; } = Array.Empty<FoodDTO>();
        public virtual ICollection<OrderDTO> OrderDTOs { get; set; } = Array.Empty<OrderDTO>();


    }
}
