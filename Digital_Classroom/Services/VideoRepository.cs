using Digital_Classroom.Data;
using Digital_Classroom.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Classroom.Services
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ClassroomContext context;

        public VideoRepository(ClassroomContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Video> GetAll(int subjectId)
        {
            return context.Videos.Where(v => v.SubjectId == subjectId).ToList();
        }

        public Video GetById(int id)
        {
            return context.Videos.FirstOrDefault(v => v.Id == id);
        }

        public async Task<int> Insert(Video video, IFormFile content)
        {
            if (content != null)
            {
                var extensions = new List<string>() { ".mp4", ".avi", ".wmv", ".mkv", ".flv" };
                string vidExt = Path.GetExtension(content.FileName);
                if (extensions.Contains(vidExt.ToLower()))
                {
                    using (var stream = new MemoryStream())
                    {
                        await content.CopyToAsync(stream);
                        video.Content = stream.ToArray();
                        video.ContentType = content.ContentType;
                        video.Title += vidExt;
                    }
                }
            }
            var newId = context.Videos.Count() == 0 ? 1 : context.Videos.Count() + 1;
            video.Id = newId;
            context.Videos.Add(video);
            return context.SaveChanges();
            
        }
    }
}
