using DatabaseCodeBase.Model;
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
        Task<List<T>> ReadByProcurementID(string storedProcedure , int iD, string selectedfield);
        Task<string> DeleteProcurement(string storedProcedure, T procurement);
        Task<List<T>> FilterProcurementByLot(string storedProcedure, string lotProcurement);
    }
}
