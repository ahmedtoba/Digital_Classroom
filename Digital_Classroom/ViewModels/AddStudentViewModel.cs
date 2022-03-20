using Digital_Classroom.Data;
using System.ComponentModel.DataAnnotations;

namespace Digital_Classroom.ViewModels
{
    public class AddStudentViewModel
    {
        [Required(ErrorMessage ="First name is rquired")]
        [Display(Name ="First Name")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last name is rquired")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
