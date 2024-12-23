using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelperFunctions.Extension;

namespace MyProcurementApp.UserSection
{
    public partial class CreateProcurement : System.Web.UI.Page
    {
        IContainer container;
        ProcurementDB procurementDB;
        ProcurementTypeDB pTDB;
        UserDB userDB;
        List<UserModel> officerList;
        List<ProcurementTypeModel> pTList;
        protected async Task Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                await InitializeDependecy();
                ddlOfficer.BindDropDownData<UserModel>("Name", "UserID", "[Select Procurement Type]", officerList);
                ddlProcurementType.BindDropDownData<ProcurementTypeModel>("Type", "ID", "Select procurement Type", pTList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        private async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            (procurementDB, userDB, pTDB) = container.InitializeDependency<ProcurementDB, UserDB, ProcurementTypeDB>();
            pTList = await pTDB.ReadProcurementType("selectProcurementTypes");
            officerList = await userDB.ReadUser("selectUsersByRoleId");
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            
        }
    }
}