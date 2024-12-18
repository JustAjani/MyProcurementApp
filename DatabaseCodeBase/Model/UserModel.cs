using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string UserStatus { get; set; }
    }
}
