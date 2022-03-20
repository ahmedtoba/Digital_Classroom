using Digital_Classroom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Digital_Classroom.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Digital_Classroom.Controllers
{
    public class SubjectController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ISubjectRepository subjectRepository;

        public SubjectController(UserManager<AppUser> userManager, ISubjectRepository subjectRepository)
        {
            this.userManager = userManager;
            this.subjectRepository = subjectRepository;
        }
        public IActionResult Index()
        {
            return View(subjectRepository.GetAll());
        }

        [Authorize(Roles ="Teacher")]
        public IActionResult New()
        {
            return View(new Subject());
        }


        [Authorize(Roles ="Teacher")]
        [HttpPost]
        public async Task<IActionResult> New(IFormFile Image ,Subject subject)
        {
            subject.TeacherId = userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                var id = await subjectRepository.Insert(subject, Image);
                return Redirect($"/Subject/Details/{id}");
            }
            TempData["Error"] = "Error Creating Subject, Please Try Again";
            return View(subject);
        }

        [Authorize]
        public async Task<IActionResult> MySubjects(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (User.IsInRole("Teacher"))
                return View("Index", user.SubjectsTeaching);
            if (User.IsInRole("Student"))
            {
                var subjects = new List<Subject>();
                foreach (var studSubject in user.SubjectsStudying)
                {
                    subjects.Add(studSubject.Subject);
                }
                return View("Index", subjects);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var subject = subjectRepository.GetById(id);
            return View(subject);
        }

    }
}
