using Dbh.Model.EF.Context;
using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace DataGeneratorTests
{
    public class Generator
    {
        [Fact]
        public void GenerateQuestions()
        {
            var ctx = new ApplicationDbContext();
            var questions = new List<Question>();
            for (int i = 0; i < 200; i++)
            {
                var q = new Question
                {
                    ApproveDate = DateTime.Now,
                    ApproverId = "11eb962c-45ea-466d-a522-b0cebbdeddcf",
                    CreationDate = DateTime.Now,
                    CreatorId = "11eb962c-45ea-466d-a522-b0cebbdeddcf",
                    HasAnwser = true,
                    IsApproved = true,
                    IsRejected = false,
                    RejectDate = null,
                    RejectorId = null,
                    RejectReason = null,
                    Title = "Question title " + i,
                    Description = "Question description " + i,
                };
                questions.Add(q);
            }

            ctx.Questions.AddRange(questions);

            ctx.SaveChanges();
        }


        [Fact]
        public void GenerateAnswers()
        {
            var ctx = new ApplicationDbContext();
            var answers = new List<Answer>();

            foreach (var q in ctx.Questions)
            {
                for (int i = 0; i < 2; i++)
                {
                    var a = new Answer
                    {
                        ApproveDate = DateTime.Now,
                        ApproverId = q.CreatorId,
                        CreationDate = DateTime.Now,
                        CreatorId = q.CreatorId,
                        IsApproved = true,
                        IsRejected = false,
                        QuestionId = q.Id,
                        RejectDate = null,
                        RejectReason = null,
                        RejectorId = null,
                        Response = "response to this question " + i
                    };
                    answers.Add(a);
                }
            }
            ctx.Answers.AddRange(answers);

            ctx.SaveChanges();
        }
    }
}
