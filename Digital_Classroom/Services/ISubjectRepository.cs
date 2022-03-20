using Digital_Classroom.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Digital_Classroom.Services
{
    public interface ISubjectRepository
    {
        List<Subject> GetAll();
        Subject GetById(int id);
        Subject GetByName(string name);
        Task<int> Insert(Subject subject, IFormFile Image);
        int Update(Subject subject, int id);
        int Delete(int id);
    }
}
