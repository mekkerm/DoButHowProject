using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.QuestionViewModels;
using MVCWebClient.Services;
using NToastNotify;
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
        private readonly IToastNotification _toaster;

        public QuestionController(IQuestionServices service,
            IAnswerServices answerService,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           MapperService mapper,
           IToastNotification toaster)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _answerService = answerService;
            _toaster = toaster;
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
            var categories = _questionService.GetQuestionCategories();
            model.QuestionCategories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");


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

        [Authorize(Policy = "RequireAtLeastModeratorRole")]
        [HttpPost]
        public IActionResult ApproveQuestion(QuestionViewModel model)
        {
            var username = this.User.Identity.Name;
            _questionService.ApproveQuestion(model.QuestionId, username);

            _toaster.AddSuccessToastMessage("Question has been approved!");

            var question = _questionService.GetQuestionById(model.QuestionId);
            var updtedModel = _mapper.Map(question);

            updtedModel.DisableInputs = !(model.CurrentUserIsTheOwner && updtedModel.IsRejected);

            ViewBag.focus = "QuestionsToApprove";

            return RedirectToAction("Index", "Questions");
        }

        [Authorize(Policy = "RequireAtLeastModeratorRole")]
        [HttpPost]
        public IActionResult RejectQuestion(QuestionViewModel model, string RejectReason)
        {
            _questionService.RejectQuestion(model.QuestionId, RejectReason, this.User.Identity.Name);

            _toaster.AddSuccessToastMessage("Question has been rejected!");

            var question = _questionService.GetQuestionById(model.QuestionId);
            var updtedModel = _mapper.Map(question);
            updtedModel.DisableInputs = !(model.CurrentUserIsTheOwner && updtedModel.IsRejected);
            
            return RedirectToAction("Index", "Questions");
        }

        [HttpPost]
        public IActionResult CorrectQuestion(QuestionViewModel model, string Title, string Description, int QuestionId)
        {
            _questionService.CorrectQuestion(QuestionId, Title, Description);

            _toaster.AddSuccessToastMessage("Your question has been corrected!");

            var question = _questionService.GetQuestionById(QuestionId);
            var updtedModel = _mapper.Map(question);

            model.DisableInputs = !(model.CurrentUserIsTheOwner && model.IsRejected);

            return RedirectToAction("Index", "Questions");
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
            var categories = _questionService.GetQuestionCategories();
            model.QuestionCategories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");

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

            try
            {
                _answerService.AnswerQuestion(questionId, Answer, currentUser);
                _toaster.AddSuccessToastMessage("Answer submitted!");
                
            }catch(ServiceException ex)
            {
                _toaster.AddErrorToastMessage("Answer was not submitted!");
                //_toaster.AddToastMessage(ex.Message, "Answer was not submitted!", Enums.ToastType.Error);
                
            }

            var quest = _questionService.GetQuestionById(questionId);
            var updtedModel = _mapper.Map(quest);
            updtedModel.DisableInputs = !(updtedModel.CurrentUserIsTheOwner && updtedModel.IsRejected);

            return View("Index", updtedModel);
        }


    }
}
