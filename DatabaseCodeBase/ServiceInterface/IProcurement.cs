using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.ServiceInterface
{
    public interface IProcurement<T> where T : class
    {
        Task<string> CreateProcurement(string storedProcedure, T procurement);
        Task<string> UpdateProcurement(string storedProcedure, T procurement);
        Task<List<T>>ReadProcurement(string storedProcedure);
        Task<T>ReadProcurementByID(string storedProcedure , int iD);
        Task<string> DeleteProcurement(string storedProcedure, T procurement);
    }
}
