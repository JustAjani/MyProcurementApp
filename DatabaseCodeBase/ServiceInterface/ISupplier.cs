using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.ServiceInterface
{
    internal interface ISupplier<T> where T : class
    {
        Task<string> CreateSupplier(string storeProcedure, T supplier);
        Task<List<T>> ReadSupplier(string storeProcedure);
        Task<string> UpdateSupplier(string storeProcedure, T supplier);
    }
}
