<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户报告查询 进度</title>
    <link href="css/base.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
    <div class="m" id="orderstate">
    <div class="mt">
          <strong>         
          报告号：<asp:Literal ID="litBgh" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;状态：<span class="ftx14"><asp:Literal ID="litStatus" runat="server" /></span>          
		  </strong>
    </div>
    </div>
    <div runat="server" id="divProcess">
    <div class="process section4">
        <asp:Literal ID="lit1" runat="server" />
        <asp:Literal ID="lit2" runat="server" />
        <asp:Literal ID="lit3" runat="server" />
        <asp:Literal ID="lit4" runat="server" />
        <asp:Literal ID="lit5" runat="server" />
	</div>
	<div class="process section4">
	    <asp:Literal ID="lit6" runat="server" />
        <asp:Literal ID="lit7" runat="server" />
        <asp:Literal ID="lit8" runat="server" />
        <asp:Literal ID="lit9" runat="server" />       
    </div>
    </div>
<div class="m" id="ordertrack" runat="server">
  <ul class="tab">
    <li>
      <h2> 报告跟踪</h2>
    </li>
  </ul>
  <div class="clr"></div>
  <div class="mc tabcon">
    <!--报告跟踪-->
  <asp:Repeater ID="rptStatus" runat="server">
        <HeaderTemplate>
         <table cellpadding="0" cellspacing="0" width="100%">
          <tbody id="tbody_track">
            <tr>
              <th width="15%"><strong>处理信息</strong></th>
              <th width="50%"><strong>处理时间</strong></th>
              <th width="35%"><strong>操作人</strong></th>
            </tr>    
          </tbody>
        </HeaderTemplate>
       <ItemTemplate>
                    <tr>
                        <td><%#DataBinder.Eval(Container.DataItem,"环节名称")%></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"处理时间")%></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"处理信息")%></td>
                    </tr>
                </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
</div>
    </div>
    </form>
</body>
</html>
