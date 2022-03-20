using Digital_Classroom.Models;
using Digital_Classroom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Classroom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISubjectRepository subjectRepository;

        public HomeController(ILogger<HomeController> logger, ISubjectRepository subjectRepository)
        {
            _logger = logger;
            this.subjectRepository = subjectRepository;
        }

        
        public IActionResult Index()
        {
            return View(subjectRepository.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
