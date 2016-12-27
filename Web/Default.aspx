<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
    <script type="text/javascript" src="lib/html5.js"></script>
    <script type="text/javascript" src="lib/respond.min.js"></script>
    <script type="text/javascript" src="lib/PIE_IE678.js"></script>
    <![endif]-->
    <link href="css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="skin/default/skin.css" rel="stylesheet" type="text/css" id="skin" />
    <link href="lib/Hui-iconfont/1.0.1/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
    <script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
    <script>DD_belatedPNG.fix('*');</script>
    <![endif]-->
    <title>客户报告查询</title>
</head>
<body class="big-page">
    <form id="form1" runat="server" autocomplete="off">
    <header class="Hui-header cl"><font class="Hui-logo l">客户报告查询后台</font>        
        <ul class="Hui-Export">
            <li><input type="button" data-title="导出Excel" _href='ExcelDetail.aspx?type=2&ftype=1' onclick="Hui_admin_tab(this)" class="btn btn-primary radius"  value="导出Excel"/> </li>
            <li><input type="button" data-title="导出所有报告" _href='ExcelDetail.aspx?type=2&ftype=2' onclick="Hui_admin_tab(this)" class="btn btn-primary radius" value="导出所有报告"/> </li>
           <%-- <li><input type="button" onclick="window.open('/ExcelDetail.aspx?type=2&ftype=1')" class="btn btn-primary radius"  value="导出Excel"/> </li>
            <li><input type="button" onclick="window.open('/ExcelDetail.aspx?type=2&ftype=2')" class="btn btn-primary radius" value="导出所有报告"/></li>--%>
         </ul>                             
         <ul class="Hui-userbar">
		    <li>客户名称：</li>
		    <li class="dropDown dropDown_hover"><a href="#" class="dropDown_A"><%= CurrentUser==null?"":CurrentUser.UserName %> <i class="Hui-iconfont">&#xe6d5;</i></a>
			    <ul class="dropDown-menu radius box-shadow">
                    <li><asp:LinkButton runat="server" ID="btnUpdatePassword" Style="font-weight: lighter;color: #584b23;" Text="修改密码" data-title="修改密码" href="javascript:;"  _href='ModifyPassword.aspx' OnClientClick="Hui_admin_tab(this)"></asp:LinkButton></li>
				    <li><asp:LinkButton runat="server" ID="btnQuit" Style="font-weight: lighter;color: #584b23;" Text="退出" OnClick="btnQuit_Click"></asp:LinkButton></li>
			    </ul>
		    </li>
		
		<li id="Hui-skin" class="dropDown right dropDown_hover"><a href="javascript:;" title="换肤"><i class="Hui-iconfont" style="font-size:18px">&#xe62a;</i></a>
			<ul class="dropDown-menu radius box-shadow">
				<li><a href="javascript:;" data-val="default" title="默认（绿色）">默认（绿色）</a></li>
				<li><a href="javascript:;" data-val="blue" title="蓝色">蓝色</a></li>
				<li><a href="javascript:;" data-val="red" title="红色">红色</a></li>
				<li><a href="javascript:;" data-val="yellow" title="黄色">黄色</a></li>
				<li><a href="javascript:;" data-val="orange" title="橙色">橙色</a></li>
			</ul>
		</li>
	</ul>
	</header>
        <asp:Label runat="server" ID="lbMenu"></asp:Label>
        <div class="dislpayArrow">
            <a class="pngfix open" href="javascript:void(0);" onclick="displaynavbar(this)"></a>
        </div>
        <section class="Hui-article-box">
	        <div id="Hui-tabNav" class="Hui-tabNav">
		        <div class="Hui-tabNav-wp">
			        <ul id="min_title_list" class="acrossTab cl">
				        <li class="active"><span title="我的桌面" data-href="Default.aspx">我的桌面</span><em></em></li>
			        </ul>
		        </div>
		        <div class="Hui-tabNav-more btn-group"><a id="js-tabNav-prev" class="btn radius btn-default size-S" href="javascript:;"><i class="Hui-iconfont">&#xe6d4;</i></a><a id="js-tabNav-next" class="btn radius btn-default size-S" href="javascript:;"><i class="Hui-iconfont">&#xe6d7;</i></a></div>
	        </div>
	        <div id="iframe_box" class="Hui-article">
		        <div class="show_iframe">
			        <div style="display:none" class="loading"></div>
			        <iframe scrolling="yes" frameborder="0" src="Index.aspx"></iframe>
		        </div>
	        </div>
        </section>

        <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>

        <script type="text/javascript" src="lib/layer/1.9.3/layer.js"></script>

        <script type="text/javascript" src="js/H-ui.js"></script>

        <script type="text/javascript" src="js/H-ui.admin.js"></script>

    </form>
</body>
</html>
