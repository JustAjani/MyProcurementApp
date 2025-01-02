using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.DBPageUtil;
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
    public partial class AdminReadProcurement : PageUtil
    {
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if(!IsPostBack)
            {
                gvProcurements.BindGridData<ProcurementTypeModel>(ProcurementTypeList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            procurementTypeDB = container.Resolve<ProcurementTypeDB>();
            ProcurementTypeList = await procurementTypeDB.ReadProcurementType("selectProcurementTypes");
        }

        protected async void OnEditCommand(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "EditProcurement")
            {
                (var textBox, var btn) = sender.FindUIComponent<TextBox,Button>("txtProcurementType");
                int procurementId = Convert.ToInt32(e.CommandArgument);
                var Type = textBox.Text.ValidateString(this);

                var procurementUpdate = new ProcurementTypeModel()
                {
                    ID = procurementId,
                    Type = Type,
                };

                var isPTUpdated = await procurementTypeDB.EditProcurementType("updateProcurementType", procurementUpdate);
                isPTUpdated.AlertSuccessORFail(this);
            }
        }
    }
}