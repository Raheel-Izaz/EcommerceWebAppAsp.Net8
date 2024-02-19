using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApp.Models
{
    public class Admin
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
