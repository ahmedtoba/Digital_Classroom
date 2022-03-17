using Digital_Classroom.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Digital_Classroom.Services
{
    public interface IVideoRepository
    {
        List<Video> GetAll(int subjectId);
        Video GetById (int id);
        int Insert(Video video);
        int Delete(int id);

    }
}
