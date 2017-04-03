using System.Collections.Generic;

namespace Dbh.Model.EF.Entities
{
    public class Blog:BaseEntity
    {
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }
}
