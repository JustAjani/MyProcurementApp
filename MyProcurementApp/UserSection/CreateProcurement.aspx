<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProcurement.aspx.cs" Inherits="MyProcurementApp.UserSection.CreateProcurement" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Create Procurement</title>
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
        }

        .procurement-header {
            color: var(--primary-color);
            margin-bottom: 25px;
            text-align: center;
        }

        .form-control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.2rem rgba(52, 152, 219, 0.25);
        }

        .form-section { display: none; }
        .form-section.active { display: block; }

        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
        }

        .btn-primary:hover {
            background-color: #2980b9;
            border-color: #2980b9;
        }

        .progress-bar {
            background-color: var(--primary-color);
            transition: width 0.3s ease;
        }

        .required::after {
            content: "*";
            color: red;
            margin-left: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="procurement-container">
                <h1 class="procurement-header">Create Procurement Request</h1>
                
                <div class="progress mb-4" style="height: 3px;">
                    <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                </div>

                <!-- Section 1: Basic Information -->
                <div class="form-section active" id="section1">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label required">Procurement Officer</label>
                            <asp:DropDownList ID="ddlOfficer" runat="server" CssClass="form-select" 
                                    AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label required">Cost Centre</label>
                            <asp:TextBox ID="txtCostCentre" runat="server" CssClass="form-control" Required="true" />
                        </div>
                        <div class="col-md-12">
                            <label class="form-label required">Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Required="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label required">Medium Used</label>
                            <asp:TextBox ID="txtMediumUsed" runat="server" CssClass="form-control" Required="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label required">Lot Procurement</label>
                            <asp:TextBox ID="txtLotProcurement" runat="server" CssClass="form-control" Required="true" />
                        </div>
                    </div>
                </div>

                <!-- Section 2: Estimates and Types -->
                <div class="form-section" id="section2">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label required">Comparative Estimate</label>
                            <asp:TextBox ID="txtComparativeEstimate" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Required="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label required">Actual Contract Value</label>
                            <asp:TextBox ID="txtActualContractValue" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Required="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label required">Procurement Type</label>
                            <asp:DropDownList ID="ddlProcurementType" runat="server" CssClass="form-select" Required="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Recommended Supplier</label>
                            <asp:TextBox ID="txtRecommendedSupplier" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </div>

                <!-- Section 3: Dates -->
                <div class="form-section" id="section3">
                    <div class="row g-3">
                        <div class="col-md-4">
                            <label class="form-label">Fit and Ready Date</label>
                            <asp:TextBox ID="txtFitAndReadyDate" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Publication Date</label>
                            <asp:TextBox ID="txtPublicationDate" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Tender Closing Date</label>
                            <asp:TextBox ID="txtDateTenderClosed" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Evaluation Completion Date</label>
                            <asp:TextBox ID="txtDateEvaluationCompleted" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Report to PPM Date</label>
                            <asp:TextBox ID="txtDateReportSentToPPM" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">PPM Resubmission Date</label>
                            <asp:TextBox ID="txtResubmissionDateToPPM" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                    </div>
                </div>

                <!-- Section 4: Approvals -->
                <div class="form-section" id="section4">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label">PC Report Date</label>
                            <asp:TextBox ID="txtDateReportSentToPC" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">PC Approval Date</label>
                            <asp:TextBox ID="txtPCApprovalDate" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Managing Director Approval Date</label>
                            <asp:TextBox ID="txtDateApprovedByManagingDirector" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">External Approval</label>
                            <asp:TextBox ID="txtExternalApproval" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-12">
                            <label class="form-label">Comments</label>
                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                </div>

                <div class="d-flex justify-content-between mt-4">
                    <button type="button" class="btn btn-secondary" id="prevBtn" style="display:none">Previous</button>
                    <button type="button" class="btn btn-primary" id="nextBtn">Next</button>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="OnSubmit" style="display:none" />
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        const sections = document.querySelectorAll('.form-section');
        const nextBtn = document.getElementById('nextBtn');
        const prevBtn = document.getElementById('prevBtn');
        const submitBtn = document.getElementById('btnSubmit');
        const progressBar = document.querySelector('.progress-bar');
        let currentSection = 0;

        function updateNavigation() {
            prevBtn.style.display = currentSection === 0 ? 'none' : 'block';
            nextBtn.style.display = currentSection === sections.length - 1 ? 'none' : 'block';
            submitBtn.style.display = currentSection === sections.length - 1 ? 'block' : 'none';
            progressBar.style.width = ((currentSection + 1) / sections.length * 100) + '%';
        }

        function showSection(index) {
            sections.forEach(section => section.classList.remove('active'));
            sections[index].classList.add('active');
            currentSection = index;
            updateNavigation();
        }

        nextBtn.addEventListener('click', () => {
            if (currentSection < sections.length - 1) showSection(currentSection + 1);
        });

        prevBtn.addEventListener('click', () => {
            if (currentSection > 0) showSection(currentSection - 1);
        });

        document.getElementById('form1').addEventListener('submit', function (e) {
            const activeSection = document.querySelector('.form-section.active');
            const requiredFields = activeSection.querySelectorAll('[required]');
            let isValid = true;

            requiredFields.forEach(field => {
                if (!field.value) {
                    isValid = false;
                    field.classList.add('is-invalid');
                } else {
                    field.classList.remove('is-invalid');
                }
            });

            if (!isValid) {
                e.preventDefault();
                alert('Please fill in all required fields');
            }
        });
    </script>
</body>
</html>