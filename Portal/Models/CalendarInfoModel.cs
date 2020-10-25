using System;
using System.ComponentModel.DataAnnotations;
namespace Portal.Models
{
    public class CalendarInfo
    {
        [Key]
        public Int64 Id { get; set; }
        public string Event { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
