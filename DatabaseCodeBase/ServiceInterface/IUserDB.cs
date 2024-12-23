using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.ServiceInterface
{
    public interface IUserDB<T> where T : class
    {
        Task<string> CreateUser(string storedprocedure , T userModel);
        Task<List<T>> ReadUser(string storedprocedure);
        Task<string> UpdateUser(string storedprocedure , T userModel);
        Task<string> UpdateUserActivity(string storedprocedure , T userModel);
        Task<string> UpdateUserRole(string storedprocedure , T userModel);
        Task<string> DeleteUser(string storedprocedure , int UserID);
    }
}
