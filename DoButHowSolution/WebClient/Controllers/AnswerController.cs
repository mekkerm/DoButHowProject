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

            _toaster.AddToastMessage("The answer has been approved!", "", ToastEnums.ToastType.Success);

            var model = GetAnswerQuestion(AnswerId);

            return RedirectToAction("Index", "Answers");
        }

        [Authorize(Policy = "RequireAtLeastModeratorRole")]
        [HttpPost]
        public IActionResult RejectAnswer(int AnswerId, string RejectReason)
        {
            var username = this.User.Identity.Name;

            _answerService.RejectAnswer(AnswerId, RejectReason, username);

            _toaster.AddToastMessage("The answer has been rejected!", "", ToastEnums.ToastType.Success);

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

            ViewBag.focus = "Index";
            return model;
        }
    }
}
