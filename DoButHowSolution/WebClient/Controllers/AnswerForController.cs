using Dbh.Model.EF.Utility;
using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.AnswerViewModels;
using MVCWebClient.Models.QuestionViewModels;
using MVCWebClient.Services;
using NToastNotify;

namespace MVCWebClient.Controllers
{
    public class AnswerForController : Controller
    {
        private IQuestionServices _questionService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly MapperService _mapper;
        private IAnswerServices _answerService;
        private readonly IToastNotification _toaster;

        public AnswerForController(IQuestionServices service,
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

        public IActionResult Index(int id)
        {   
            return View(id);
        }

        [HttpGet]
        public QuestionFullModel GetQuestionWithAnswers(int questionId)
        {
            var result = new QuestionFullModel();

            var question = _questionService.GetQuestionById(questionId);
            _mapper.Map(question, result);

            string userName = null;

            if (this.User.Identity.IsAuthenticated)
            {
                userName = this.User.Identity.Name;
            }
            var answers = _answerService.GetAnswersOfQuestion(questionId, userName);
            result.AnswerList = _mapper.Map(answers);
            

            return result;
        }
        
        [HttpGet]
        public RatingInformation RateAnswer(int answerId, decimal rate)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userName = this.User.Identity.Name;

                _answerService.AddOrModifyAnswerRating(answerId, userName, rate);
                return _answerService.GetRatingInformation(answerId, userName);
            }
            return null;
        }
        
        [HttpGet]
        public RatingInformation UnRateAnswer([FromBody] int answerId)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userName = this.User.Identity.Name;

                _answerService.RemoveAnswerRating(answerId, userName);
               return _answerService.GetRatingInformation(answerId, userName);
            }

            return null;
        }

    }
}
