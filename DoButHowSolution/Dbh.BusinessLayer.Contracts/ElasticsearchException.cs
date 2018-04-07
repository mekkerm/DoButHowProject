using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public class ElasticsearchException : BusinessException
    {
        public ElasticsearchException(string Message) : base(Message)
        {
        }
    }
}
