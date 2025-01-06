using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.DBPageUtil;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using InputOutputOperation.PDFUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.UserSection
{
    public partial class DisplayProcurement : PageUtil
    {
        int userID;
        protected async Task Page_Load(object sender, EventArgs e)
        {
            userID = Session["TransferID"].ToString().ConvertStringTo<int>();
            await InitializeDependecy();
            LoadLabels();
            if (!IsPostBack)
            {
                gvProcurements.BindGridData<ProcurementModel>(ProcurementList);
                gvProcurements.BindDropDownFromGridView<ProcurementTypeModel>("ddlType","Type","ID", "[Select Procurment Type]", ProcurementTypeList);
                gvProcurements.BindDropDownFromGridView<UserModel>("ddlOfficer", "Name", "UserId", "[Select Officer]", UserList);
                gvProcurements.BindDropDownFromGridView<SupplierModel>("ddlSupplier", "SupplierName", "SupplierId", "[Select Supplier]", SupplierList);
                gvProcurements.BindDropDownFromGridView<CostCenterModel>("ddlCostCentre", "CostCenterName", "CostCenterId", "[Select CostCenrer]", CostCenterList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this,e);
        }

        protected void LoadLabels()
        {
            gvProcurements.FindDataWithGrid<Label>("lblOfficer", officerLabel =>
            {
                var procurement = ProcurementList.FirstOrDefault(u => u.UserId == userID);
                if (procurement != null)
                {
                    officerLabel.Text = UserList.FirstOrDefault(u => u.UserId == procurement.ProcurementOfficer)?.Name ?? "N/A";
                }
            });

            gvProcurements.FindDataWithGrid<Label>("lblType", typeLabel =>
            {
                var procurement = ProcurementList.FirstOrDefault(u => u.UserId == userID);
                if (procurement != null)
                {
                    typeLabel.Text = ProcurementTypeList.FirstOrDefault(p => p.ID == procurement.ProcurementTypeId)?.Type ?? "N/A";
                }
            });

            gvProcurements.FindDataWithGrid<Label>("lblCostCentre", costCenterLabel =>
            {
                var procurement = ProcurementList.FirstOrDefault(u => u.UserId == userID);
                if (procurement != null)
                {
                    costCenterLabel.Text = CostCenterList.FirstOrDefault(c => c.CostCenterId == procurement.CostCentreId)?.CostCenterName ?? "N/A";
                }
            });

            gvProcurements.FindDataWithGrid<Label>("lblSupplier", supplierLabel =>
            {
                var procurement = ProcurementList.FirstOrDefault(u => u.UserId == userID);
                if (procurement != null)
                {
                    supplierLabel.Text = SupplierList.FirstOrDefault(s => s.SupplierId == procurement.RecommendedSupplierID)?.SupplierName ?? "N/A";
                }
            });
        }



        protected override async Task InitializeDependecy()
        {
            container = (Autofac.IContainer)Application["AutofacContainer"];
            (userDB, procurementDB, procurementTypeDB) = container.InitializeDependency<UserDB, ProcurementDB, ProcurementTypeDB>();
            (costCenterDB, supplierDB) = container.InitializeDependency<CostCenterDB, SupplierDB>();
            UserList = await userDB.ReadUser("selectProcurementOfficer");
            ProcurementList = await procurementDB.ReadByProcurementID("selectProcurementTrackingByUserId", userID, "@UserId");
            ProcurementTypeList = await procurementTypeDB.ReadProcurementType("selectProcurementTypes");
            CostCenterList = await costCenterDB.ReadCostCenter("selectCostCentre");
            SupplierList = await supplierDB.ReadSupplier("selectAllSuppliers");        
        }


        protected async void OnViewProcurement(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewProcurement")
                {
                    var procurementID = Convert.ToInt32(e.CommandArgument);
                    var pList = await procurementDB.ReadByProcurementID("selectProcurementTrackingById", procurementID, "@ProcurementTrackingId");
                    Aprocurement = pList.FirstOrDefault();
                    var pDF = new ProcurementPDF();
                    await pDF.CreatePDF(Aprocurement, this);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }

        protected async void OnEditProcurement(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "EditProcurement")
            {
                int procurementID = Convert.ToInt32(e.CommandArgument);
                (DropDownList ddlOfficer, _)= sender.FindUIComponent<DropDownList, Button>("ddlOfficer");
                (DropDownList ddlType, _) = sender.FindUIComponent<DropDownList, Button>("ddlType");
                (TextBox costCenterTB, _) = sender.FindUIComponent<TextBox, Button>("txtCostCentre");
                (TextBox descriptionTB, _) = sender.FindUIComponent<TextBox, Button>("txtDescription");
                (TextBox lotTB, _) = sender.FindUIComponent<TextBox, Button>("txtLot");
                (TextBox estimateTB, _) = sender.FindUIComponent<TextBox, Button>("txtEstimate");
                (TextBox requestDateTB, _) = sender.FindUIComponent<TextBox, Button>("txtRequestDate");
                (TextBox publicationDateTB, _) = sender.FindUIComponent<TextBox, Button>("txtPublicationDate");
                (TextBox contractValueTB, _) = sender.FindUIComponent<TextBox, Button>("txtContractValue");
                (TextBox supplierTB, _) = sender.FindUIComponent<TextBox, Button>("txtSupplier");

                var editedProcurement = new ProcurementModel()
                {
                    ProcurementTrackingId = procurementID,
                    ProcurementTypeId = ddlType.SelectedValue.ValidateString(this).ConvertStringTo<int>(),
                    ProcurementOfficer = ddlOfficer.SelectedValue.ValidateString(this).ConvertStringTo<int>(),
                    
                    Description = descriptionTB.Text.ValidateString(this),
                    LotProcurement = lotTB.Text.ValidateString(this),
                    ComparativeEstimate = estimateTB.Text.ValidateString(this).Replace("$","").ConvertStringTo<decimal>(),
                    DateOfRequest = requestDateTB.Text.ValidateString(this).StringToDate(this),
                    PublicationDate = publicationDateTB.Text.ValidateString(this).StringToDate(this),
                    ActualContractValue = contractValueTB.Text.ValidateString(this).Replace("$","").ConvertStringTo<decimal>(),
                    
                };

                string isComplete = await procurementDB.UpdateProcurement("updateProcurementTrackingById", editedProcurement);
                isComplete.AlertSuccessORFail(this);
            }
        }
    }
}