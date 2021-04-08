using System.ComponentModel.DataAnnotations;

namespace SmartZone.DataObjects
{
    public class BaseDTOEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
