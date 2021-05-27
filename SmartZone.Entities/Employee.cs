namespace SmartZone.Entities
{
    public class Employee : User
    {
        public Store? Store { get; set; }
        public int? StoreId { get; set; }

        // public virtual ICollection<Task> Tasks { get; set; } = new HashSet<Task>();
    }
}
