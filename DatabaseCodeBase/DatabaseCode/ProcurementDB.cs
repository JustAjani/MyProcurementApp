using DatabaseCodeBase.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseCodeBase.Model;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseCodeBase.DatabaseCode
{
    public class ProcurementDB : BaseDatabase, IProcurement<ProcurementModel>
    {
        public ProcurementDB(string connectionString) : base(connectionString) { }

        public async Task<string> CreateProcurement(string storedProcedure, ProcurementModel procurement)
        {
            string output = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProcurementOfficer", SqlDbType.Int) { Value = procurement.ProcurementOfficer });
                        cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = procurement.UserId });
                        cmd.Parameters.Add(new SqlParameter("@CostCentreId", SqlDbType.Int) { Value = procurement.CostCentreId });
                        cmd.Parameters.Add(new SqlParameter("@DateOfRequest", SqlDbType.Date) { Value = procurement.DateOfRequest });
                        cmd.Parameters.Add(new SqlParameter("@MediumUsedToSendRequest", SqlDbType.NVarChar, 255) { Value = procurement.MediumUsedToSendRequest });
                        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, -1) { Value = procurement.Description });
                        cmd.Parameters.Add(new SqlParameter("@LotProcurement", SqlDbType.NVarChar, 255) { Value = procurement.LotProcurement });
                        cmd.Parameters.Add(new SqlParameter("@ComparativeEstimate", SqlDbType.Decimal) { Value = procurement.ComparativeEstimate });
                        cmd.Parameters.Add(new SqlParameter("@ProcurementTypeId", SqlDbType.Int) { Value = procurement.ProcurementTypeId });
                        cmd.Parameters.Add(new SqlParameter("@FitAndReadyDate", SqlDbType.Date) { Value = procurement.FitAndReadyDate });
                        cmd.Parameters.Add(new SqlParameter("@PublicationDate", SqlDbType.Date) { Value = procurement.PublicationDate });
                        cmd.Parameters.Add(new SqlParameter("@DateTenderClosed", SqlDbType.Date) { Value = procurement.DateTenderClosed });
                        cmd.Parameters.Add(new SqlParameter("@DateEvaluationCompleted", SqlDbType.Date) { Value = procurement.DateEvaluationCompleted });
                        cmd.Parameters.Add(new SqlParameter("@DateReportSentToPPM", SqlDbType.Date) { Value = procurement.DateReportSentToPPM });
                        cmd.Parameters.Add(new SqlParameter("@ResubmissionDateToPPM", SqlDbType.Date) { Value = procurement.ResubmissionDateToPPM });
                        cmd.Parameters.Add(new SqlParameter("@DateReportSentToPC", SqlDbType.Date) { Value = procurement.DateReportSentToPC });
                        cmd.Parameters.Add(new SqlParameter("@PCApprovalDate", SqlDbType.Date) { Value = procurement.PCApprovalDate });
                        cmd.Parameters.Add(new SqlParameter("@DateApprovedByManagingDirector", SqlDbType.Date) { Value = procurement.DateApprovedByManagingDirector });
                        cmd.Parameters.Add(new SqlParameter("@DateReceivedApprovedSubmission", SqlDbType.Date) { Value = procurement.DateReceivedApprovedSubmission });
                        cmd.Parameters.Add(new SqlParameter("@DateContractReceivedFromCostCentre", SqlDbType.Date) { Value = procurement.DateContractReceivedFromCostCentre });
                        cmd.Parameters.Add(new SqlParameter("@DateContractSubmittedToLegal", SqlDbType.Date) { Value = procurement.DateContractSubmittedToLegal });
                        cmd.Parameters.Add(new SqlParameter("@DateContractApproved", SqlDbType.Date) { Value = procurement.DateContractApproved });
                        cmd.Parameters.Add(new SqlParameter("@RecommendedSupplier", SqlDbType.NVarChar, 255) { Value = procurement.RecommendedSupplier });
                        cmd.Parameters.Add(new SqlParameter("@ActualContractValue", SqlDbType.Decimal) { Value = procurement.ActualContractValue });
                        cmd.Parameters.Add(new SqlParameter("@ExternalApproval", SqlDbType.NVarChar, 3) { Value = procurement.ExternalApproval });
                        cmd.Parameters.Add(new SqlParameter("@DateSentToPurchasingUnit", SqlDbType.Date) { Value = procurement.DateSentToPurchasingUnit });
                        cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50) { Value = procurement.Status });
                        cmd.Parameters.Add(new SqlParameter("@Comments", SqlDbType.NVarChar, -1) { Value = procurement.Comments });
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) output = "Procurement Created Successfully";
                        else output = "Created Failed";
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return output;
        }


        public Task<string> DeleteProcurement(string storedProcedure, ProcurementModel procurement)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateProcurement(string storedProcedure, ProcurementModel procurement)
        {
            string output = string.Empty;
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand(storedProcedure,conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProcurementTrackingId", SqlDbType.Int) { Value = procurement.ProcurementTrackingId });
                        cmd.Parameters.Add(new SqlParameter("@ProcurementOfficer", SqlDbType.Int) { Value = procurement.ProcurementOfficer });
                        cmd.Parameters.Add(new SqlParameter("@ProcurementTypeId", SqlDbType.Int) { Value = procurement.ProcurementTypeId });
                        cmd.Parameters.Add(new SqlParameter("@CostCentreId", SqlDbType.Int) { Value = procurement.CostCentreId });
                        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500) { Value = procurement.Description });
                        cmd.Parameters.Add(new SqlParameter("@LotProcurement", SqlDbType.NVarChar, 50) { Value = procurement.LotProcurement });
                        cmd.Parameters.Add(new SqlParameter("@ComparativeEstimate", SqlDbType.Decimal) { Value = procurement.ComparativeEstimate });
                        cmd.Parameters.Add(new SqlParameter("@DateOfRequest", SqlDbType.Date) { Value = procurement.DateOfRequest });
                        cmd.Parameters.Add(new SqlParameter("@PublicationDate", SqlDbType.Date) { Value = procurement.PublicationDate });
                        cmd.Parameters.Add(new SqlParameter("@ActualContractValue", SqlDbType.Decimal) { Value = procurement.ActualContractValue });
                        cmd.Parameters.Add(new SqlParameter("@RecommendedSupplier", SqlDbType.NVarChar, 100) { Value = procurement.RecommendedSupplier });

                        await cmd.ExecuteNonQueryAsync();
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) output = "Procurement Update Successfully";
                        else output = "Update Failed";
                    }
                }
            }
            catch(SqlException ex)
            {
                OnQueryFail($"Error {ex.Message}");
            }
            catch(Exception ex)
            {
                OnQueryFail($"Error {ex.Message}");
            }
            return output;
        }

        public async Task<List<ProcurementModel>> ReadProcurement(string storedProcedure)
        {
            var procurementList = new List<ProcurementModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            var procurement = new ProcurementModel()
                            {
                                ProcurementTrackingId = (int)reader["ProcurementTrackingId"],
                                ProcurementOfficer = (int)reader["Procurement Officer"],
                                UserId = (int)reader["UserId"],
                                CostCentreId = (int)reader["CostCentreId"],
                                DateOfRequest = (DateTime)reader["Date of Request"],
                                MediumUsedToSendRequest = (string)reader["Medium used to send request"],
                                Description = (string)reader["Description"],
                                LotProcurement = (string)reader["Lot Procurement"],
                                ComparativeEstimate = (decimal)reader["Comparative Estimate"],
                                ProcurementTypeId = (int)reader["ProcurementTypeId"],
                                FitAndReadyDate = (DateTime)reader["Fit and Ready Date"],
                                PublicationDate = (DateTime)reader["Publication Date"],
                                DateTenderClosed = (DateTime)reader["Date Tender Closed"],
                                DateEvaluationCompleted = (DateTime)reader["Date Evaluation Completed"],
                                DateReportSentToPPM = (DateTime)reader["Date Report sent to PPM"],
                                ResubmissionDateToPPM = (DateTime)reader["Resubmission Date to PPM (If Applicable)"],
                                DateReportSentToPC = (DateTime)reader["Date report sent to PC"],
                                PCApprovalDate = (DateTime)reader["PC Approval Date"],
                                DateApprovedByManagingDirector = (DateTime)reader["Date approved by Managing Director"],
                                DateReceivedApprovedSubmission = (DateTime)reader["Date Received Approved Submission"],
                                DateContractReceivedFromCostCentre = (DateTime)reader["Date Contract received from Cost Centre (if Required)"],
                                DateContractSubmittedToLegal = (DateTime)reader["Date Contract Submitted to Legal"],
                                DateContractApproved = (DateTime)reader["Date Contract Approved (if Required)"],
                                RecommendedSupplier = (string)reader["Recommended Supplier"],
                                ActualContractValue = (decimal)reader["Actual Contract Value"],
                                ExternalApproval = (string)reader["External Approval (Yes/No)"],
                                DateSentToPurchasingUnit = (DateTime)reader["Date sent to Purchasing Unit"],
                                Status = (string)reader["Status"],
                                Comments = (string)reader["Comments"]
                            };
                            procurementList.Add(procurement);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return procurementList;
        }

        public async Task<List<ProcurementModel>> ReadByProcurementID(string storedProcedure, int iD, string selectedfield)
        {
            var procurmentList = new List<ProcurementModel>();
            var procurement = new ProcurementModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter(selectedfield, SqlDbType.Int) { Value = iD });
                        var reader = await cmd.ExecuteReaderAsync();
                        if (reader.Read())
                        {
                            procurement.ProcurementTrackingId = (int)reader["ProcurementTrackingId"];
                            procurement.ProcurementOfficer = (int)reader["Procurement Officer"];
                            procurement.UserId = (int)reader["UserId"];
                            procurement.CostCentreId = (int)reader["CostCentreId"];
                            procurement.DateOfRequest = (DateTime)reader["Date of Request"];
                            procurement.MediumUsedToSendRequest = reader["Medium used to send request"].ToString();
                            procurement.Description = reader["Description"].ToString();
                            procurement.LotProcurement = reader["Lot Procurement"].ToString();
                            procurement.ComparativeEstimate = (decimal)reader["Comparative Estimate"];
                            procurement.ProcurementTypeId = (int)reader["ProcurementTypeId"];
                            procurement.FitAndReadyDate = (DateTime)reader["Fit and Ready Date"];
                            procurement.PublicationDate = (DateTime)reader["Publication Date"];
                            procurement.DateTenderClosed = (DateTime)reader["Date Tender Closed"];
                            procurement.DateEvaluationCompleted = (DateTime)reader["Date Evaluation Completed"];
                            procurement.DateReportSentToPPM = (DateTime)reader["Date Report sent to PPM"];
                            procurement.ResubmissionDateToPPM = (DateTime)reader["Resubmission Date to PPM (If Applicable)"];
                            procurement.DateReportSentToPC = (DateTime)reader["Date report sent to PC"];
                            procurement.PCApprovalDate = (DateTime)reader["PC Approval Date"];
                            procurement.DateApprovedByManagingDirector = (DateTime)reader["Date approved by Managing Director"];
                            procurement.DateReceivedApprovedSubmission = (DateTime)reader["Date Received Approved Submission"];
                            procurement.DateContractReceivedFromCostCentre = (DateTime)reader["Date Contract received from Cost Centre (if Required)"];
                            procurement.DateContractSubmittedToLegal = (DateTime)reader["Date Contract Submitted to Legal"];
                            procurement.DateContractApproved = (DateTime)reader["Date Contract Approved (if Required)"];
                            procurement.RecommendedSupplier = reader["Recommended Supplier"].ToString();
                            procurement.ActualContractValue = (decimal)reader["Actual Contract Value"];
                            procurement.ExternalApproval = reader["External Approval (Yes/No)"].ToString();
                            procurement.DateSentToPurchasingUnit = (DateTime)reader["Date sent to Purchasing Unit"];
                            procurement.Status = reader["Status"].ToString();
                            procurement.Comments = reader["Comments"].ToString();

                            procurmentList.Add(procurement);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            return procurmentList;
        }
    }
}
