using Dbh.BusinessLayer.Contracts;
using Dbh.Common.IoCContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Services
{
    public abstract class ServiceBase
    {
        protected IBusinessObjectFactory GetBusinessObjectFactory()
        {
            return Resolver.Get<IBusinessObjectFactory>();
        }
    }
}
