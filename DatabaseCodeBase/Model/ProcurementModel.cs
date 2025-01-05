using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.Model
{
    public class ProcurementModel
    {
        public int ProcurementTrackingId { get; set; }
        public int ProcurementOfficer { get; set; }
        public int UserId { get; set; }
        public int CostCentreId { get; set; }
        public string CostCentre { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string MediumUsedToSendRequest { get; set; }
        public string Description { get; set; }
        public string LotProcurement { get; set; }
        public decimal ComparativeEstimate { get; set; }
        public int ProcurementTypeId { get; set; }
        public DateTime FitAndReadyDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime DateTenderClosed { get; set; }
        public DateTime DateEvaluationCompleted { get; set; }
        public DateTime DateReportSentToPPM { get; set; }
        public DateTime ResubmissionDateToPPM { get; set; }
        public DateTime DateReportSentToPC { get; set; }
        public DateTime PCApprovalDate { get; set; }
        public DateTime DateApprovedByManagingDirector { get; set; }
        public DateTime DateReceivedApprovedSubmission { get; set; }
        public DateTime DateContractReceivedFromCostCentre { get; set; }
        public DateTime DateContractSubmittedToLegal { get; set; }
        public DateTime DateContractApproved { get; set; }
        public string RecommendedSupplier { get; set; }
        public decimal ActualContractValue { get; set; }
        public string ExternalApproval { get; set; }
        public DateTime DateSentToPurchasingUnit { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }

}
