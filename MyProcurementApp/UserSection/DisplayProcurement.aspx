﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayProcurement.aspx.cs" Inherits="MyProcurementApp.UserSection.DisplayProcurement" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Procurement Management</title>
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

        .procurement-container {
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.1);
            padding: 30px;
            margin-top: 50px;
            margin-bottom: 50px;
        }

        .procurement-header {
            color: var(--primary-color);
            margin-bottom: 25px;
            text-align: center;
        }

        .table {
            background-color: white;
            border-radius: 8px;
            overflow: hidden;
        }

        .table thead {
            background-color: var(--primary-color);
            color: white;
        }

        .form-control, .form-select {
            margin-bottom: 10px;
        }

        .date-field {
            width: 150px;
        }

        .currency-field {
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid procurement-container">
            <h2 class="procurement-header">Procurement Management</h2>
            
            <asp:GridView ID="gvProcurements" runat="server" 
                CssClass="table table-hover" 
                AutoGenerateColumns="false"
                AllowPaging="true"
                PageSize="10">
                <Columns>
                    <asp:BoundField DataField="ProcurementTrackingId" HeaderText="ID" Visible="true" />
                    
                    <asp:TemplateField HeaderText="Officer">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlOfficer" runat="server" 
                                CssClass="form-select"
                                DataValueField="ProcurementOfficer"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="OnOfficerChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlType" runat="server" 
                                CssClass="form-select"
                                DataValueField="ProcurementTypeId"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="OnTypeChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="CostCentre" HeaderText="Cost Centre" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="LotProcurement" HeaderText="Lot" />
                    
                    <asp:TemplateField HeaderText="Estimate">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEstimate" runat="server" 
                                CssClass="form-control currency-field"
                                Text='<%# Eval("ComparativeEstimate", "{0:C}") %>'>
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Key Dates">
                        <ItemTemplate>
                            <div class="date-controls">
                                <asp:TextBox ID="txtRequestDate" runat="server" 
                                    TextMode="Date" 
                                    CssClass="form-control date-field"
                                    Text='<%# Eval("DateOfRequest", "{0:yyyy-MM-dd}") %>'
                                    ToolTip="Request Date">
                                </asp:TextBox>
                                
                                <asp:TextBox ID="txtPublicationDate" runat="server" 
                                    TextMode="Date" 
                                    CssClass="form-control date-field"
                                    Text='<%# Eval("PublicationDate", "{0:yyyy-MM-dd}") %>'
                                    ToolTip="Publication Date">
                                </asp:TextBox>
                                
                                <asp:TextBox ID="txtClosingDate" runat="server" 
                                    TextMode="Date" 
                                    CssClass="form-control date-field"
                                    Text='<%# Eval("DateTenderClosed", "{0:yyyy-MM-dd}") %>'
                                    ToolTip="Closing Date">
                                </asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="RecommendedSupplier" HeaderText="Supplier" />
                    
                    <asp:TemplateField HeaderText="Contract Value">
                        <ItemTemplate>
                            <asp:TextBox ID="txtContractValue" runat="server" 
                                CssClass="form-control currency-field"
                                Text='<%# Eval("ActualContractValue", "{0:C}") %>'>
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" 
                                CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="OnStatusChanged">
                                <asp:ListItem Text="Draft" Value="Draft" />
                                <asp:ListItem Text="In Progress" Value="InProgress" />
                                <asp:ListItem Text="Under Review" Value="UnderReview" />
                                <asp:ListItem Text="Approved" Value="Approved" />
                                <asp:ListItem Text="Completed" Value="Completed" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" 
                                Text="Download Details" 
                                CssClass="btn btn-sm btn-primary"
                                OnCommand ="OnViewProcurement"
                                CommandName="ViewProcurement"
                                CommandArgument='<%# Eval("ProcurementTrackingId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination justify-content-center" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>