using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace TaskHubAPI.Models
{
    public class Project
    {
        [Key]
        public int IdProject { get; set; }
        public string Title { get; set; }
        public DateTime DateOfCriation { get; set; } = DateTime.Now;
        public int IdUser { get; set; }
        public List<User> Users { get; set; }
        public List<Task> Tasks { get; set; }
    }
}