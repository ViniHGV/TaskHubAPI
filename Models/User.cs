using System;

namespace TaskHubAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreateAccount { get; set; } = DateTime.Now;
    }
}