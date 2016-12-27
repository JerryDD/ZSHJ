<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户报告查询 报表</title>
    <link href="css/base.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">  
        <div id="title">中华人民共和国企业所得税年度纳税申报表(A类)</div>
        <div id="cName">公司名称：<span style="color:#005ea7;"><asp:Literal ID="litCompanyName" runat="server" /></span></div>
        <table class="reportTab"> 
        <tr><td style=" width:35">行次</td><td style=" width:35">类别</td><td style="width:630">项目</td><td  style="width:200">企业自报数</td></tr>
        <tr><td>1</td><td rowspan="13">利润<br />总额<br />计算</td><td class="pl3">一、营业收入(填写A101010\101020\103000)</td><td class="amount"><asp:Literal ID="lblAmount1" runat="server"/></td></tr>
        <tr><td>2</td><td class="pl30">减：营业成本(填写A102010\102020\103000)</td><td class="amount"><asp:Literal ID="lblAmount2" runat="server"/></td></tr>
        <tr><td>3</td><td class="pl60">营业税金及附加</td><td class="amount"><asp:Literal ID="lblAmount3" runat="server"/></td></tr>
        <tr><td>4</td><td class="pl60">销售费用(填写A104000)</td><td class="amount"><asp:Literal ID="lblAmount4" runat="server"/></td></tr>
        <tr><td>5</td><td class="pl60">管理费用(填写A104000)</td><td class="amount"><asp:Literal ID="lblAmount5" runat="server"/></td></tr>
        <tr><td>6</td><td class="pl60">财务费用(填写A104000)</td><td class="amount"><asp:Literal ID="lblAmount6" runat="server"/></td></tr>
        <tr><td>7</td><td class="pl60">资产减值损失</td><td class="amount"><asp:Literal ID="lblAmount7" runat="server"/></td></tr>
        <tr><td>8</td><td class="pl30">加：公允价值变动收益</td><td class="amount"><asp:Literal ID="lblAmount8" runat="server"/></td></tr>
        <tr><td>9</td><td class="pl60">投资收益</td><td class="amount"><asp:Literal ID="lblAmount9" runat="server"/></td></tr>
        <tr><td>10</td><td class="pl3">二、营业利润(1-2-3-4-5-6-7+8+9)	</td><td class="amount"><asp:Literal ID="lblAmount10" runat="server"/></td></tr>
        <tr><td>11</td><td class="pl30">加：营业外收入(填写A101010\101020\103000)</td><td class="amount"><asp:Literal ID="lblAmount11" runat="server"/></td></tr>
        <tr><td>12</td><td class="pl30">减：营业外支出(填写A102010\102020\103000)</td><td class="amount"><asp:Literal ID="lblAmount12" runat="server"/></td></tr>	        			
        <tr><td>13</td><td class="pl3">三、利润总额（10+11-12）</td><td class="amount"><asp:Literal ID="lblAmount13" runat="server"/></td></tr>
        <tr><td>14</td><td rowspan="10">应纳<br />税所<br />得额<br />计算</td><td class="pl30">减：境外所得（填写A108010）</td><td class="amount"><asp:Literal ID="lblAmount14" runat="server"/></td></tr>
        <tr><td>15</td><td class="pl30">加：纳税调整增加额（填写A105000）</td><td class="amount"><asp:Literal ID="lblAmount15" runat="server"/></td></tr>
        <tr><td>16</td><td class="pl30">减：纳税调整减少额（填写A105000）</td><td class="amount"><asp:Literal ID="lblAmount16" runat="server"/></td></tr>
        <tr><td>17</td><td class="pl30">减：免税、减计收入及加计扣除（填写A107010）</td><td class="amount"><asp:Literal ID="lblAmount17" runat="server"/></td></tr>
        <tr><td>18</td><td class="pl30">加：境外应税所得抵减境内亏损（填写A108000）</td><td class="amount"><asp:Literal ID="lblAmount18" runat="server"/></td></tr>
        <tr><td>19</td><td class="pl3">四、纳税调整后所得（13-14+15-16-17+18）</td><td class="amount"><asp:Literal ID="lblAmount19" runat="server"/></td></tr>
        <tr><td>20</td><td class="pl30">减：所得减免（填写A107020）</td><td class="amount"><asp:Literal ID="lblAmount20" runat="server"/></td></tr>
        <tr><td>21</td><td class="pl30">减：抵扣应纳税所得额（填写A107030）</td><td class="amount"><asp:Literal ID="lblAmount21" runat="server"/></td></tr>
        <tr><td>22</td><td class="pl30">减：弥补以前年度亏损（填写A106000）</td><td class="amount"><asp:Literal ID="lblAmount22" runat="server"/></td></tr>	        			
        <tr><td>23</td><td class="pl3">五、应纳税所得额（19-20-21-22）</td><td class="amount"><asp:Literal ID="lblAmount23" runat="server"/></td></tr>
        <tr><td>24</td><td rowspan="13">应纳<br />税额<br />计算</td><td class="pl30">税率（25%）</td><td class="amount"><asp:Literal ID="lblAmount24" runat="server"/></td></tr>
        <tr><td>25</td><td class="pl3">六、应纳所得税额（23×24）</td><td class="amount"><asp:Literal ID="lblAmount25" runat="server"/></td></tr>
        <tr><td>26</td><td class="pl30">减：减免所得税额（填写A107040）</td><td class="amount"><asp:Literal ID="lblAmount26" runat="server"/></td></tr>
        <tr><td>27</td><td class="pl30">减：抵免所得税额（填写A107050）</td><td class="amount"><asp:Literal ID="lblAmount27" runat="server"/></td></tr>
        <tr><td>28</td><td class="pl3">七、应纳税额（25-26-27）</td><td class="amount"><asp:Literal ID="lblAmount28" runat="server"/></td></tr>
        <tr><td>29</td><td class="pl30">加：境外所得应纳所得税额（填写A108000）</td><td class="amount"><asp:Literal ID="lblAmount29" runat="server"/></td></tr>
        <tr><td>30</td><td class="pl30">减：境外所得抵免所得税额（填写A108000）</td><td class="amount"><asp:Literal ID="lblAmount30" runat="server"/></td></tr>
        <tr><td>31</td><td class="pl3">八、实际应纳所得税额（28+29-30）</td><td class="amount"><asp:Literal ID="lblAmount31" runat="server"/></td></tr>
        <tr><td>32</td><td class="pl30">减：本年累计实际已预缴的所得税额</td><td class="amount"><asp:Literal ID="lblAmount32" runat="server"/></td></tr>	        			
        <tr><td>33</td><td class="pl3">九、本年应补（退）所得税额（31-32）</td><td class="amount"><asp:Literal ID="lblAmount33" runat="server"/></td></tr>
        <tr><td>34</td><td class="pl30">其中：总机构分摊本年应补（退）所得税额(填写A109000)	</td><td class="amount"><asp:Literal ID="lblAmount34" runat="server" Text="0.00"/></td></tr>
        <tr><td>35</td><td class="pl60">财政集中分配本年应补（退）所得税额（填写A109000）</td><td class="amount"><asp:Literal ID="lblAmount35" runat="server"  Text="0.00"/></td></tr>
        <tr><td>36</td><td class="pl60">总机构主体生产经营部门分摊本年应补（退）所得税额(填写A109000)</td><td class="amount"><asp:Literal ID="lblAmount36" runat="server"  Text="0.00"/></td></tr>
        <tr><td>37</td><td rowspan="2">附列<br />资料</td><td class="pl3">以前年度多缴的所得税额在本年抵减额</td><td class="amount"><asp:Literal ID="lblAmount37" runat="server"/></td></tr>
        <tr><td>38</td><td class="pl3">以前年度应缴未缴在本年入库所得税额</td><td class="amount"><asp:Literal ID="lblAmount38" runat="server"/></td></tr>
        </table>    	
    </div>
    </form>
</body>
</html>
