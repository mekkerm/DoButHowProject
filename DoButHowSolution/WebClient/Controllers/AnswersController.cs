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
    [Authorize(Policy = "RequireAtLeastUserRole")]
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

            if (this.User.Identity.IsAuthenticated)
            {
                var username = this.User.Identity.Name;
                var model = new AllAnswersViewModel();
                var answers = _answerService.GetAnswersOfUser(username);
                foreach (var answer in answers)
                {
                    model.AnswerList.Add(_mapper.Map(answer));
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult MyRejectedAnswers()
        {
            var model = new AllAnswersViewModel();
            var username = this.User.Identity.Name;
            var asnwers = _answerService.GetRejectedQuestionsOfUser(username);

            foreach (var answer in asnwers)
            {
                var ans = _mapper.Map(answer);
                ans.QuestionTitle = _questionService.GetQuestionTitle(answer.QuestionId);
                model.AnswerList.Add(ans);

            }
            ViewBag.focus = "MyRejectedAnswers";
            return View("Index", model);
        }

        public IActionResult AnswersToApprove()
        {
            var model = new AllAnswersViewModel();
            var asnwers = _answerService.GetNotApprovedAnswers();

            foreach (var answer in asnwers)
            {
                var ans = _mapper.Map(answer);
                ans.QuestionTitle = _questionService.GetQuestionTitle(answer.QuestionId);
                model.AnswerList.Add(ans);

            }

            ViewBag.focus = "AnswersToApprove";
            return View("Index", model);
        }
    }
}
