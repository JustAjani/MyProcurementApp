<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="MyProcurementApp.AdminSection.Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Manager Dashboard</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <style>
        :root {
            --primary-color: #3498db;
            --secondary-color: #2ecc71;
            --background-color: #f4f6f7;
            --scrollbar-bg: #ddd;
            --scrollbar-thumb: #3498db;
            --scrollbar-thumb-hover: #2980b9;
        }

        body {
            background-color: var(--background-color);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            min-height: 100vh;
        }

        .dashboard-container {
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            padding: 30px 15px; /* Reduced horizontal padding to create more balance */
            margin-top: 50px;
            overflow-x: auto;
            white-space: nowrap;
            scrollbar-width: thin;
            scrollbar-color: var(--scrollbar-thumb) var(--scrollbar-bg);
            max-width: 100%; /* Prevents the container from overflowing */
            margin: auto; /* Centers the container on the page */
        }

        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
        }

        .dashboard-container::-webkit-scrollbar {
            height: 5px;
        }

        .dashboard-container::-webkit-scrollbar-track {
            background: var(--scrollbar-bg);
        }

        .dashboard-container::-webkit-scrollbar-thumb {
            background-color: var(--scrollbar-thumb);
            border-radius: 20px;
            border: 2px solid var(--scrollbar-bg);
        }

        .dashboard-container::-webkit-scrollbar-thumb:hover {
            background-color: var(--scrollbar-thumb-hover);
        }

        .welcome-header {
            color: var(--primary-color);
            margin-bottom: 25px;
            text-align: center;
        }

        .quick-actions {
            display: flex; /* Changed to Flexbox for better responsiveness */
            flex-wrap: nowrap; /* Prevents wrapping but ensures proportional alignment */
            gap: 20px;
            justify-content: flex-start; /* Align items to the start */
            overflow-x: auto; /* Allows horizontal scrolling for overflow */
            padding-bottom: 10px; /* Adds padding for scrolling comfort */
        }

        .quick-action-card {
            text-align: center;
            padding: 20px;
            border-radius: 8px;
            background-color: var(--background-color);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            width: 200px;
            flex: 0 0 auto; /* Ensures cards maintain their size and don't shrink */
            overflow: hidden;
        }

        .quick-action-card h4, .quick-action-card p {
            white-space: normal;
            overflow-wrap: break-word;
            word-wrap: break-word;
        }

        .quick-action-card:hover {
            transform: translateY(-10px);
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
        }

        .quick-action-icon {
            font-size: 2.5rem;
            color: var(--primary-color);
            margin-bottom: 15px;
        }
        
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="dashboard-container">
                <h1 class="welcome-header">
                    Welcome, <asp:Label ID="lblUserName" runat="server" Text="User"></asp:Label>!
                </h1>
                <div class="quick-actions">
                    <div class="quick-actions">
                        <div class="quick-action-card">
                            <div class="quick-action-icon">👤</div>
                            <h4>Manage Users</h4>
                            <p>Create, edit, or view users and their roles</p>
                            <asp:Button ID="btnManageUsers" runat="server" Text="User Management" CssClass="btn btn-primary" OnClick="RedirectToManageUsers" />
                        </div>

                        <div class="quick-action-card">
                            <div class="quick-action-icon">➕</div>
                            <h4>Create User</h4>
                            <p>Add a new user to the system</p>
                            <asp:Button ID="btnCreateUser" runat="server" Text="Create User" CssClass="btn btn-primary" OnClick="RedirectToCreateUser" />
                        </div>

                        <div class="quick-action-card">
                            <div class="quick-action-icon">📂</div>
                            <h4>Create Procurement</h4>
                            <p>Start a new procurement process</p>
                            <asp:Button ID="btnCreateProcurement" runat="server" Text="Create Procurement" CssClass="btn btn-primary" OnClick="RedirectToCreateProcurement" />
                        </div>

                        <div class="quick-action-card">
                            <div class="quick-action-icon">📜</div>
                            <h4>View Procurements</h4>
                            <p>Check all procurement requests</p>
                            <asp:Button ID="btnViewProcurements" runat="server" Text="View Procurements" CssClass="btn btn-primary" OnClick="RedirectToViewProcurements" />
                        </div>

                        <div class="quick-action-card">
                            <div class="quick-action-icon">➕</div>
                            <h4>Procurement Types</h4>
                            <p>Create procurement types</p>
                            <asp:Button ID="btnManageTypes" runat="server" Text="Manage Types" CssClass="btn btn-primary" OnClick="RedirectToManageTypes" />
                        </div>

                        <div class="quick-action-card">
                            <div class="quick-action-icon">📊</div>
                            <h4>View Procurement Types</h4>
                            <p>View all procurement types</p>
                            <asp:Button ID="btnViewTypes" runat="server" Text="View Types" CssClass="btn btn-primary" OnClick="RedirectToViewTypes" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS and Popper.js -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>
</body>

</html>
