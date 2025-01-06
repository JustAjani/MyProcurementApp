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
        private List<UserModel> usersList;
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
            container = (Autofac.IContainer)Application["AutofacContainer"];
            (userDB, procurementDB, procurementTypeDB) = container.InitializeDependency<UserDB, ProcurementDB, ProcurementTypeDB>();
            (costCenterDB, supplierDB) = container.InitializeDependency<CostCenterDB, SupplierDB>();
            UserList = await userDB.ReadUser("selectProcurementOfficer");
            usersList = await userDB.ReadUser("selectAllUsers");
            ProcurementTypeList = await procurementTypeDB.ReadProcurementType("selectProcurementTypes");
            CostCenterList = await costCenterDB.ReadCostCenter("selectCostCentre");
            SupplierList = await supplierDB.ReadSupplier("selectAllSuppliers");
            ProcurementList = await procurementDB.ReadProcurement("selectProcurementTracking");

        }

        protected void gvProcurements_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var procurement = e.Row.DataItem as ProcurementModel;

                if (procurement != null)
                {
                    var lblOfficer = e.Row.FindControl("lblOfficer") as Label;
                    var lblUser = e.Row.FindControl("lblUser") as Label;
                    var lblCostCentre = e.Row.FindControl("lblCostCentre") as Label;
                    var lblType = e.Row.FindControl("lblType") as Label;
                    var lblSupplier = e.Row.FindControl("lblSupplier") as Label;

                    if (lblOfficer != null)
                        lblOfficer.Text = UserList.FirstOrDefault(u => u.UserId == procurement.ProcurementOfficer)?.Name ?? "N/A";
                    if (lblUser != null)
                        lblUser.Text = usersList.FirstOrDefault(u => u.UserId == procurement.UserId)?.Name ?? "N/A";
                    if (lblCostCentre != null)
                        lblCostCentre.Text = CostCenterList.FirstOrDefault(c => c.CostCenterId == procurement.CostCentreId)?.CostCenterName ?? "N/A";
                    if (lblType != null)
                        lblType.Text = ProcurementTypeList.FirstOrDefault(p => p.ID == procurement.ProcurementTypeId)?.Type ?? "N/A";
                    if (lblSupplier != null)
                        lblSupplier.Text = SupplierList.FirstOrDefault(s => s.SupplierId == procurement.RecommendedSupplierID)?.SupplierName ?? "N/A";
                }
            }
        }


    }
}