using System.ComponentModel.DataAnnotations;

namespace scopeindia.Models
{
    public class Firsttimelogin
    {
     [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="otp is required")]
        public string Otp {  get; set; }

        [Required(ErrorMessage ="password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
