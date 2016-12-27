<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <title>客户报告查询</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="lib/html5.js"></script>
    <script type="text/javascript" src="lib/respond.min.js"></script>
    <script type="text/javascript" src="lib/PIE_IE678.js"></script>
    <![endif]-->
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="lib/Hui-iconfont/1.0.7/iconfont.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
    <script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
    <script>DD_belatedPNG.fix('*');</script>
    <![endif]-->
</head>

<body>
    <form runat="server" id="form1">
    <div class="main">
    <asp:HiddenField ID="hdfCurentPage" runat="server" />
        <table align = "center" class="tab mt-30 text-l">       
         <tr>
            <td style="width:260px;">
            客户名称：<asp:Literal ID="lblUserName" runat="server" />  【<asp:Literal ID="lblUserID" runat="server" />】
            </td>
            <td colspan="2">&nbsp;</td>
            <td  style="width:140px;"><%--<asp:LinkButton ID="lbtnLogout" runat="server" Text ="登出" OnClick="lbtnLogout_Click" />--%></td>
        </tr>
        <tr>
        <td colspan="4" style="text-align:right; padding-right:15px; color:Red;">当前状态记录数：<asp:Literal ID="litRecordsCount" runat="server" /></td>
        </tr>
        <tr>
            <td>
                排序依据:
                <asp:DropDownList ID="ddlOrderBy" runat="server" Height="20" Width="120">
                    <asp:ListItem Text="请选择排序依据" Value="" />
                    <asp:ListItem Text="公司名称" Value="公司名称" />
                    <asp:ListItem Text="应交税" Value="应纳税额28" />
                    <asp:ListItem Text="已交税" Value="已缴税32" />
                    <asp:ListItem Text="免税" Value="减免所得税额26" />
                    <asp:ListItem Text="应补税" Value="应补税33" />
                    <asp:ListItem Text="当前订单存在的问题" Value="当前问题说明" />
                    <asp:ListItem Text="当前状态" Value="当前状态" />
                </asp:DropDownList>
                 <asp:DropDownList ID="ddlOrderDirection" runat="server"  Height="20" Width="60">
                    <asp:ListItem Text="升序" Value="ASC" />
                    <asp:ListItem Text="降序" Value="DESC" />               
                </asp:DropDownList>                
            </td>
           <td style="width:340px;">
               公司名称:
               <asp:TextBox ID="txtCompanyName" runat="server" Width="200"/>               
            </td>
            <td style="width:140px;">
                状态:			
                 <asp:DropDownList ID="ddlStatus" runat="server" Height="20">
                    <asp:ListItem Text="全部状态" Value="" />
                    <asp:ListItem Text="收集资料" Value="收集资料" />
                    <asp:ListItem Text="录入信息" Value="录入信息" />
                    <asp:ListItem Text="等待处理" Value="等待处理" />
                    <asp:ListItem Text="等待审核" Value="等待审核" />
                    <asp:ListItem Text="等待确认" Value="等待确认" Selected="True"/>
                    <asp:ListItem Text="等待上传" Value="等待上传" />
                    <asp:ListItem Text="等待打印" Value="等待打印" />
                    <asp:ListItem Text="等待出库" Value="等待出库" />
                    <asp:ListItem Text="报告完成" Value="报告完成" />
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text = "查询" OnClick="btnSearch_Click" CssClass="btn btn-primary radius"/>
                <asp:Button ID="btnRefresh" runat="server" Text = "刷新" OnClick="btnSearch_Click" CssClass="btn btn-primary radius"/>
            </td>
        </tr>
        </table>
        <br />
        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
                <HeaderTemplate>
                    <table align = "center" class="table table-border table-bordered table-bg table-hover" style="margin-top:20px;">                    
                    <tr class="head">                        
                        <td style="width:75px;">应交税</td>
                        <td style="width:75px;">已交税</td>
                        <td style="width:75px;">免税</td>
                        <td style="width:75px;">应补税</td>
                        <td  style="width:200px;">当前订单存在的问题</td>
                        <td  style="width:60px;">当前状态</td>
                        <td  style="width:350px;" colspan="2">请在此处录入您对我们审核结果的复核意见</td>
                        <td  style="width:80px;">查看报表</td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="tdGsmc" colspan="9">公司名称:<span class="gsmc"><%#DataBinder.Eval(Container.DataItem, "公司名称")%></span>&nbsp;&nbsp;&nbsp;<span class="gsbgh"><%#DataBinder.Eval(Container.DataItem, "报告号")%></span><asp:HiddenField ID="hdfBGH" runat="server" Value='<%# Eval("报告号") %>' /></td>
                    </tr>
                    <tr style="height:45px;">                      
                        <td style="text-align:right;"><%#DataBinder.Eval(Container.DataItem,"应纳税额28")%></td>
                        <td style="text-align:right;"><%#DataBinder.Eval(Container.DataItem,"已缴税32")%></td>
                        <td style="text-align:right;"><%#DataBinder.Eval(Container.DataItem,"减免所得税额26")%></td>
                        <td style="text-align:right; color:#005ea7;"><%#DataBinder.Eval(Container.DataItem,"应补税33")%></td>
                        <td style="text-align:left;" class="tdWordBreak"><%#DataBinder.Eval(Container.DataItem,"当前问题说明")%></td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem,"当前状态")%><br />
                            <asp:Button ID="btnUpdateStatus" runat="server" CommandArgument='<%# Eval("报告号") %>' CssClass="btn-primary radius" />
                        </td>
                        <td>               
                        <asp:TextBox ID="txtKHQRSM" runat="server" Text='<%# Eval("客户确认说明") %>' Width="299" Height="40" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                        <asp:ImageButton ID="btnUpdate" CommandArgument='<%# Eval("报告号") %>' CommandName="Update" runat="server" ImageUrl="~/images/bcyj.jpg" Width="35px" Height="40"/>                       
                        </td>
                        <td>   
                             <a data-title="查看进度" _href='Detail.aspx?bgh=<%# HttpUtility.UrlEncode(Eval("报告号").ToString(), Encoding.GetEncoding("utf-8"))%>' onclick="Hui_admin_tab(this)" href="javascript:;">查看进度</a><br />
                             <asp:Literal ID="litHref" runat="server" /> 
                        </td>                      
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </asp:Repeater>
             <tr>           
  		        <td height="25px" align="center" valign="middle" colspan="9">
  		        <asp:LinkButton id="lbtnFirst" runat="server" CommandName="First" OnCommand="lbtnPage_Command">首页</asp:LinkButton> 
                <asp:LinkButton id="lbtnPrevious" runat="server" CommandName="Previous" OnCommand="lbtnPage_Command">上一页</asp:LinkButton> 
                <asp:Label id="lblMessage" runat="server" /> 
                <asp:LinkButton id="lbtnNext" runat="server" CommandName="Next" OnCommand="lbtnPage_Command">下一页</asp:LinkButton> 
                <asp:LinkButton id="lbtnLast" runat="server" CommandName="Last" OnCommand="lbtnPage_Command">尾页</asp:LinkButton>  		      
	        </tr>
            </table>
    </div>

    <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>    
    <script type="text/javascript" src="lib/layer/2.1/layer.js"></script> 
    <script type="text/javascript" src="lib/datatables/1.10.0/jquery.dataTables.min.js"></script> 
    <script type="text/javascript" src="js/H-ui.js"></script> 
    <script type="text/javascript" src="js/H-ui.admin.js"></script>
    </form>
</body>
</html>
