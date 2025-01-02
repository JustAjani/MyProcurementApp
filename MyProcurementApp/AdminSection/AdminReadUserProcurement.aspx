<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminReadUserProcurement.aspx.cs" Inherits="MyProcurementApp.AdminSection.AdminReadUserProcurement" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>User Procurement Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        :root {
            --primary-color: #3498db;
            --secondary-color: #2ecc71;
            --background-color: #f4f6f7;
        }

        body {
            background-color: var(--background-color);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .admin-container {
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.1);
            padding: 30px;
            margin-top: 50px;
            margin-bottom: 50px;
        }

        .admin-header {
            color: var(--primary-color);
            margin-bottom: 25px;
            text-align: center;
        }

        .table-container {
            max-height: 600px;
            overflow-y: auto;
            scrollbar-width: thin;
            scrollbar-color: var(--primary-color) white;
        }

        .table-container::-webkit-scrollbar {
            width: 8px;
        }

        .table-container::-webkit-scrollbar-thumb {
            background-color: var(--primary-color);
            border-radius: 4px;
        }

        .table-container::-webkit-scrollbar-track {
            background-color: white;
            border-radius: 4px;
        }

        .table {
            background-color: white;
            border-radius: 8px;
            overflow: hidden;
            width: 100%;
            margin-bottom: 0;
            border-spacing: 0 15px; /* Adds spacing between rows */
            border-collapse: separate; /* Ensures spacing is applied */
        }

        .table th, .table td {
            padding: 20px 25px; /* Adds padding for a more spacious feel */
            text-align: left;
            vertical-align: middle;
        }

        .table thead {
            background-color: var(--primary-color);
            color: white;
            position: sticky;
            top: 0;
            z-index: 1;
        }

        .table-hover tbody tr:hover {
            background-color: rgba(52, 152, 219, 0.1);
        }

        .date-cell {
            white-space: nowrap;
        }

        .currency-cell {
            text-align: right;
            white-space: nowrap;
        }

        .text-cell {
            max-width: 200px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .status-badge {
            padding: 5px 10px;
            border-radius: 15px;
            font-size: 0.9em;
            background-color: var(--secondary-color);
            color: white;
        }

        .comments-cell {
            max-width: 300px;
            white-space: pre-line;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="admin-container">
                <h1 class="admin-header">Procurement Records</h1>
                <div class="table-container">
                    <asp:GridView ID="gvProcurements" runat="server" 
                        CssClass="table table-hover" 
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        PageSize="10">
                        <Columns>
                            <asp:BoundField DataField="ProcurementTrackingId" HeaderText="ID"  Visible="false"/>
                            <asp:BoundField DataField="ProcurementOfficer" HeaderText="Officer ID" />
                            <asp:BoundField DataField="UserId" HeaderText="User ID" />
                            <asp:BoundField DataField="CostCentre" HeaderText="Cost Centre" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="DateOfRequest" HeaderText="Request Date" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="MediumUsedToSendRequest" HeaderText="Request Medium" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="LotProcurement" HeaderText="Lot" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="ComparativeEstimate" HeaderText="Estimate" DataFormatString="{0:C}" ItemStyle-CssClass="currency-cell" />
                            <asp:BoundField DataField="ProcurementTypeId" HeaderText="Type ID" />
                            <asp:BoundField DataField="FitAndReadyDate" HeaderText="Ready Date" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="PublicationDate" HeaderText="Publication" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateTenderClosed" HeaderText="Tender Closed" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateEvaluationCompleted" HeaderText="Evaluation Done" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateReportSentToPPM" HeaderText="Sent to PPM" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="ResubmissionDateToPPM" HeaderText="Resubmit to PPM" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateReportSentToPC" HeaderText="Sent to PC" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="PCApprovalDate" HeaderText="PC Approval" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateApprovedByManagingDirector" HeaderText="MD Approval" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateReceivedApprovedSubmission" HeaderText="Submission Received" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateContractReceivedFromCostCentre" HeaderText="Contract From CC" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateContractSubmittedToLegal" HeaderText="Sent to Legal" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="DateContractApproved" HeaderText="Contract Approved" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="RecommendedSupplier" HeaderText="Supplier" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="ActualContractValue" HeaderText="Contract Value" DataFormatString="{0:C}" ItemStyle-CssClass="currency-cell" />
                            <asp:BoundField DataField="ExternalApproval" HeaderText="External Approval" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="DateSentToPurchasingUnit" HeaderText="To Purchasing" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <span class="status-badge"><%# Eval("Status") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-CssClass="comments-cell" />
                        </Columns>
                        <PagerStyle CssClass="pagination justify-content-center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>
</body>
</html>
