using System.ComponentModel.DataAnnotations;

namespace SmartZone.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
