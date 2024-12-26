using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace DatabaseCodeBase.DBPageUtil
{
    public abstract class PageUtil : Page
    {
        protected IContainer container;
        protected UserDB userDB;
        protected ProcurementDB procurementDB;
        protected RoleDB roleDB;
        protected ProcurementTypeDB procurementTypeDB;

        protected List<UserModel> userList;
        protected List<ProcurementTypeModel> procurementTypeList;
        protected List<RoleModel> roleList;
        protected List<ProcurementModel> procurementList;
        protected virtual async Task InitializeDependecy() { }
    }
}
