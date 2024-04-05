using System.ComponentModel.DataAnnotations;

namespace scopeindia.Models
{
    public class Contact
    {
        [Required(ErrorMessage="name is required")]
        public string Name {  get; set; }
        [Required(ErrorMessage ="email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="subject is required")]
        public string Subject {  get; set; }

        [Required(ErrorMessage ="message is required")]
        public string Message {  get; set; }
    }
}
