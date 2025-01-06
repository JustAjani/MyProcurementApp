using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.DBPageUtil;
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
    public partial class AdminCreateSupplier : PageUtil
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void OnSubmitSupplier(object sender, EventArgs e)
        {
            container = (IContainer)Application["AutofacContainer"];
            supplierDB = container.Resolve<SupplierDB>();

            var addSupplier = new SupplierModel()
            {
                SupplierName = txtSupplierName.Text.ValidateString(this),
                IsActive = true,
            };

            string isCreated = await supplierDB.CreateSupplier("insertSupplier", addSupplier);
            isCreated.AlertSuccessORFail(this);
        }
    }
}