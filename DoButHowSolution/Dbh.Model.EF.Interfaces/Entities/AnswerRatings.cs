using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.Model.EF.Entities
{
    public class AnswerRatings:BaseEntity
    {

        public int AnswerId { get; set; }

        public string UserId { get; set; }

        public decimal Rate { get; set; }

        public DateTime? RateDate { get; set; }
    }
}
