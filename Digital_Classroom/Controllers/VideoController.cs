using Digital_Classroom.Models;
using Digital_Classroom.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Digital_Classroom.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoRepository videoRepo;

        public VideoController(IVideoRepository videoRepo)
        {
            this.videoRepo = videoRepo;
        }
        public IActionResult New(int id)
        {
            ViewBag.CourseId = id;
            return View(new Video());
        }

        [Authorize(Roles ="Teacher")]
        [HttpPost]
        public async Task<IActionResult> New(int courseId, IFormFile content, Video video)
        {
            if (ModelState.IsValid)
            {
                video.SubjectId = courseId;
                await videoRepo.Insert(video, content);
                return Redirect($"/Subject/Details/{courseId}");
            }
            
            ViewBag.CourseId = courseId;
            return View(video);
        }


        [HttpPost]
        public FileResult Download(int id)
        {
            Video file = videoRepo.GetById(id);
            return File(file.Content, file.ContentType, file.Title);
        }
    }
}
