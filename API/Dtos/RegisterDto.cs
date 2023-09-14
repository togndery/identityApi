using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterDto
    {

        public string UserName { get; set; }    
        [Required]
        [StringLength(15 ,MinimumLength =3 ,ErrorMessage ="Fist Name Should be at last 3 char")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
