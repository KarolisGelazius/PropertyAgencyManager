<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form1.aspx.cs" Inherits="U4_18.Form1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NT Agentūra</title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="page-wrapper">
            <div class="card">
                <asp:Label ID="LabelFile" runat="server" Text="Pasirinkite duomenų failus:" CssClass="input-label" />
                
                <div class="upload-section">
                    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" CssClass="file-input" />
                </div>

                <asp:Label ID="LabelError" runat="server" CssClass="error-text" />

                <div class="button-container">
                    <asp:Button ID="Button1" runat="server" Text="Vykdyti" OnClick="Button1_Click" CssClass="primary-button" />
                </div>

                <asp:Label ID="LabelInitial" runat="server" Text="Pradiniai duomenys" CssClass="section-title" Visible="false" />
                <asp:Table ID="Table1" runat="server" CssClass="data-table" Visible="false"></asp:Table>

                <asp:Label ID="LabelResult1" runat="server" Text="Populiariausi tipai" CssClass="section-title" Visible="false" />
                <asp:Table ID="Table2" runat="server" CssClass="data-table" Visible="false"></asp:Table>

                <asp:Label ID="LabelResult2" runat="server" Text="Sugeneruoti failai (CSV)" CssClass="section-title" Visible="false" />
                <asp:BulletedList ID="BulletedList1" runat="server" CssClass="result-list" DisplayMode="HyperLink" Visible="false"></asp:BulletedList>
            </div>
        </div>
    </form>
</body>
</html>