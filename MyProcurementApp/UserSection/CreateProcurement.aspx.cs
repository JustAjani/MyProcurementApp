using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelperFunctions.Extension;
using DatabaseCodeBase.DBPageUtil;

namespace MyProcurementApp.UserSection
{
    public partial class CreateProcurement : PageUtil
    {
        int UserID;
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            UserID = Session["TransferID"].ToString().ConvertStringTo<int>();
            if (!IsPostBack)
            {
                ddlOfficer.BindDropDownData<UserModel>("Name", "UserId", "[Select Procurement Type]", UserList);
                ddlProcurementType.BindDropDownData<ProcurementTypeModel>("Type", "ID", "Select procurement Type", ProcurementTypeList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            (procurementDB, userDB, procurementTypeDB) = container.InitializeDependency<ProcurementDB, UserDB, ProcurementTypeDB>();
            ProcurementTypeList = await procurementTypeDB.ReadProcurementType("selectProcurementTypes");
            UserList = await userDB.ReadUser("selectUsersByRoleId");
        }

        protected async void OnSubmit(object sender, EventArgs e)
        {
            var officer = ddlOfficer.SelectedValue.ValidateString(this).ConvertStringTo<int>();
            var costCenter = txtCostCentre.Text.ValidateString(this);
            var description = txtDescription.Text.ValidateString(this);
            var mediumUsed = txtMediumUsed.Text.ValidateString(this);
            var lotProcurement = txtLotProcurement.Text.ValidateString(this);
            var comparativeEstimate = txtComparativeEstimate.Text.ValidateString(this).ConvertStringTo<decimal>();
            var actualContractValue = txtActualContractValue.Text.ValidateString(this).ConvertStringTo<decimal>();
            var procurementType = ddlProcurementType.SelectedValue.ValidateString(this).ConvertStringTo<int>();
            var recommendedSupplier = txtRecommendedSupplier.Text.ValidateString(this);
            var fitAndReadyDate = txtFitAndReadyDate.Text.ValidateString(this).StringToDate(this);
            var publicationDate = txtPublicationDate.Text.ValidateString(this).StringToDate(this);
            var dateTenderClosed = txtDateTenderClosed.Text.ValidateString(this).StringToDate(this);
            var dateEvaluationCompleted = txtDateEvaluationCompleted.Text.ValidateString(this).StringToDate(this);
            var dateReportSentToPPM = txtDateReportSentToPPM.Text.ValidateString(this).StringToDate(this);
            var resubmissionDateToPPM = txtResubmissionDateToPPM.Text.ValidateString(this).StringToDate(this);
            var dateReportSentToPC = txtDateReportSentToPC.Text.ValidateString(this).StringToDate(this);
            var pCApprovalDate = txtPCApprovalDate.Text.ValidateString(this).StringToDate(this);
            var dateApprovedByManagingDirector = txtDateApprovedByManagingDirector.Text.ValidateString(this).StringToDate(this);
            var externalApproval = txtExternalApproval.Text.ValidateString(this);
            var contractDateApproval = txtDateApproved.Text.ValidateString(this).StringToDate(this);
            var comments = txtComments.Text.ValidateString(this);

            var procurement = new ProcurementModel()
            {
                ProcurementOfficer = officer,
                UserId = UserID,
                CostCentre = costCenter,
                Description = description,
                DateOfRequest = DateTime.Now,
                MediumUsedToSendRequest = mediumUsed,
                LotProcurement = lotProcurement,
                ComparativeEstimate = comparativeEstimate,
                ActualContractValue = actualContractValue,
                ProcurementTypeId = procurementType,
                RecommendedSupplier = recommendedSupplier,
                FitAndReadyDate = fitAndReadyDate,
                PublicationDate = publicationDate,
                DateTenderClosed = dateTenderClosed,
                DateEvaluationCompleted = dateEvaluationCompleted,
                DateReportSentToPPM = dateReportSentToPPM,
                ResubmissionDateToPPM = resubmissionDateToPPM,
                DateReportSentToPC = dateReportSentToPC,
                PCApprovalDate = pCApprovalDate,
                DateApprovedByManagingDirector = dateApprovedByManagingDirector,
                ExternalApproval = externalApproval,
                DateContractApproved = contractDateApproval,
                Status = "Pending Approval",
                Comments = comments,
            };

            var procurementMade = await procurementDB.CreateProcurement("insertProcurementTracking", procurement);
            procurementMade.AlertSuccessORFail(this);
        }

    }
}