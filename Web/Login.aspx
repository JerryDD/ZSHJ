<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登陆</title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link href="css/signin.css" rel="stylesheet"/>   
</head>
<body >
    <div class="container">

      <form id="form1" runat="server" class="form-signin">
        <h2 class="form-signin-heading"  style="text-align:center; color:Gray; font-size: 35px; font-weight:bold;">客户报告查询</h2>
        <br />
        <br />
        <label for="inputEmail" class="sr-only">客户编码</label>
        <asp:TextBox id="txtEmail" runat="server" class="form-control" placeholder="客户编码" required="" autofocus="" />
       
        <br />
        <label for="inputPassword" class="sr-only" >密码</label>
        <asp:TextBox id="txtPassword" runat="server" class="form-control" placeholder="密码"  required="" TextMode="Password"/>
        <asp:Button ID="btnLogin" runat="server"  
            class="btn btn-lg btn-primary btn-block" Text="登录" onclick="btnLogin_Click" />
      </form>

    </div>
</body>
</html>

