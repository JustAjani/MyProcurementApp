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
    public partial class AdminCreateCostCenter : PageUtil
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected async void OnSubmitCostCenter(object sender, EventArgs e)
        {
            container = (IContainer)Application["AutofacContainer"];
            costCenterDB = container.Resolve<CostCenterDB>();
            var validatedCCN = txtcostCenterName.Text.ValidateString(this);

            var addCostCenter = new CostCenterModel()
            {
                CostCenterName = validatedCCN
            };

            string isCreated =await costCenterDB.CreateCostCenter("insertCostCentre", addCostCenter);
            isCreated.AlertSuccessORFail(this);
        }
    }
}