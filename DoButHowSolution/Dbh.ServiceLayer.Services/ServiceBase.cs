using Dbh.BusinessLayer.Contracts;
using Dbh.Common.IoCContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Services
{
    public abstract class ServiceBase
    {
        protected IBusinessObjectFactory GetUoW()
        {
            return Resolver.Get<IBusinessObjectFactory>();
        }
    }
}
