using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.ServiceInterface
{
    public interface IProcurementType<T> where T : class
    {
        Task<string> CreateProcurement(string storedProcedure, T procurmentTypeModel);
        Task<List<T>> ReadProcurementType(string stroredProcdure);
        Task<string> EditProcurementType(string storedProcedure, T procurmentTypeModel);    
    }
}
