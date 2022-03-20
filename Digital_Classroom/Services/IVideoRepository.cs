using Digital_Classroom.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Digital_Classroom.Services
{
    public interface IVideoRepository
    {
        List<Video> GetAll(int subjectId);
        Video GetById (int id);
        Task<int> Insert(Video video, IFormFile content);
        int Delete(int id);

    }
}
