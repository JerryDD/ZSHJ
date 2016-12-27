<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyPassword.aspx.cs" Inherits="ModifyPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户报告查询</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/JavaScript">
        function Check() {
            var txtOldPwd = document.getElementById("txtOldPassword");
            var txtPwd = document.getElementById("txtNewPassword");
            var txtRePwd = document.getElementById("txtConfirmPassword");

            var str = "";
            var pattern = "";            if (txtOldPwd.value == txtPwd.value) {
                str += "新密码不能与旧密码相同,请重新输入。\n";
                txtPwd.focus();
            }
            if (txtPwd.value.length <= 5) {
                str += "密码长度不够，请输入6位或以上的密码。\n";
                txtPwd.focus();
            }
            if (txtPwd.value != txtRePwd.value) {
                str += "两次输入密码不一致。\n";
                txtRePwd.focus();
            }
            pattern = new RegExp(/^[\x00-\xff]*$/g);
            if (!pattern.test(txtPwd.value)) {
                str += "密码需由英文和数字或半角符号组成。\n";
                txtPwd.focus();
            }
            if (str != "") {
                alert(str);
                return false;
            }
            return true;
        }
    </script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />    
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .table {
            border: 0px #D1DDAA; /*边框颜色*/
            margin-top: 5px;
            margin-bottom: 5px;
            background: #D1DDAA;
        }

        .bg_tr {
            color: #584b23; /*标题字体色*/
            font-size: 12px;
            font-weight: bolder;
            background: #fdf3d0; /*标题背景色*/
            line-height: 22px;
        }
        .fontStyle {
            font-family: "Microsoft Yahei","Hiragino Sans GB","Helvetica Neue",Helvetica,tahoma,arial,"WenQuanYi Micro Hei",Verdana,sans-serif,"宋体";
            font-size: 12px;
        }
    </style>
</head>
<body leftmargin="8" topmargin="10" class="fontStyle">
    <form id="form1" runat="server" autocomplete="off">
        <table width="98%" align="center" border="0" cellpadding="4" cellspacing="1" bgcolor="#CBD8AC"
            style="margin-bottom: 8px;" class="table">
            <tr bgcolor="#EEF4EA">
                <td colspan="3" class="bg_tr" align="left">
                    <span>修改密码</span></td>
            </tr>
            <tr bgcolor="#FFFFFF">
                <td colspan="3">
                    <table width="400px" align="center" border="0" cellpadding="4" cellspacing="1" bgcolor="#CBD8AC"
                        style="margin-bottom: 8px" class="table">
                        <tr>
                            <td bgcolor="#FFFFFF" align="right" style="width: 120px; height: 23px;">原密码：
                            </td>
                            <td bgcolor="#FFFFFF" align="left">
                                <asp:TextBox runat="server" ID="txtOldPassword" TextMode="Password" BorderColor="#cccccc"
                                    ForeColor="#B00E00" BorderWidth="1px" Style="border: solid 1px #D1DDAA; height: 19px; background-color: #FFFFFF;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#FFFFFF" align="right" style="height: 23px;">新密码：
                            </td>
                            <td bgcolor="#FFFFFF" align="left">
                                <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" BorderColor="#cccccc"
                                    ForeColor="#B00E00" BorderWidth="1px" Style="border: solid 1px #D1DDAA; height: 19px; background-color: #FFFFFF;"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#FFFFFF" align="right" style="height: 23px;">新密码确认：
                            </td>
                            <td bgcolor="#FFFFFF" align="left">
                                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" BorderColor="#cccccc"
                                    ForeColor="#B00E00" BorderWidth="1px" Style="border: solid 1px #D1DDAA; height: 19px; background-color: #FFFFFF;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#FFFFFF" align="left" style="height: 23px;padding-left:40px;" colspan="2">密码需由6位英文或数字或半角符号组成
                            </td>
                        <tr>
                            <td bgcolor="#FFFFFF" colspan="2" style="text-align: center;">
                                <asp:Button runat="server" ID="btnUpdatePwd" Text="修改密码" Width="90px" Height="23px"
                                    BorderWidth="1px" BorderStyle="none" BorderColor="green" BackColor="#D1DDAA"
                                    Style="cursor: pointer;" OnClick="btnUpdatePwd_Click" OnClientClick="return Check();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
