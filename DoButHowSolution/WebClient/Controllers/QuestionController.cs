using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Controllers
{
    public class QuestionController : Controller
    {
        private IQuestionServices _questionService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public QuestionController(IQuestionServices service,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var all = _questionService.GetAll();
            var user = await _userManager.FindByEmailAsync("mekker_mark@yahoo.com");
            _questionService.CreateNewQuestion(new Dbh.Model.EF.Entities.Question
            {
                Title = "valami kerdes",
                Description = "kljasdjkldsajklasd jklasdjklasd"
            }, user);
            return View();
        }
    }
}
