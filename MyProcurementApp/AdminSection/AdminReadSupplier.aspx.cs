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
    public partial class AdminReadSupplier : PageUtil
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if (!IsPostBack)
            {
                gvSuppliers.BindGridData<SupplierModel>(SupplierList);
            }
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            supplierDB = container.Resolve<SupplierDB>();
            SupplierList = await supplierDB.ReadSupplier("selectAllSuppliers");
        }

        protected async void OnEditCommand(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "EditSupplier")
            {
                int supplierId = Convert.ToInt32(e.CommandArgument);
                (TextBox supplierName, _) = sender.FindUIComponent<TextBox,Button>("txtSupplierName");
                (CheckBox isActive, _) = sender.FindUIComponent<CheckBox, Button>("chkActive");

                var updatedSupplier = new SupplierModel()
                {
                    SupplierId = supplierId,
                    SupplierName = supplierName.Text.ValidateString(this),
                    IsActive = isActive.Checked,
                };

                string isUpdated = await supplierDB.UpdateSupplier("updateSupplier", updatedSupplier);
                isUpdated.AlertSuccessORFail(this);
            }
        }
    }
}