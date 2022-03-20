using Digital_Classroom.Data;
using Digital_Classroom.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Classroom.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ClassroomContext context;

        public DocumentRepository(ClassroomContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Document> GetAll(int subjectId)
        {
            return context.Documents.Where(v => v.SubjectId == subjectId).ToList();
        }

        public Document GetById(int id)
        {
            return context.Documents.FirstOrDefault(v => v.Id == id);
        }

        public async Task<int> Insert(Document document, IFormFile content)
        {
            if (content != null)
            {
                string vidExt = Path.GetExtension(content.FileName);
                if (vidExt.ToLower() == ".pdf")
                {
                    using (var stream = new MemoryStream())
                    {
                        await content.CopyToAsync(stream);
                        document.Content = stream.ToArray();
                        document.ContentType = content.ContentType;
                        document.Title += vidExt;
                    }
                }
            }
            var newId = context.Documents.Count() == 0 ? 1 : context.Documents.Count() + 1;
            document.Id = newId;
            context.Documents.Add(document);
            return context.SaveChanges();

        }
    }
}
