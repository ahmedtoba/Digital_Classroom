using Digital_Classroom.Models;
using System.Collections.Generic;

namespace Digital_Classroom.Services
{
    public interface IDocumentRepository
    {
        List<Document> GetAll(int subjectId);
        Document GetById(int id);
        int Insert(Document document);
        int Delete(int id);
    }
}
