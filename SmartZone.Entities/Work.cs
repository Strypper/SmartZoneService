using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartZone.Entities
{
    public class Work : BaseEntity
    {
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime Deadline { get; set; }
        public WorkStatus TaskStatus { get; set; } = WorkStatus.Processing;
    }

    public enum WorkStatus
    {
        Processing, Done, Canceled
    }
}
