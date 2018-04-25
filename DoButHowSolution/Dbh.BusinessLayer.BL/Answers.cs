using Dbh.BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.Entities;
using System.Linq;
using Dbh.Model.EF.Utility;

namespace Dbh.BusinessLayer.BL
{
    public class Answers : BusinessObjectBase, IAnswers
    {
        public Answers(IUnitOfWork uow)
            : base(uow)
        { }

        public bool CreateAnswer(int questionId, string response, string creatorName)
        {
            var user = _uow.AppUsers.GetUserByName(creatorName);

            var answers = _uow.Answers.FindAll(a => a.CreatorId == user.Id && a.QuestionId == questionId ).ToList();
            //if (answers.Any())
            //{
            //    throw new BusinessException("You have anwsered already this question!");
            //}

            var answer = new Answer();
            answer.QuestionId = questionId;
            answer.Response = response;
            answer.CreationDate = DateTime.Now;
            answer.CreatorId = user.Id;
            answer.IsApproved = false;
            answer.IsRejected = false;

            _uow.Answers.Add(answer);
            return true;
            
        }

        public IEnumerable<AnswerHeaderDTO> GetNotApprovedAnswers(int skip, int take)
        {
            var answers = _uow.Answers.FindAll(q => !q.IsApproved && !q.IsRejected).Skip(skip).Take(take).Select(x => new AnswerHeaderDTO
            {
                Id = x.Id,
                Response = x.Response,
                CreatorId = x.CreatorId,
                QuestionId = x.QuestionId
            });
               
            foreach (var answer in answers)
            {
                var creator = _uow.AppUsers.GetUser(answer.CreatorId);
                answer.CreatorName = creator.UserName;
            }
            return answers;
        }

        public Answer GetAnswerById(int id)
        {
            var answer = _uow.Answers.Get(id);

            answer.Creator = _uow.AppUsers.GetUser(answer.CreatorId);
            
            return answer;
        }

        public void ApproveAnswer(int answerId, string username)
        {
            var answer = _uow.Answers.Get(answerId);
            var question = _uow.Questions.Get(answer.QuestionId);
            
            if (!answer.IsApproved && !answer.IsRejected)
            {
                var approver = _uow.AppUsers.GetUserByName(username);

                answer.ApproveDate = DateTime.Now;
                answer.ApproverId = approver.Id;
                answer.IsApproved = true;
                answer.IsRejected = false;
                answer.RejectReason = null;

                question.HasAnwser = true;
            }
            
        }

        public void RejectAnswer(int answerId, string rejectReason, string username)
        {
            var answer = _uow.Answers.Get(answerId);
            if (!answer.IsRejected)
            {
                var rejector = _uow.AppUsers.GetUserByName(username);

                answer.RejectDate = DateTime.Now;
                answer.RejectorId = rejector.Id;
                answer.IsApproved = false;
                answer.IsRejected = true;
                answer.RejectReason = rejectReason;
                answer.ApproveDate = null;
            }
        }

        public IEnumerable<Answer> GetAnswers(int skip, int take)
        {
            return _uow.Answers.FindAll(a => a.IsApproved).Skip(skip).Take(take);
        }

        public IEnumerable<Answer> GetAnswersOfQuestion(int questionId, string username)
        {
            var answers = _uow.Answers.FindAll(a => a.QuestionId == questionId && a.IsApproved).ToList();
            
            
            foreach (var answer in answers)
            {
                var ratingInfo = GetRatingInformation(answer.Id, username);

                answer.AverageRating = ratingInfo.AverageRating;
                answer.CurrentRatingCount = ratingInfo.RatingCount;
                answer.CurrentUserRating = ratingInfo.CurrentUserRating;
            }
            
            return answers.OrderByDescending(x => x.AverageRating); 
        }

        public IEnumerable<AnswerHeaderDTO> GetAnswersOfUser(string username, int skip, int take)
        {
            var user = _uow.AppUsers.GetUserByName(username);

            return _uow.Answers.FindAll(a => a.CreatorId == user.Id).Skip(skip).Take(take).Select(x=> new AnswerHeaderDTO
            {
                Id = x.Id,
                Response = x.Response,
                QuestionId = x.QuestionId
            });
        }

        public IEnumerable<AnswerHeaderDTO> GetRejectedAnswersOfUser(string username, int skip, int take)
        {
            var user = _uow.AppUsers.GetUserByName(username);

            return _uow.Answers.FindAll(a => a.CreatorId == user.Id && a.IsRejected).Skip(skip).Take(take).Select(x => new AnswerHeaderDTO
            {
                Id = x.Id,
                Response = x.Response,
                QuestionId = x.QuestionId
            });
        }

        public void CorrectAnswer(int answerId, string response)
        {
            var oldAnswer = _uow.Answers.Get(answerId);
            oldAnswer.Response = response;

            oldAnswer.ApproveDate = null;
            oldAnswer.RejectDate = null;
            oldAnswer.IsApproved = false;
            oldAnswer.IsRejected = false;
            oldAnswer.RejectReason = null;


            var answer = _uow.Answers.Get(answerId);
            var question = _uow.Questions.Get(answer.QuestionId);
            question.HasAnwser = true;
        }

        public void AddOrModifyAnswerRating(int answerId, string username, decimal rating)
        {
            var userId = _uow.AppUsers.GetUserIdByName(username);
            if (userId != null)
            {
                var answerRating = _uow.AnswerRatings.SingleOrDefault(x => x.AnswerId == answerId && x.UserId == userId);

                if(answerRating == null)
                {
                    //create
                    var newRating = new AnswerRatings();
                    newRating.AnswerId = answerId;
                    newRating.Rate = rating;
                    newRating.RateDate = DateTime.Now;
                    newRating.UserId = userId;
                    _uow.AnswerRatings.Add(newRating);
                }
                else
                {
                    //update 
                    if(answerRating.Rate != rating) {
                        answerRating.Rate = rating;
                    }
                }
            }
        }


        public void RemoveAnswerRating(int answerId, string username)
        {
            var userId = _uow.AppUsers.GetUserIdByName(username);
            if (userId != null)
            {
                var answerRating = _uow.AnswerRatings.SingleOrDefault(x => x.AnswerId == answerId && x.UserId == userId);

                if (answerRating == null)
                {
                   //do nothing
                }
                else
                {
                    //remove the existing rating
                    _uow.AnswerRatings.Remove(answerRating);
                }
            }
        }

        public RatingInformation GetRatingInformation(int asnwerId, string username)
        {
            var result = new RatingInformation();

            var ratings = _uow.AnswerRatings.FindAll(x => x.AnswerId == asnwerId);
            result.AverageRating = ratings.Any() ? ratings.Average(x => x.Rate) : 0;
            result.RatingCount = ratings.Count();
            if (!string.IsNullOrEmpty(username))
            {
                var userId = _uow.AppUsers.GetUserIdByName(username);
                var usersRating = ratings.FirstOrDefault(x => x.UserId == userId);
                result.CurrentUserRating = usersRating != null ? usersRating.Rate : 0;

            }

            return result;
        }
    }
}
