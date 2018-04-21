using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public class AnswerHeaderDTO
    {
        public int Id { get; set; }
        public string CreatorName { get; set; }

        public string CreatorId { get; set; }

        public int QuestionId { get; set; }
        public string Response { get; set; }

        public string QuestionTitle { get; set; }
    }
}
