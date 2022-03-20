using Digital_Classroom.Data;
using Digital_Classroom.Models;
using Digital_Classroom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Classroom.Controllers
{
    public class StudentController : Controller
    {
        private readonly ClassroomContext context;
        private readonly UserManager<AppUser> userManager;

        public StudentController(ClassroomContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        [Authorize(Roles ="Teacher")]
        public async Task<IActionResult> Add(int id)
        {
            var students = await userManager.GetUsersInRoleAsync("Student");
            ViewBag.CourseId = id;
            ViewBag.Students = new SelectList(students, "Id", "FullName");
            return View(new AddStudentViewModel());
        }

        [Authorize(Roles ="Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddExisting(int courseId, string studentId)
        {
            IList<AppUser> students;
            var student = context.StudentSubjects.
                FirstOrDefault(s => s.subjectId == courseId && s.StudentId==studentId); 
            if (student == null)
            {
                context.StudentSubjects.Add(new StudentSubject
                {
                    StudentId = studentId,
                    subjectId = courseId
                });

                context.SaveChanges();

                TempData["Message"] = "Student Added Successfully";
                students = await userManager.GetUsersInRoleAsync("Student");
                ViewBag.CourseId = courseId;
                ViewBag.Students = new SelectList(students, "Id", "FullName");
                return View("Add",new AddStudentViewModel());
            }

            TempData["Message"] = "Sorry, Student Already Exists";
            students = await userManager.GetUsersInRoleAsync("Student");
            ViewBag.CourseId = courseId;
            ViewBag.Students = new SelectList(students, "Id", "FullName");
            return View("Add", new AddStudentViewModel());

        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Add(int id, AddStudentViewModel addStVM)
        {
            IList<AppUser> students;
            if (ModelState.IsValid)
            {
                var userByEmail = await userManager.FindByEmailAsync(addStVM.Email);

                if (userByEmail == null)
                {
                    var newUser = new AppUser()
                    {
                        Email = addStVM.Email,
                        FullName = addStVM.FName + " " + addStVM.LName,
                        UserName = addStVM.FName + new Guid().ToString().Substring(0, 6),
                    };
                    var response = await userManager.CreateAsync(newUser, addStVM.Password);
                    if (response.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, "Student");
                        var nUser = await userManager.FindByEmailAsync(addStVM.Email);

                        await context.StudentSubjects.AddAsync(new StudentSubject()
                        {
                            StudentId = nUser.Id,
                            subjectId = id
                        });
                        context.SaveChanges();
                        TempData["Message2"] = "Student Added Successfully";
                        students = await userManager.GetUsersInRoleAsync("Student");
                        ViewBag.CourseId = id;
                        ViewBag.Students = new SelectList(students, "Id", "FullName");
                        return View(new AddStudentViewModel());
                    }
                }
                TempData["Message2"] = "Email Already Exists";
                students = await userManager.GetUsersInRoleAsync("Student");
                ViewBag.CourseId = id;
                ViewBag.Students = new SelectList(students, "Id", "FullName");
                return View(new AddStudentViewModel());
            }
            TempData["Message2"] = "Something Went Wrong - Please Try Again";
            students = await userManager.GetUsersInRoleAsync("Student");
            ViewBag.CourseId = id;
            ViewBag.Students = new SelectList(students, "Id", "FullName");
            return View(new AddStudentViewModel());
        }
    }
}
