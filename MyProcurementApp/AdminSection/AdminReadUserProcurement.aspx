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
            --border-color: #e0e0e0;
            --approval-color: #9b59b6;
            --contract-color: #e67e22;
            --status-color: #34495e;
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
            font-weight: bold;
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

        /* Base table styles */
        .table {
            background-color: white;
            border-radius: 8px;
            overflow: hidden;
            width: 100%;
            margin-bottom: 0;
            border-collapse: separate !important;
            border-spacing: 0;
        }

        .table th, .table td {
            padding: 15px;
            vertical-align: middle;
        }

        /* Basic Information Section */
        .table th:nth-child(-n+4),
        .table td:nth-child(-n+4) {
            background-color: rgba(52, 152, 219, 0.05);
            border-right: 2px solid var(--primary-color);
        }

        /* Request Details Section */
        .table th:nth-child(n+5):nth-child(-n+10),
        .table td:nth-child(n+5):nth-child(-n+10) {
            background-color: rgba(46, 204, 113, 0.05);
            border-right: 2px solid var(--secondary-color);
        }

        /* Tender Dates Section */
        .table th:nth-child(n+11):nth-child(-n+14),
        .table td:nth-child(n+11):nth-child(-n+14) {
            background-color: rgba(155, 89, 182, 0.05);
            border-right: 2px solid var(--approval-color);
        }

        /* Approval Dates Section */
        .table th:nth-child(n+15):nth-child(-n+20),
        .table td:nth-child(n+15):nth-child(-n+20) {
            background-color: rgba(230, 126, 34, 0.05);
            border-right: 2px solid var(--contract-color);
        }

        /* Contract & Status Section */
        .table th:nth-child(n+21),
        .table td:nth-child(n+21) {
            background-color: rgba(52, 73, 94, 0.05);
        }

        /* Header styling */
        .table thead th {
            background-color: var(--primary-color);
            color: white;
            font-weight: 600;
            position: sticky;
            top: 0;
            z-index: 1;
            white-space: nowrap;
        }

        /* Cell styles */
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

        .comments-cell {
            max-width: 300px;
            white-space: pre-line;
        }

        /* Status badge */
        .status-badge {
            padding: 5px 10px;
            border-radius: 15px;
            font-size: 0.9em;
            background-color: var(--secondary-color);
            color: white;
            display: inline-block;
        }

        /* Hover effect */
        .table-hover tbody tr:hover td {
            opacity: 0.9;
        }

        /* Section labels */
        .section-label {
            position: absolute;
            top: -20px;
            left: 0;
            padding: 2px 10px;
            background-color: var(--primary-color);
            color: white;
            border-radius: 4px;
            font-size: 0.8em;
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .table-container {
                max-height: none;
                overflow-x: auto;
            }
            
            .admin-container {
                padding: 15px;
                margin-top: 20px;
                margin-bottom: 20px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="admin-container">
                <h1 class="admin-header">Procurement Records</h1>
                <div class="mb-3"> 
                    <label for="txtSearchLot" class="form-label">Search by Lot Procurement</label> 
                    <asp:TextBox ID="txtSearchLot" runat="server" CssClass="form-control" /> 
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch_Click" />
                </div>
                <div class="table-container">
                    <asp:GridView ID="gvProcurements" runat="server" 
                        CssClass="table table-hover" 
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        PageSize="10"
                        OnRowDataBound="gvProcurements_RowDataBound">

                        <Columns>
                            <asp:BoundField DataField="ProcurementTrackingId" HeaderText="ID" Visible="false"/>
        
                            <asp:TemplateField HeaderText="Officer">
                                <ItemTemplate>
                                    <asp:Label ID="lblOfficer" runat="server" Text='<%# Eval("ProcurementOfficer") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
        
                            <asp:TemplateField HeaderText="User ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblUser" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cost Centre">
                                <ItemTemplate>
                                    <asp:Label ID="lblCostCentre" runat="server" CssClass="text-cell" Text='<%# Eval("CostCentreId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="DateOfRequest" HeaderText="Request Date" DataFormatString="{0:d}" ItemStyle-CssClass="date-cell" />
                            <asp:BoundField DataField="MediumUsedToSendRequest" HeaderText="Request Medium" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="LotProcurement" HeaderText="Lot" ItemStyle-CssClass="text-cell" />
                            <asp:BoundField DataField="ComparativeEstimate" HeaderText="Estimate" DataFormatString="{0:C}" ItemStyle-CssClass="currency-cell" />
        
                            <asp:TemplateField HeaderText="Type ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("ProcurementTypeId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

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

                            <asp:TemplateField HeaderText="Supplier">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier" runat="server" CssClass="text-cell" Text='<%# Eval("RecommendedSupplierID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

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