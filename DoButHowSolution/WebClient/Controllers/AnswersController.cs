using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.AnswerViewModels;
using MVCWebClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Controllers
{
    [Authorize(Policy = "RequireAtLeastModeratorRole")]
    public class AnswersController:Controller
    {
        private IQuestionServices _questionService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly MapperService _mapper;
        private IAnswerServices _answerService;

        public AnswersController(IQuestionServices service,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           MapperService mapper,
           IAnswerServices answerService)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _answerService = answerService;
        }

        public IActionResult Index()
        {
            var model = new AllAnswersViewModel();
            var asnwers = _answerService.GetNotApprovedAnswers();
            
            foreach (var answer in asnwers)
            {
                var ans = _mapper.Map(answer);
                ans.QuestionTitle = _questionService.GetQuestionTitle(answer.QuestionId);
                model.AnswerList.Add(ans);
                
            }

            ViewBag.focus = "Index";
            return View(model);
        }
    }
}
