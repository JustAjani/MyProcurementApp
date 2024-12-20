﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminCreateRole.aspx.cs" Inherits="MyProcurementApp.AdminSection.AdminCreateRole" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Create Role</title>
    
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
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .role-creation-container {
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.1);
            padding: 30px;
            width: 100%;
            max-width: 400px;
        }

        .role-creation-header {
            text-align: center;
            margin-bottom: 25px;
            color: var(--primary-color);
        }

        .form-label {
            color: #333;
            font-weight: 500;
        }

        .form-control {
            border-color: var(--primary-color);
            transition: all 0.3s ease;
        }

        .form-control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.25rem rgba(52, 152, 219, 0.25);
        }

        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
            transition: all 0.3s ease;
            width: 100%;
        }

        .btn-primary:hover {
            background-color: #2980b9;
            border-color: #2980b9;
        }
    </style>

    <!-- SweetAlert2 -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="role-creation-container">
            <h2 class="role-creation-header">Create Role</h2>
            <div class="mb-3">
                <asp:Label ID="lblRole" runat="server" AssociatedControlID="Role" CssClass="form-label">Enter Role Name:</asp:Label>
                <asp:TextBox ID="Role" runat="server" CssClass="form-control" placeholder="Enter Role Name" />
            </div>
            <div class="mb-3">
                <asp:Button ID="RoleSubmit" runat="server" OnClick="OnRoleSubmit" Text="Create Role" CssClass="btn btn-primary" />
            </div>
        </div>
    </form>

    <!-- Bootstrap JS and Popper.js -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>
</body>
</html>