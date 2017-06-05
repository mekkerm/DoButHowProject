using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dbh.ServiceLayer.Contracts;
using MVCWebClient.Services;
using MVCWebClient.Models.QuestionViewModels;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {

        private IQuestionServices _questionService;
        private readonly MapperService _mapper;
        private IAnswerServices _answerService;

        public HomeController(IQuestionServices service,
           MapperService mapper,
           IAnswerServices answerService)
        {
            _questionService = service;
            _mapper = mapper;
            _answerService = answerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Name = "GetInitialAnswers")]
        public IActionResult GetInitialAnswers(int skip, int take)
        {
            var answeredQuestions = _questionService.GetAnsweredQuestions(skip, take);
            var results = new List<QuestionViewModel>();
            foreach(var a in answeredQuestions)
            {
                results.Add(_mapper.Map(a));
            }
            return Json(results);
        }

        
        [HttpGet()]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
