using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Classroom.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public virtual List<Subject> SubjectsTeaching { get; set; }
        public virtual List<StudentSubject> SubjectsStudying { get; set; }
    }
}
