<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testZip.aspx.cs" Inherits="testZip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <asp:Button ID="btnGen" runat="server" Text="Gen" OnClick="btnGen_Click" />
        <a href="Zip/中盛汇缴[2015]第0452号.Zip" id="out">导出</a>   
        <asp:Button ID="Button1" runat="server" Text="Out" OnClick="btnOut_Click" />
    </div>
    </form>
</body>
</html>
