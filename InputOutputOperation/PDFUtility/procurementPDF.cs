using DatabaseCodeBase.Model;
using InputOutputOperation.PdfInterface;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.UI;

namespace InputOutputOperation.PDFUtility
{
    public class ProcurementPDF : IPDF<ProcurementModel>
    {
        public Task CreatePDF(ProcurementModel model, Page page)
        {
            try
            {
                using (Document document = new Document(PageSize.A4, 10, 10, 10, 10))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfWriter.GetInstance(document, stream);

                        document.Open();

                        // Add title
                        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        document.Add(new Paragraph("Procurement Details", titleFont));
                        document.Add(new Paragraph("\n"));

                        // Create table
                        PdfPTable table = new PdfPTable(3); // 3 columns: Field, Value, Comments
                        table.WidthPercentage = 100;

                        // Set column widths
                        table.SetWidths(new float[] { 1.5f, 4f, 2.5f });

                        // Add headers
                        AddTableHeader(table);

                        // Add all fields dynamically
                        AddTableRows(table, model);

                        // Add table to document
                        document.Add(table);

                        document.Close();

                        // Return PDF as a response
                        byte[] bytes = stream.ToArray();
                        page.Response.ContentType = "application/pdf";
                        page.Response.AddHeader("content-disposition", "attachment;filename=ProcurementDetails.pdf");
                        page.Response.BinaryWrite(bytes);
                        page.Response.Flush();
                        page.Response.End();
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showalert", $"Swal.fire('Error', '{ex.Message}', 'error');", true);
                return Task.FromException(ex);
            }
        }

        private void AddTableHeader(PdfPTable table)
        {
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

            // Define headers
            string[] headers = { "Field", "Value", "Comments" };

            foreach (var header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, headerFont))
                {
                    BackgroundColor = BaseColor.DARK_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                table.AddCell(cell);
            }
        }

        private void AddTableRows(PdfPTable table, ProcurementModel model)
        {
            Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            // Add all fields
            AddDataRow(table, "Procurement ID", model.ProcurementTrackingId.ToString(), dataFont);
            AddDataRow(table, "Procurement Officer ID", model.ProcurementOfficer.ToString(), dataFont);
            AddDataRow(table, "User ID", model.UserId.ToString(), dataFont);
            //AddDataRow(table, "Cost Centre", model.CostCentreId, dataFont);
            AddDataRow(table, "Date of Request", model.DateOfRequest.ToShortDateString(), dataFont);
            AddDataRow(table, "Medium Used to Send Request", model.MediumUsedToSendRequest, dataFont);
            AddDataRow(table, "Description", model.Description, dataFont);
            AddDataRow(table, "Lot Procurement", model.LotProcurement, dataFont);
            AddDataRow(table, "Comparative Estimate", model.ComparativeEstimate.ToString("C"), dataFont);
            AddDataRow(table, "Procurement Type ID", model.ProcurementTypeId.ToString(), dataFont);
            AddDataRow(table, "Fit and Ready Date", model.FitAndReadyDate.ToShortDateString(), dataFont);
            AddDataRow(table, "Publication Date", model.PublicationDate.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Tender Closed", model.DateTenderClosed.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Evaluation Completed", model.DateEvaluationCompleted.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Report Sent to PPM", model.DateReportSentToPPM.ToShortDateString(), dataFont);
            AddDataRow(table, "Resubmission Date to PPM", model.ResubmissionDateToPPM.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Report Sent to PC", model.DateReportSentToPC.ToShortDateString(), dataFont);
            AddDataRow(table, "PC Approval Date", model.PCApprovalDate.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Approved by Managing Director", model.DateApprovedByManagingDirector.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Received Approved Submission", model.DateReceivedApprovedSubmission.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Contract Received from Cost Centre", model.DateContractReceivedFromCostCentre.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Contract Submitted to Legal", model.DateContractSubmittedToLegal.ToShortDateString(), dataFont);
            AddDataRow(table, "Date Contract Approved", model.DateContractApproved.ToShortDateString(), dataFont);
            //AddDataRow(table, "Recommended Supplier", model.RecommendedSupplierID, dataFont);
            AddDataRow(table, "Actual Contract Value", model.ActualContractValue.ToString("C"), dataFont);
            AddDataRow(table, "External Approval", model.ExternalApproval, dataFont);
            AddDataRow(table, "Date Sent to Purchasing Unit", model.DateSentToPurchasingUnit.ToShortDateString(), dataFont);
            AddDataRow(table, "Status", model.Status, dataFont);
            AddDataRow(table, "Comments", model.Comments, dataFont);
        }

        private void AddDataRow(PdfPTable table, string field, string value, Font font)
        {
            table.AddCell(new PdfPCell(new Phrase(field, font)) { Padding = 5 });
            table.AddCell(new PdfPCell(new Phrase(value, font)) { Padding = 5 });
            table.AddCell(new PdfPCell(new Phrase("", font)) { Padding = 5 }); // Empty for comments
        }
    }
}
