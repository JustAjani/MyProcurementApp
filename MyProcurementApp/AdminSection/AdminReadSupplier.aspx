﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminReadSupplier.aspx.cs" Inherits="MyProcurementApp.AdminSection.AdminReadSupplier" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Supplier Management</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
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
        }

        .admin-header {
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

        .table-hover tbody tr:hover {
            background-color: rgba(52, 152, 219, 0.1);
        }

        .btn-action {
            margin-right: 5px;
        }

        .btn-edit {
            background-color: var(--primary-color);
            color: white;
        }

        .btn-edit:hover {
            background-color: #2980b9;
            color: white;
        }

        .name-input {
            width: 100%;
            padding: 0.375rem 0.75rem;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }

        .name-input:focus {
            border-color: var(--primary-color);
            outline: 0;
            box-shadow: 0 0 0 0.2rem rgba(52, 152, 219, 0.25);
        }

        .status-checkbox {
            width: 20px;
            height: 20px;
            cursor: pointer;
        }

        .status-cell {
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="admin-container">
                <h1 class="admin-header">Supplier Management</h1>

                <asp:GridView ID="gvSuppliers" runat="server" 
                    CssClass="table table-hover" 
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="SupplierId" HeaderText="Supplier ID" Visible="false" />
                        <asp:TemplateField HeaderText="Supplier Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSupplierName" runat="server" 
                                    Text='<%# Eval("SupplierName") %>' 
                                    CssClass="name-input"
                                    placeholder="Enter supplier name">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active Status">
                            <ItemTemplate>
                                <div class="status-cell">
                                    <asp:CheckBox ID="chkActive" runat="server" 
                                        Checked='<%# Eval("IsActive") %>' 
                                        CssClass="status-checkbox"
                                        AutoPostBack="true" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" 
                                    Text="Edit" 
                                    CssClass="btn btn-sm btn-action btn-edit" 
                                    CommandName="EditSupplier" 
                                    CommandArgument='<%# Eval("SupplierId") %>' 
                                    OnCommand="OnEditCommand" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination justify-content-center" />
                </asp:GridView>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS and Popper.js -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>
</body>
</html>
