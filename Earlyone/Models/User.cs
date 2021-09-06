using System.ComponentModel.DataAnnotations;

namespace Earlyone.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FistName { get; set; }
       
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
       
        public string Role { get; set; }
    }
}
