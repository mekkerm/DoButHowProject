using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.QuestionViewModels;
using MVCWebClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Controllers
{
    public class QuestionController : Controller
    {
        private IQuestionServices _questionService;
        private IAnswerServices _answerService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly MapperService _mapper;

        public QuestionController(IQuestionServices service,
            IAnswerServices answerService,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           MapperService mapper)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            
            var question = _questionService.GetQuestionById(id);
            if (question == null)
            {

                return RedirectToAction("Index", "Home");
            }
            var model = _mapper.Map(question);

            
            if (this.User.Identity.IsAuthenticated)
            {
                var username = this.User.Identity.Name;
                model.CurrentUserIsTheOwner = (username == model.CreatorName);
            }
            else
            {
                model.CurrentUserIsTheOwner = false;
            }

            model.DisableInputs = !(model.CurrentUserIsTheOwner && model.IsRejected);
            return View(model);
        }

        [HttpPost]
        public IActionResult ApproveQuestion(QuestionViewModel model)
        {
            var username = this.User.Identity.Name;
            _questionService.ApproveQuestion(model.QuestionId, username);

            ViewBag.message = "Question has been approved!";

            var question = _questionService.GetQuestionById(model.QuestionId);
            var updtedModel = _mapper.Map(question);

            updtedModel.DisableInputs = !(model.CurrentUserIsTheOwner && updtedModel.IsRejected);
            return View("Index", updtedModel);
        }

        [HttpPost]
        public IActionResult RejectQuestion(QuestionViewModel model, string RejectReason)
        {
            _questionService.RejectQuestion(model.QuestionId, RejectReason, this.User.Identity.Name);

            ViewBag.message = "Question has been rejected!";

            var question = _questionService.GetQuestionById(model.QuestionId);
            var updtedModel = _mapper.Map(question);
            updtedModel.DisableInputs = !(model.CurrentUserIsTheOwner && updtedModel.IsRejected);
            return View("Index", updtedModel);
        }

        [HttpPost]
        public IActionResult CorrectQuestion(QuestionViewModel model, string Title, string Description, int QuestionId)
        {
            _questionService.CorrectQuestion(QuestionId, Title, Description);

            ViewBag.message = "Question has been rejected!";

            var question = _questionService.GetQuestionById(QuestionId);
            var updtedModel = _mapper.Map(question);

            model.DisableInputs = !(model.CurrentUserIsTheOwner && model.IsRejected);

            return View("Index", updtedModel);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAtLeastUserRole")]
        public IActionResult AnswerQuestion(int id)
        {
            var question = _questionService.GetQuestionById(id);
            if (question == null)
            {

                return RedirectToAction("Index", "Home");
            }
            var model = _mapper.Map(question);


            if (this.User.Identity.IsAuthenticated)
            {
                var username = this.User.Identity.Name;
                model.CurrentUserIsTheOwner = (username == model.CreatorName);
            }
            else
            {
                model.CurrentUserIsTheOwner = false;
            }

            model.DisableInputs = !(model.CurrentUserIsTheOwner && model.IsRejected);
            return View(model);
        }


        [HttpPost]
        [Authorize(Policy = "RequireAtLeastUserRole")]
        public IActionResult AnswerTheQuestion(string Answer, QuestionViewModel question)
        {
            var questionId = question.QuestionId;
            var currentUser = this.User.Identity.Name;

            _answerService.AnswerQuestion(questionId, Answer, currentUser);

            return View();
        }


    }
}
