using System.ComponentModel.DataAnnotations;

namespace SmartZone.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
