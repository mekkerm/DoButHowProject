using Dbh.Model.EF.Interfaces;
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
