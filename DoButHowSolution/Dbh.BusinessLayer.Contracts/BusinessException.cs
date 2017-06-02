using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public class BusinessException:Exception
    {
        public BusinessException(String Message):base(Message)
        {

        }
    }
}
