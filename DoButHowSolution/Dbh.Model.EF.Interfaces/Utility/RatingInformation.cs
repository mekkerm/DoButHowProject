using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.Model.EF.Utility
{
    public class RatingInformation
    {
        public decimal AverageRating { get; set; }
        public decimal RatingCount { get; set; }
        public decimal CurrentUserRating { get; set; }
    }
}
