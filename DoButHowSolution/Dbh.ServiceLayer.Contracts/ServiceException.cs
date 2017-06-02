using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Contracts
{
    public class ServiceException:Exception
    {
        public ServiceException(String Message):base(Message)
        {

        }

        public ServiceException(Exception ex) : base(ex.Message)
        {

        }
    }
}
