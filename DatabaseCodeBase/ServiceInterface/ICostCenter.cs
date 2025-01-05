using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.ServiceInterface
{
    public interface ICostCenter<T> where T : class
    {
        Task<string> CreateCostCenter(string storeProcedure, T costCenter);
        Task<List<T>> ReadCostCenter(string storeProcedure);
    }
}
