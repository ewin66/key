﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yaodianSearch3.aspx.cs"
    Inherits="DrectWeb.Search2.yaodian.yaodianSearch3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../css/style_search_subpage.css" type="text/css" />
    <style type="text/css">
        #search_logo
        {
            width: 270px;
            height: 75px;
            background: url(../images/logo_search.jpg) no-repeat center center;
            margin: 0 auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <a style="cursor: pointer" href="../default.aspx?type=sms">
            <div id="search_logo">
            </div>
        </a>
        <div style="width: 800px; border: 1px solid #ACC4E0; padding: 5px">
            <div class="posi_nav" style="background-color: #E1EEFF; width: 770px">
                <p>
                    您所在的位置：</p>
                <a href="../default.aspx?type=sms">医药搜索</a> <font style="float: left">&nbsp;&nbsp;说明书
                </font>
            </div>
            <table width="100%">
                <tr>
                    <td colspan="2">
                 </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TreeView ID="TreeView1" runat="server">
                        </asp:TreeView>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
