using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.DBPageUtil;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using InputOutputOperation.PDFUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.UserSection
{
    public partial class DisplayProcurement : PageUtil
    {
 
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if (!IsPostBack)
            {
                gvProcurements.BindGridData<ProcurementModel>(ProcurementList);
                gvProcurements.BindDropDownFromGridView<ProcurementTypeModel>("ddlType","Type","ID","[Select Procurment Type]", ProcurementTypeList);
                gvProcurements.BindDropDownFromGridView<UserModel>("ddlOfficer", "Name", "UserId", "[Select Officer]", UserList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this,e);
        }

        protected override async Task InitializeDependecy()
        {
            container = (Autofac.IContainer)Application["AutofacContainer"];
            (userDB, procurementDB, procurementTypeDB) = container.InitializeDependency<UserDB, ProcurementDB, ProcurementTypeDB>();
            UserList = await userDB.ReadUser("selectUsersByRoleId");
            ProcurementList = await procurementDB.ReadProcurement("selectProcurementTracking");
            ProcurementTypeList = await procurementTypeDB.ReadProcurementType("selectProcurementTypes");
        }

        protected void OnTypeChanged(object sender, EventArgs e)
        {

        }

        protected void OnStatusChanged(object sender, EventArgs e)
        {

        }

        protected async void OnViewProcurement(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewProcurement")
                {
                    var procurementID = Convert.ToInt32(e.CommandArgument);
                    Aprocurement = await procurementDB.ReadProcurementByID("selectProcurementTrackingById", procurementID);
                    var pDF = new ProcurementPDF();
                    await pDF.CreatePDF(Aprocurement, this);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }

        protected void OnOfficerChanged(object sender, EventArgs e)
        {

        }
    }
}