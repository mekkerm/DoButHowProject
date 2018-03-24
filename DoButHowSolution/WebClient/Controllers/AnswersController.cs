﻿using Dbh.Model.EF.Entities;
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
    public class AnswersController : Controller
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
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult MyRejectedAnswers()
        {
            ViewBag.focus = "MyRejectedAnswers";
            return View("Index");
        }

        public IActionResult AnswersToApprove()
        {
            ViewBag.focus = "AnswersToApprove";
            return View("Index");
        }

        public List<AnswerViewModel> GetAnswers(int take, int skip, string type)
        {
            IEnumerable<Answer> answers = null;
            var model = new List<AnswerViewModel>();
            switch (type)
            {
                case "all":
                    if (this.User.Identity.IsAuthenticated)
                    {
                        var username = this.User.Identity.Name;
                        answers = _answerService.GetAnswersOfUser(username, skip, take);
                        foreach (var answer in answers)
                        {
                            model.Add(_mapper.Map(answer));
                        }
                        return model;
                    }
                    break;
                case "AnswersToApprove":
                    answers = _answerService.GetNotApprovedAnswers(skip, take);

                    foreach (var answer in answers)
                    {
                        var ans = _mapper.Map(answer);
                        ans.QuestionTitle = _questionService.GetQuestionTitle(answer.QuestionId);
                        model.Add(ans);

                    }
                    break;
                case "MyRejectedAnswers":
                    var uname = this.User.Identity.Name;
                    answers = _answerService.GetRejectedQuestionsOfUser(uname, skip, take);

                    foreach (var answer in answers)
                    {
                        var ans = _mapper.Map(answer);
                        ans.QuestionTitle = _questionService.GetQuestionTitle(answer.QuestionId);
                        model.Add(ans);

                    }
                    break;
            }
            return model;
        }
    }
}
