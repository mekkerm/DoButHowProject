using Dbh.Model.EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.BL
{
    public class BusinessObjectBase
    {
        protected IUnitOfWork _uow;

        public BusinessObjectBase(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
