using Digital_Classroom.Models;
using Digital_Classroom.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Digital_Classroom.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository documentRepo;

        public DocumentController(IDocumentRepository documentRepo)
        {
            this.documentRepo = documentRepo;
        }
        public IActionResult New(int id)
        {
            ViewBag.CourseId = id;
            return View(new Document());
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> New(int courseId, IFormFile content, Document document)
        {
            if (ModelState.IsValid)
            {
                document.SubjectId = courseId;
                await documentRepo.Insert(document, content);
                return Redirect($"/Subject/Details/{courseId}");
            }

            ViewBag.CourseId = courseId;
            return View(document);
        }


        [HttpPost]
        public FileResult Download(int id)
        {
            Document file = documentRepo.GetById(id);
            return File(file.Content, file.ContentType, file.Title);
        }
    }
}
