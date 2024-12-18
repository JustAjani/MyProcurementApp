using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.AdminSection
{
    public partial class AdminCreateProcurement : System.Web.UI.Page
    {
        IContainer container;
        ProcurementTypeDataBase procurementTDB;
        protected async void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void OnSubmitProcurement(object sender, EventArgs e)
        {
            container = (IContainer)Application["AutofacContainer"];
            procurementTDB = container.Resolve<ProcurementTypeDataBase>();
            
            string ProcurementT = PType.Text.ValidateString();
            var PTM = new ProcurementTypeModel()
            {
                Type = ProcurementT
            };

            string isCreated = await procurementTDB.CreateProcurement("insertProcurementType", PTM);

            if (isCreated.Contains("Successfully"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Success', '{isCreated}', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Error', '{isCreated}', 'error');", true);
            }
        }
    }
}