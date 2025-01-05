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
            if (!IsPostBack)
            {
                gvProcurements.BindGridData<ProcurementModel>(ProcurementList);
                gvProcurements.BindDropDownFromGridView<ProcurementTypeModel>("ddlType","Type","ID", "[Select Procurment Type]", ProcurementTypeList);
                gvProcurements.BindDropDownFromGridView<UserModel>("ddlOfficer", "Name", "UserId", "[Select Officer]", UserList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this,e);
        }

        protected override async Task InitializeDependecy()
        {
            container = (Autofac.IContainer)Application["AutofacContainer"];
            (userDB, procurementDB, procurementTypeDB) = container.InitializeDependency<UserDB, ProcurementDB, ProcurementTypeDB>();
            UserList = await userDB.ReadUser("selectUsersByRoleId");
            ProcurementList = await procurementDB.ReadByProcurementID("selectProcurementTrackingByUserId", userID, "@UserId");
            ProcurementTypeList = await procurementTypeDB.ReadProcurementType("selectProcurementTypes");
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