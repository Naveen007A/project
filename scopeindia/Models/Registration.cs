namespace scopeindia.Models;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class Registration
{
    [Required(ErrorMessage = "first name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "choose the gender")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "date of birth required")]
    [Display(Name = " Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "please enter the Email")]
    [Display(Name = "Email Address")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "please enter the mobile number")]
    [Phone]
    public string Phone { get; set; }



    [Required(ErrorMessage ="country is required")]
    public string Country {  get; set; }

    [Required(ErrorMessage ="state is required")]
    public string State { get; set; }

    [Required(ErrorMessage ="city is required")]
    public string City {  get; set; }

    [Required(ErrorMessage ="hobbies is required")]
    public string Hobbies {  get; set; }

    [Required(ErrorMessage ="avathar is required")]

    public IFormFile myfile {  get; set; }





    }

