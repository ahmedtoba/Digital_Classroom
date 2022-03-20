using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Classroom.Models
{
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        [Required]
        [Display(Name="Subject Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual AppUser Teacher { get; set; }
        public virtual List<StudentSubject> Students { get; set; }
        public virtual List<Document> Documents { get; set; }
        public virtual List<Video> Videos { get; set; }

    }
}
