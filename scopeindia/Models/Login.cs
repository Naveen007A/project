using System.ComponentModel.DataAnnotations;

namespace scopeindia.Models
{
    public class Login
    { 
    
        [Required(ErrorMessage="Email is required")]
            [EmailAddress]
            public string Email { get; set; }

        [Required(ErrorMessage ="password is required")]
        [DataType(DataType.Password)]
        public string Password {  get; set; }
         public bool CheckBox {  get; set; }
    }
}
