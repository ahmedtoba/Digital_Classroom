using Digital_Classroom.Data;
using Digital_Classroom.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Classroom.Services
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ClassroomContext context;
        private readonly IWebHostEnvironment webhost;

        public SubjectRepository(ClassroomContext context, IWebHostEnvironment _webhost)
        {
            this.context = context;
            webhost = _webhost;
        }
        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Subject> GetAll()
        {
            return context.Subjects.ToList();
        }

        public Subject GetById(int id)
        {
            return context.Subjects.SingleOrDefault(s => s.Id == id);
        }

        public Subject GetByName(string name)
        {
            return context.Subjects.SingleOrDefault(s => s.Name == name);
        }

        public async Task<int> Insert(Subject subject, IFormFile Image)
        {
            if (Image != null)
            {
                var extensions = new List<string>() { ".png", ".jpg", ".jpeg", ".svg" };
                string imgExt = Path.GetExtension(Image.FileName);
                if (extensions.Contains(imgExt.ToLower()))
                {
                    using (var stream = new MemoryStream())
                    {
                        await Image.CopyToAsync(stream);
                        subject.Image = stream.ToArray();
                    }
                }
            }

            var newId = context.Subjects.Count() == 0 ? 1 : context.Subjects.Max(x => x.Id)+1;
            subject.Id = newId;
            context.Subjects.Add(subject);
            context.SaveChanges();
            return newId;
        }

        public int Update(Subject newSubject, int id)
        {
            var oldSubject = GetById(id);
            if (oldSubject == null)
            {
                oldSubject.Name = newSubject.Name;
                oldSubject.Description = newSubject.Description;
            }
            return context.SaveChanges();
        }

    }
}
