using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MVCWebClient.Models.QuestionViewModels;
using MVCWebClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;
using Dbh.Model.EF.Entities;

namespace MVCWebClient.Controllers
{
    public class QuestionsController : Controller
    {
        private IQuestionServices _questionService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly MapperService _mapper;
        private readonly IToastNotification _toaster;
        

        public QuestionsController(IQuestionServices service,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           MapperService mapper,
           IToastNotification toaster)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _toaster = toaster;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var model = new AllQuestionsViewModel();

            //var questions = _questionService.GetApprovedQuestions(Take, Skip);
            //foreach (var question in questions)
            //{
            //    model.Questions.Add(_mapper.Map(question));
            //}

            ViewBag.focus = "Index";
            return View(/*model*/);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAtLeastUserRole")]
        public IActionResult AskQuestion()
        {
            if (!this.User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Index", "Home");
            }
            var model = new QuestionViewModel();
            model.Title = "How To";


            var categories = _questionService.GetQuestionCategories();
            model.QuestionCategories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAtLeastUserRole")]
        public IActionResult AskQuestion(QuestionViewModel model)
        {

            if (this.User.Identity.IsAuthenticated)
            {
                var creatorName = this.User.Identity.Name;
                var result = _questionService.CreateNewQuestion(_mapper.Map(model), creatorName);
                if (result)
                {
                    _toaster.AddSuccessToastMessage("Your question has been created!");
                }
                else
                {
                    _toaster.AddErrorToastMessage("Your question has not been created!");
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Questions");

        }

        [HttpGet]
        [Authorize(Policy = "RequireAtLeastUserRole")]
        public IActionResult MyQuestions()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                //var username = this.User.Identity.Name;
                //var questions = _questionService.GetQuestionsOfUser(username);
                //var model = new AllQuestionsViewModel();

                //foreach (var question in questions)
                //{
                //    model.Questions.Add(_mapper.Map(question));
                //}
                //ViewBag.focus = "MyQuestions";
                //return View("Index", model);

                ViewBag.focus = "MyQuestions";
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        [Authorize(Policy = "RequireAtLeastModeratorRole")]
        public IActionResult QuestionsToApprove()
        {

            if (this.User.Identity.IsAuthenticated)
            {
                //var username = this.User.Identity.Name;
                //var questions = _questionService.GetNotApprovedQuestions();
                //var model = new AllQuestionsViewModel();

                //foreach (var question in questions)
                //{
                //    model.Questions.Add(_mapper.Map(question));
                //}
                //ViewBag.focus = "QuestionsToApprove";
                //return View("Index", model);

                ViewBag.focus = "MyQuestions";
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public List<QuestionViewModel> GetQuestions(int take, int skip, string type)
        {
            IEnumerable<Question> questions = null;
            var model = new List<QuestionViewModel>();
            switch (type)
            {
                case "all":
                    questions = _questionService.GetApprovedQuestions(take, skip);
                    foreach (var question in questions)
                    {
                        model.Add(_mapper.Map(question));
                    }
                    return model;
                case "QuestionsToApprove":
                    if (this.User.Identity.IsAuthenticated)
                    {
                        var username = this.User.Identity.Name;
                        questions = _questionService.GetNotApprovedQuestions(take, skip);

                        foreach (var question in questions)
                        {
                            model.Add(_mapper.Map(question));
                        }
                        return model;
                    }
                    break;
                case "MyQuestions":
                    if (this.User.Identity.IsAuthenticated)
                    {
                        var username = this.User.Identity.Name;
                        questions = _questionService.GetQuestionsOfUser(username, take, skip);
                        model = new List<QuestionViewModel>();

                        foreach (var question in questions)
                        {
                            model.Add(_mapper.Map(question));
                        }
                        return model;
                    }
                    break;
            }
            return model;
        }
        
    }
}