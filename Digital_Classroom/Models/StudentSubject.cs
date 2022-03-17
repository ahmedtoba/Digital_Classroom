using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Classroom.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        [ForeignKey("Subject")]
        public int subjectId { get; set; }
        public virtual AppUser Student { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
