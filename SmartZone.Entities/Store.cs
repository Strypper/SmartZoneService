using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartZone.Entities
{
    public class Store : BaseEntity
    {
        public SmartZone SmartZone { get; set; }
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
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public bool IsDeleted { get; set; } = false;

        // A store may have many employees
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public virtual ICollection<Food> Foods { get; set; } = new HashSet<Food>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
