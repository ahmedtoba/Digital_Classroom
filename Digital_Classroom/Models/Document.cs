
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Classroom.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
