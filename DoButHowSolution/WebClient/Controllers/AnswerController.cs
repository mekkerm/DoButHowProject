using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.AnswerViewModels;
using MVCWebClient.Services;
using NToastNotify;

namespace MVCWebClient.Controllers
{
    public class AnswerController:Controller
    {
        private IQuestionServices _questionService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly MapperService _mapper;
        private IAnswerServices _answerService;
        private readonly IToastNotification _toaster;

        public AnswerController(IQuestionServices service,
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

        [Authorize(Policy = "RequireAtLeastUserRole")]
        public IActionResult Index(int id)
        {
            var model = GetAnswerQuestion(id);

            return View(model);
        }

        [Authorize(Policy = "RequireAtLeastModeratorRole")]
        [HttpPost]
        public IActionResult ApproveAnswer(int AnswerId)
        {
            var username = this.User.Identity.Name;

            _answerService.ApproveAnswer(AnswerId, username);
            _toaster.AddSuccessToastMessage("The answer has been approved!");
            

            var model = GetAnswerQuestion(AnswerId);
            //IAuthorizationService serv = null;
            
            //serv.AuthorizeAsync(null, "RequireAtLeastUserRole").Result.Succeeded
            
            return RedirectToAction("Index", "Answers");
        }

        [Authorize(Policy = "RequireAtLeastModeratorRole")]
        [HttpPost]
        public IActionResult RejectAnswer(int AnswerId, string RejectReason)
        {
            var username = this.User.Identity.Name;

            _answerService.RejectAnswer(AnswerId, RejectReason, username);

            _toaster.AddSuccessToastMessage("The answer has been rejected!");

            var model = GetAnswerQuestion(AnswerId);

            return RedirectToAction("Index", "Answers");
        }

        private AnswerQuestionViewModel GetAnswerQuestion(int id)
        {
            var model = new AnswerQuestionViewModel();

            var answer = _answerService.GetAnswerById(id);

            var question = _questionService.GetQuestionById(answer.QuestionId);

            model.Answer = _mapper.Map(answer);
            model.Question = _mapper.Map(question);

            var categories = _questionService.GetQuestionCategories();
            model.Question.QuestionCategories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");


            if (this.User.Identity.IsAuthenticated)
            {
                var username = this.User.Identity.Name;
                model.Answer.CurrentUserIsTheOwner = (username == model.Answer.CreatorName);
            }
            else
            {
                model.Answer.CurrentUserIsTheOwner = false;
            }


            model.DisableInputs = !(model.Answer.CurrentUserIsTheOwner && model.Answer.IsRejected);

            ViewBag.focus = "Index";
            return model;
        }

        [HttpPost]
        public IActionResult CorrectAnswer(int AnswerId, string Response)
        {
            _answerService.CorrectAnswer(AnswerId, Response);

            _toaster.AddSuccessToastMessage("Your answer has been corrected!");

            var answer = _answerService.GetAnswerById(AnswerId);
            var updtedModel = _mapper.Map(answer);

            updtedModel.DisableInputs = !(updtedModel.CurrentUserIsTheOwner && updtedModel.IsRejected);

            return RedirectToAction("Index", "Answers");
        }
    }
}
