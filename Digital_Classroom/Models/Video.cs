using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Classroom.Models
{
    public class Video
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
