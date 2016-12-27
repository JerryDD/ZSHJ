<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelDetail.aspx.cs" Inherits="ExcelDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<script type="text/javascript" src="lib/layer/1.9.3/layer.js"></script>
<script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
<script type="text/javascript" src="js/H-ui.js"></script>
<script type="text/javascript">
    function Hui_Menu_Change(cName, rName, htmlClientFolderName) {
    var initDivHTML = "<dl id=\"menu-Report\">" +
            "<dt class=\"selected\"><i class=\"Hui-iconfont\">&#xe616;</i> COMPANY_NAME<i class=\"Hui-iconfont menu_dropdown-arrow\">&#xe6d5;</i></dt>" +
            "<dd style=\"display: block;\">" +
            "<ul><li><a data-title=\"1基础信息\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A01&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;1基础信息</a></li>" +
            "<li><a data-title=\"2申报主表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A02&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;2申报主表</a></li>" +
            "<li><a data-title=\"3收入\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A03&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;3收入</a></li>" +
            "<li><a data-title=\"4成本支出\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A04&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;4成本支出</a></li>" +
            "<li><a data-title=\"5期间费用\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A05&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;5期间费用</a></li>" +
            "<li><a data-title=\"6纳税调整\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A06&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;6纳税调整</a></li>" +
            "<li><a data-title=\"7视同销售\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A07&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;7视同销售</a></li>" +
            "<li><a data-title=\"8职工薪酬\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A08&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;8职工薪酬</a></li>" +
            "<li><a data-title=\"9广告费\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A09&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;9广告费</a></li>" +
            "<li><a data-title=\"10折旧摊销\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A10&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;10折旧摊销</a></li>" +
            "<li><a data-title=\"11资产损失\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A11&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;11资产损失</a></li>" +
            "<li><a data-title=\"12弥补亏损\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A12&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;12弥补亏损</a></li>" +
            "<li><a data-title=\"13免税减计\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A13&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;13免税减计</a></li>" +
            "<li><a data-title=\"14所得减免\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A14&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;14所得减免</a></li>" +
            "<li><a data-title=\"15抵扣所得额\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A15&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;15抵扣所得额</a></li>" +
            "<li><a data-title=\"16减免所得税\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A16&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;16减免所得税</a></li>" +
            "<li><a data-title=\"17税额抵免\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=A17&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;17税额抵免</a></li>" +		
            "<li><a data-title=\"关联关系表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=B01&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;关联关系表</a></li>" +
            "<li><a data-title=\"关联交易汇总表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=B02&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;关联交易汇总表</a></li>" +
            "<li><a data-title=\"购销表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=B03&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;购销表</a></li>" +
            "<li><a data-title=\"劳务表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=B04&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;劳务表</a></li>" +
            "<li><a data-title=\"无形资产表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=B05&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;无形资产表</a></li>" +
            "<li><a data-title=\"固定资产表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=B06&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;固定资产表</a></li>" +
            "<li><a data-title=\"资产表负债表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=C01&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;资产表负债表</a></li>" +
            "<li><a data-title=\"利润表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=C02&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;利润表</a></li>" +
            "<li><a data-title=\"现金流量表主表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=C03&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;现金流量表主表</a></li>" +
            "<li><a data-title=\"现金流量表附表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=C04&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;现金流量表附表</a></li>" +
            "<li><a data-title=\"所有者权益变动表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=C05&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;所有者权益变动表</a></li>" +
            "<li><a data-title=\"科目余额表\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=D01&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;科目余额表</a></li>" +
            "<li><a data-title=\"记账凭证\" href=\"javascript:void(0)\" _href=\"ExcelDetail.aspx?Pid=D02&ReportName=REPORT_NAME\" onclick=\"Hui_admin_tab(this)\">&nbsp;&nbsp;&nbsp;记账凭证</a></li>" +
            "</ul></dd></dl>";
    initDivHTML = initDivHTML.replace(/COMPANY_NAME/g, cName).replace(/REPORT_NAME/g, encodeURIComponent(rName));    
    var topWindow = $(window.parent.document);
    var cMenuReport = topWindow.find('#menu-Report');
    cMenuReport.replaceWith(initDivHTML);
    var aside = topWindow.find('.Hui-aside');
    var mainBody = topWindow.find('body');
    mainBody.removeClass("big-page");
    var dislpayArrow = topWindow.find('.dislpayArrow .pngfix ');
    if (dislpayArrow.hasClass("open"))
    {
        dislpayArrow.removeClass("open");
    }    
    var htmlClientFolderName = rName.replace("[", "_").replace("]", "_");    
    window.location.href = "HTML/" + htmlClientFolderName + "/A02.html"; 
    /*左侧菜单
    $.Huifold(".menu_dropdown dl dt", ".menu_dropdown dl dd", "fast", 1, "click");*/
    /*选项卡导航
    $(".Hui-aside").on("click", ".menu_dropdown a", function () {
        Hui_admin_tab(this);
    });*/
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>

    </form>
</body>
</html>
