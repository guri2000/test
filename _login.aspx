<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login"  culture="en-CA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html lang="en">
<head>
        <title>Acumen Admin</title><meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
		<link rel="stylesheet" href="css/bootstrap.min.css" />
		<link rel="stylesheet" href="css/bootstrap-responsive.min.css" />
        <link rel="stylesheet" href="css/matrix-login.css" />
        <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
		<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700,800' rel='stylesheet' type='text/css'>

        <script src="jquery-1.10.2.min.js"></script>
        <script src="full-screen.js"></script>  
        <link rel="stylesheet" href="full-screen.css" />

        <style type="text/css">
        .TelerikModalOverlay{background-color: transparent !important;}
        .RadWindow{left: 15% !important; top: 30% !important;}
        </style>

        <style type="text/css">
.logo-stipe1{background:url(logo-stipe.png) no-repeat center left; width:879px; height:109px; position:absolute; top:1px;}
#loginbox form{background:none;}
.form-vertical, .form-actions{border:none !important;}
#loginbox .control-group{padding: 20px 0 0;}
#loginbox .main_input_box input{border: 1px solid rgba(0, 0, 0, 0.2);}
#loginbox .form-actions{padding: 0 20px 15px;}
.btn_login {
    background: #bd1920;
    color: #fff;
    font-size: 14px;
    border: 1px solid rgba(0, 0, 0, 0.1);
    cursor: pointer;
    padding: 8px 40px 8px 40px;
    margin: 10px 0px 10px 0px;
    border-radius: 3px;
    font-weight: 600;
    line-height: 20px;
}
.btn_login:hover, .btn_login:focus {
    background: #a10d13;
    color: #fff;
    box-shadow: 0 2px 5px 0px #333;
    text-decoration: underline;
}
</style>

    </head>
    <body>

    <div class="fullscreen background" style="background-image:url('login-bg1.jpg');" data-img-width="1600" data-img-height="1064">
    <div class="content-a">
        <div class="content-b">
            <div class="logo-stipe1"></div>

            <%--<div id="loginbox" style="background:url(login-box-bg.png) repeat-x center top; 
    border-left: 4px solid #ce0e0e;
    border-top: 4px solid #ce0e0e;
    bottom: 0;
    box-shadow: 15px 15px 22px rgba(0, 0, 0, 0.5);
    height: 315px;
    left: 0;
    margin: 10px;
    position: absolute;
    width: 490px;">   --%>
    <div id="loginbox" style="background: rgba(255, 255, 255, 0.8) none repeat scroll 0 0;
    border-left: 4px solid #bd1920;
    border-radius: 20px;
    border-top: 4px solid #bd1920;
    bottom: 0;
    box-shadow: 15px 15px 22px rgba(0, 0, 0, 0.5);
    height: 270px;
    left: 0;
    margin: 10px;
    position: absolute;
    width: 430px;">          
            <form id="loginform" runat="server" class="form-vertical"  style="padding-top: 50px;"  >
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
				 <%--<div class="control-group normal_text"> <h3><img src="img/acumen-logo2-white.png" alt="Logo" /></h3></div>--%>
                <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_lr"><i class="icon-user"></i></span> 
                            <asp:TextBox ID="TextBox1" placeholder="Username" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_lr" style="margin-right:4px;"><i class="icon-lock"></i></span><asp:TextBox ID="TextBox2" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-actions" style="margin-right: 24px;">
                    <span class="pull-left" style="display:none;"><a href="#" class="flip-link btn btn-info" id="to-recover">Lost password?</a></span>
                    <span class="pull-right">
                        <asp:Button ID="Button1" runat="server" class="btn_login" ToolTip="Click here to login" Text="Login" /></span>
                </div>
            </form>
            <form id="recoverform" action="#" class="form-vertical">
				<p class="normal_text">Enter your e-mail address below and we will send you instructions how to recover a password.</p>
				
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_lo"><i class="icon-envelope"></i></span><input type="text" placeholder="E-mail address" />
                        </div>
                    </div>
               
                <div class="form-actions">
                    <span class="pull-left"><a href="#" class="flip-link btn btn-success" id="to-login">&laquo; Back to login</a></span>
                    <span class="pull-right"><a class="btn btn-info"/>Reecover</a></span>
                </div>
            </form>
        </div>

        </div>

    </div>



</div>


        
        
        <script src="js/jquery.min.js"></script>  
        <script src="js/matrix.login.js"></script> 
    </body>
</html>
