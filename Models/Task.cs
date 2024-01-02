using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHubAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}