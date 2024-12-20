using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.ServiceInterface
{
    public interface IRole<T> where T : class
    {
        Task<List<T>> ReadRoles(string storedProcedure);
        Task<string> CreateRoles(string storedProcedure, T RoleModel);

    }
}
