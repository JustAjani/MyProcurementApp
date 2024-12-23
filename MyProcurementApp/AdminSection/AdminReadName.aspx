<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminReadName.aspx.cs" Inherits="MyProcurementApp.AdminSection.AdminReadName" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>User Management</title>
    
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

        .name-input, .role-select {
            width: 100%;
            padding: 0.375rem 0.75rem;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }

        .name-input:focus, .role-select:focus {
            border-color: var(--primary-color);
            outline: 0;
            box-shadow: 0 0 0 0.2rem rgba(52, 152, 219, 0.25);
        }

        .status-checkbox {
            width: 1.2em;
            height: 1.2em;
            margin-right: 0.5em;
            vertical-align: middle;
        }

        .status-label {
            vertical-align: middle;
            margin-bottom: 0;
        }

        .role-section {
            margin-bottom: 20px;
        }
        .role-select {
            width: 200px;
        }

    </style>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="admin-container">
                <h1 class="admin-header">User Management</h1>
                
                <asp:GridView ID="gvUsers" runat="server" 
                    CssClass="table table-hover" 
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="User ID" Visible="false" />
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" 
                                    Text='<%# Eval("Name") %>' 
                                    CssClass="name-input"
                                    placeholder="Enter name">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="d-flex align-items-center gap-2">
                                    <asp:Button ID="btnEdit" runat="server" 
                                        Text="Edit Name" 
                                        CssClass="btn btn-sm btn-action btn-edit" 
                                        CommandName="EditUser" 
                                        CommandArgument='<%# Eval("UserID") %>' 
                                        OnCommand="OnEditCommand"/>
                                    

                                    <asp:DropDownList ID="ddlRole" runat="server" 
                                        CssClass="form-select form-select-sm role-select"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="OnRoleChanged">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnId4Role" runat="server" Value='<%# Eval("UserID") %>'/>

                                    
                                    <div class="form-check">
                                        <asp:CheckBox ID="chkActive" runat="server" 
                                            CssClass="status-checkbox"
                                            Checked='<%# Eval("Active") %>'
                                            AutoPostBack="true"
                                            OnCheckedChanged="OnStatusChanged" />
                                        <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# Eval("UserID") %>' />
                                        <label class="status-label">Active?</label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination justify-content-center" />
                </asp:GridView>
            </div>
        </div>  
    </form>
</body>
</html>