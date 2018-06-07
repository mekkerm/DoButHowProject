using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.AnswerViewModels;
using MVCWebClient.Models.SearchViewModels;
using MVCWebClient.Services;
using NToastNotify;
using System.Diagnostics;
using System.Linq;

namespace MVCWebClient.Controllers
{
    public class SearchController : Controller
    {
        private IQuestionServices _questionService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly MapperService _mapper;
        private IAnswerServices _answerService;
        private readonly IToastNotification _toaster;

        public SearchController(IQuestionServices service,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           MapperService mapper,
           IAnswerServices answerService,
           IToastNotification toaster)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _answerService = answerService;
            _toaster = toaster;
        }
        public IActionResult Index(string search)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            var questions = _questionService.FindQuestions(search);
            
            sw.Stop();
            var vm = new SearchResultsViewModel();
            vm.QueryDuration = sw.Elapsed.TotalMilliseconds;/*TODO minimize data between the client and the server!*/
            vm.Hits = questions.ToList();
            return View(vm);
        }
    }
}