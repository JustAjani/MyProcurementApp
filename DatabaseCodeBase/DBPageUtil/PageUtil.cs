using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI;

namespace DatabaseCodeBase.DBPageUtil
{
    public abstract class PageUtil : Page
    {
        // database Container
        protected IContainer container;
        protected UserDB userDB;
        protected ProcurementDB procurementDB;
        protected RoleDB roleDB;
        protected ProcurementTypeDB procurementTypeDB;
        protected CostCenterDB costCenterDB;
        protected SupplierDB supplierDB;

        // pdf info container
        protected ProcurementModel Aprocurement {  get; set; } = new ProcurementModel();

        // model list
        protected List<UserModel> UserList { get; set; } = new List<UserModel>();
        protected List<ProcurementTypeModel> ProcurementTypeList { get; set; } = new List<ProcurementTypeModel>();
        protected List<RoleModel> RoleList { get; set; } = new List<RoleModel>();
        protected List<ProcurementModel> ProcurementList { get; set; } = new List<ProcurementModel>();
        protected List<CostCenterModel> CostCenterList { get; set; } = new List<CostCenterModel>();
        protected List<SupplierModel> SupplierList { get; set; } = new List<SupplierModel>();

        // dependency Initialization function
        protected virtual async Task InitializeDependecy()
        {
            await Task.CompletedTask;
        }

        // Redirects user to homepage based on roleID
        protected void RedirectUserOrManageHomePage(int roleID, string userName, int userID)
        {
            if (roleID == 3)
            {
                Response.Redirect($"~/AdminSection/Manager.aspx?manager={userName}?id={userID}", false);
            }
            else
            {
                Response.Redirect($"Procurement.aspx?userName={userName}?id={userID}", false);
            }

            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
