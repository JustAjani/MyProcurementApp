<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Procurement.aspx.cs" Inherits="MyProcurementApp.Procurement" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Procurement Dashboard</title>
    
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

        .dashboard-container {
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.1);
            padding: 30px;
            margin-top: 50px;
        }

        .welcome-header {
            color: var(--primary-color);
            margin-bottom: 25px;
            text-align: center;
        }

        .quick-actions {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin-top: 30px;
        }

        .quick-action-card {
            text-align: center;
            padding: 20px;
            border-radius: 8px;
            background-color: var(--background-color);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            width: 200px;
        }

        .quick-action-card:hover {
            transform: translateY(-10px);
            box-shadow: 0 10px 20px rgba(0,0,0,0.1);
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
                    <div class="quick-action-card">
                        <div class="quick-action-icon">📋</div>
                        <h4>Create Request</h4>
                        <p>Start a new procurement request</p>
                    </div>
                    <div class="quick-action-card">
                        <div class="quick-action-icon">📊</div>
                        <h4>View Reports</h4>
                        <p>Check procurement analytics</p>
                    </div>
                    <div class="quick-action-card">
                        <div class="quick-action-icon">🤝</div>
                        <h4>Vendor Management</h4>
                        <p>Manage your vendor list</p>
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