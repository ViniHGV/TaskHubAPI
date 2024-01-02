using System;

namespace TaskHubAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; }
    }
}