using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dbh.Model.EF.Entities
{
    public class QuestionCategory : BaseEntity
    {
        [MaxLength(80)]
        public string Name { get; set; }
    }
}
