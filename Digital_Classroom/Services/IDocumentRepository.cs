using Digital_Classroom.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Digital_Classroom.Services
{
    public interface IDocumentRepository
    {
        List<Document> GetAll(int subjectId);
        Document GetById(int id);
        Task<int> Insert(Document document, IFormFile doc);
        int Delete(int id);
    }
}
