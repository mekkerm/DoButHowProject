﻿using Dbh.Model.EF.Entities;
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
            dest.CreatorName = source.Creator.UserName;
            dest.CreatorId = source.CreatorId;

            return dest;
        }

        public Question Map(QuestionViewModel source)
        {
            var dest = new Question();
            dest.Title = source.Title;
            dest.Description = source.Description;

            return dest;
        }
    }
}
