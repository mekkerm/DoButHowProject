﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public class QuestionHeaderDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }

        public int AnswerCount { get; set; }

        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; }
    }
}
