using Dbh.Model.EF.Entities;
using MVCWebClient.Models.AnswerViewModels;
using MVCWebClient.Models.QuestionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Services
{
    public class MapperService
    {
        public QuestionViewModel Map(Question source)
        {
            var dest = new QuestionViewModel();
            dest.QuestionId = source.Id;
            dest.Title = source.Title;
            dest.Description = source.Description;
            dest.CreatorName = source.Creator != null ? source.Creator.UserName : "";
            dest.CreatorId = source.CreatorId;
            dest.IsApproved = source.IsApproved;
            dest.IsRejected = source.IsRejected;
            dest.RejectReason = source.RejectReason;
            dest.Status = source.IsApproved ? "Approved" :
                source.IsRejected ? "Rejected" : "Created";

            return dest;
        }

        public void Map(Question source, QuestionViewModel dest)
        {
            dest.QuestionId = source.Id;
            dest.Title = source.Title;
            dest.Description = source.Description;
            dest.CreatorName = source.Creator != null ? source.Creator.UserName : "";
            dest.CreatorId = source.CreatorId;
            dest.IsApproved = source.IsApproved;
            dest.IsRejected = source.IsRejected;
            dest.RejectReason = source.RejectReason;
            dest.Status = source.IsApproved ? "Approved" :
                source.IsRejected ? "Rejected" : "Created";
            
        }

        public AnswerViewModel Map(Answer source)
        {
            var dest = new AnswerViewModel();
            dest.AnswerId = source.Id;
            dest.QuestionId = source.QuestionId;
            dest.Response = source.Response;

            dest.CreatorName = source.Creator != null ? source.Creator.UserName : "";
            dest.CreatorId = source.CreatorId;
            dest.IsApproved = source.IsApproved;
            dest.IsRejected = source.IsRejected;
            dest.RejectReason = source.RejectReason;
            dest.Status = source.IsApproved ? "Approved" :
                source.IsRejected ? "Rejected" : "Created";

            dest.CurrentRating = source.AverageRating;
            dest.CurrentUserRating = source.CurrentUserRating;
            dest.CurrentRatingCount = source.CurrentRatingCount;

            return dest;
        }

        public List<AnswerViewModel> Map(IEnumerable<Answer> sourceList)
        {
            var results = new List<AnswerViewModel>();
            foreach (var source in sourceList)
            {
                results.Add(Map(source));
            }
            return results; ;
        }

        public Question Map(QuestionViewModel source)
        {
            var dest = new Question();
            dest.Title = source.Title;
            dest.Description = source.Description;
            dest.Id = source.QuestionId;
            dest.CategoryId = source.QuestionCategoryId;

            return dest;
        }
    }
}
