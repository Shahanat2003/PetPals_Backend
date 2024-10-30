using System.ComponentModel.DataAnnotations;

namespace WebApplication7_petPals.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        //[MaxLength(20, ErrorMessage = "Name should not execeed 20 character")]
        public string ?UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string ?Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        //[MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string ?Password { get; set; }
        public string ?Role { get; set; }
        public bool blocked { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Wishlists> Wishlists { get; set; }
        
    }
}
