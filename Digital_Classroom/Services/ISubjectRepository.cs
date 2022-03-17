using Digital_Classroom.Models;
using System.Collections.Generic;

namespace Digital_Classroom.Services
{
    public interface ISubjectRepository
    {
        List<Subject> GetAll();
        Subject GetById(int id);
        Subject GetByName(string name);
        int Insert(Subject subject);
        int Update(Subject subject, int id);
        int Delete(int id);
    }
}
