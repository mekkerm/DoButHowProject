using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IIndexer<T> where T: class
    {

        bool IndexData(IEnumerable<T> data);

        bool IndexData(T data);
    }
}
