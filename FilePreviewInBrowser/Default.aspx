<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Button ID="btnWord" runat="server" Text="WordPreivew" OnClick="btnWord_Click" />
        </div>
        <br />
        <div>
            <asp:Button ID="btnExel" runat="server" Text="ExcelPreivew" OnClick="btnExcel_Click" />
        </div>
        <br />
        <div>
            <asp:Button ID="btnPDF" runat="server" Text="PDFPreivew" OnClick="btnPDF_Click" />
        </div>
        <br />
        <div>
            <asp:Button ID="btnTxt" runat="server" Text="TxtPreivew" OnClick="btnTxt_Click" />
        </div>
    </div>
    </form>
</body>
</html>
