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
    public partial class AdminReadUserProcurement : PageUtil
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if(!IsPostBack)
            {
                gvProcurements.BindGridData<ProcurementModel>(ProcurementList);
            }
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            procurementDB = container.Resolve<ProcurementDB>();
            ProcurementList = await procurementDB.ReadProcurement("selectProcurementTracking");
        }
    }
}