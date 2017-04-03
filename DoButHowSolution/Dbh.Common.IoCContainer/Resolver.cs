using Dbh.BusinessLayer.BL;
using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.UnitOfWork;
using StructureMap;

namespace Dbh.Common.IoCContainer
{
    public static class Resolver
    {

        private static Container _container;
        static Resolver()
        {
            _container = new Container(x=>
            {
                x.For<IUnitOfWork>().Add<UnitOfWork>();

                x.For<IBusinessObjectFactory>().Add<BusinessObjectFactory>();

            });
        }

        public static T Get<T>()
        {
            return _container.GetInstance<T>();
        }
    }
}
