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
    public partial class AdminCreateProcurement : PageUtil
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected async void OnSubmitProcurement(object sender, EventArgs e)
        {
            container = (IContainer)Application["AutofacContainer"];
            procurementTypeDB = container.Resolve<ProcurementTypeDB>();

            string ProcurementType = PType.Text.ValidateString(this);
            var PTM = new ProcurementTypeModel()
            {
                Type = ProcurementType
            };

            string isCreated = await procurementTypeDB.CreateProcurement("insertProcurementType", PTM);

            isCreated.AlertSuccessORFail(this);
        }
    }
}