using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.AdminSection
{
    public partial class AdminReadProcurement : System.Web.UI.Page
    {
        private ProcurementTypeDB procurementDB;
        private IContainer container;
        private List<ProcurementTypeModel> procurementList;
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await ResolveDependecy();
            if(!IsPostBack)
            {
                BindProcurementData();
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        private async Task ResolveDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            procurementDB = container.Resolve<ProcurementTypeDB>();
            procurementList = await procurementDB.ReadProcurementType("selectProcurementTypes");
        }

        private void BindProcurementData()
        {
            gvProcurements.DataSource = procurementList;
            gvProcurements.DataBind();
        }
        protected async void OnEditCommand(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "EditProcurement")
            {
                var button = (Button)sender;
                int procurementId = Convert.ToInt32(e.CommandArgument);
                var row = (GridViewRow)button.NamingContainer;
                var pType = (TextBox)row.FindControl("txtProcurementType");
                var Type = pType.Text.ValidateString(this);

                var procurementUpdate = new ProcurementTypeModel()
                {
                    ID = procurementId,
                    Type = Type,
                };

                var isPTUpdated = await procurementDB.EditProcurementType("updateProcurementType", procurementUpdate);
                isPTUpdated.AlertSuccessORFail(this);
            }
        }
    }
}