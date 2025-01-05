using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.DBPageUtil;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.AdminSection
{
    public partial class AdminReadCostCentre : PageUtil
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if (!IsPostBack)
            {
                gvCostCenters.BindGridData<CostCenterModel>(CostCenterList);
            }
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            costCenterDB = container.Resolve<CostCenterDB>();
            CostCenterList = await costCenterDB.ReadCostCenter("selectCostCentre");
        }

        protected async void OnEditCommand(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "EditCostCenter")
            {
                int costCenterid = Convert.ToInt32(e.CommandArgument);
                (TextBox costCenterName, _) = sender.FindUIComponent<TextBox, Button>("txtCostCenterName");
                (CheckBox isActive, _) = sender.FindUIComponent<CheckBox, Button>("chkActive");

                var editCostCenter = new CostCenterModel()
                {
                    CostCenterId = costCenterid,
                    CostCenterName = costCenterName.Text.ValidateString(this),
                    isActive = isActive.Checked
                };

                string isUpdated = await costCenterDB.UpdateCostCenter("updateCostCentre", editCostCenter);
                isUpdated.AlertSuccessORFail(this);
                
            }
        }

        
    }
}