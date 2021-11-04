using E_Tickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_Tickets.Models.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Identity;
using E_Tickets.Models.ModelsDB;
using Microsoft.AspNetCore.Authorization;

namespace E_Tickets.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_userManager.Users.ToList());
        }


        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ConformidContact(EmailViewModel emailContent)
        {

            ViewBag.result = SendEmail.SendAnEmail(emailContent);

            return View("Contact");
            
        }




    }
}
