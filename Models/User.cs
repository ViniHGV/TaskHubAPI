using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskHubAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmailAddressAttribute Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreateAccount { get; set; } = DateTime.Now;
        public ICollection<Task> Tasks { get; set; }
    }
}