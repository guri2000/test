<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Complaint.aspx.vb" Inherits="Complaint"  ValidateRequest="false"  %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

   
<script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  <link rel="stylesheet" type="text/css" href="fancy/jquery.fancybox.css"
        media="screen" />
   <%-- <link href="css/popfo_style.css" rel="stylesheet" type="text/css" />--%>
<style type="text/css">
    body {
    margin: 0px;
    padding: 0px;
    background: #eeeeee !important;
    font-family: Arial, Helvetica, sans-serif;
    font-size: 11px;
    color: #272727;
}
.form_font
{
    font-family:Arial;
    font-size:12px;
    color:#666;
    float:left;
    
}
.form_height
{
    line-height:20px;
}
    .titleBg
        {
            background-image: url(images/title_bar_bg2.png);
            background-position: left bottom;
            background-repeat: repeat-x;
            height: auto;
            position: fixed;
            z-index: 5000;
            width: 100%;
        }
        .errcls
        {
            font:11px arial; background:rgba(158, 95, 13, 0.1); color:red; font-weight:600 !important; text-align:center; padding:10px 5px; width:85%; float:left; border:1px solid rgba(158, 95, 13, 0.1); margin:20px 10px; border-radius:4px;
        }
        .succls
        {
            font:11px arial; background:rgba(220, 242, 185, 0.1); color:green; font-weight:600 !important; text-align:center; padding:10px 5px; width:85%; float:left; border:1px solid rgba(220, 242, 185, 0.1); margin:20px 10px; border-radius:4px;
        }
        .zaxis-logo
        {
            float: right;
            height: 35px;
            margin-right: 0;
            margin-top: 0;
            padding: 0;
            width: 66px;
        }
        .fixedDataBg {
    -moz-border-bottom-colors: none;
    -moz-border-left-colors: none;
    -moz-border-right-colors: none;
    -moz-border-top-colors: none;
    background-color: #fcf3cf;
    border-color: #d6a45b -moz-use-text-color;
    border-image: none;
    border-style: solid none;
    border-width: 1px medium;
    padding: 3px;
    position: fixed;
    width: 100%;
}
.success_msg{background: #84ae40 none repeat scroll 0 0;
    border: 2px solid rgba(255, 255, 255, 0.1);
    border-radius: 6px;
    color: rgb(255, 255, 255);
    font: 300 14px arial;
    margin: 4px 8px;
    padding: 7px;
}

.error_msg{background: #BF5858 none repeat scroll 0 0;
    border: 2px solid rgba(255, 255, 255, 0.1);
    border-radius: 6px;
    color: rgb(255, 255, 255);
    font: 300 14px arial;
    margin: 4px 8px;
    padding: 7px;
}
.attched-sty{background: rgba(116, 220, 246, 0.06) none repeat scroll 0 0;
    border: 1px solid rgba(0, 0, 0, 0.04);
    border-radius: 5px;
    float: left;
    margin: 5px;
    padding: 7px 10px;}
    .attched-ctrl-ico{margin-bottom: -4px;
    margin-left: 7px;}
</style>
<script type="text/javascript">
    var myWindow;
    function closeWin() {
        myWindow.close();
    }
</script>
<style type="text/css">
.row-fluid [class*="span"]{margin-left:0;}
.row-fluid .span11{width:94%;}
.form-horizontal .control-label{padding-left: 10px; text-align: left;}
.form-horizontal .control-label{width: 120px;}
.form-horizontal .controls{margin-left: 130px;}
html body .RadInput_Default .riTextBox, html body .RadInputMgr_Default
{
font-size: 14px !important;
color: #555 !important;
font-family: "Helvetica Neue",Helvetica,Arial,sans-serif !important;
}


</style>

<style>
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}

.modal {
    position: fixed;
    top: 1%;
    left: 5%;
    right:5%;
    z-index: 1050;
    width: 90%;
    height:96%;
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.modal-body{max-height: 556px !important; padding: 0px !important;
overflow-y: hidden !important;}

</style>

 <script src="js/jquery.min.js" type="text/javascript"></script>
 <script type="text/javascript">
    /* $(document).ready(function () {
         $('.text').hide();
         $('.expander').click(function () {
             $('.text').slideToggle(300);
         });
     }); */
</script>
<script type="text/javascript">
    $(document).ready(function () {
    $('.text1').hide();
    $('.expander1').click(function () {
    $('.text1').slideToggle(300);
    });
    });
</script> 
</head>
<body>
<div class="container-fluid">
    <form id="form1" runat="server" class="form-horizontal">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
 
     <div class="container-fluid" style="min-width:800px; width:1100px;">
     <div class="row-fluid">
    <div class="span12">
        <div class="clear" style="padding-top:24px;"></div>
        <div class="widget-content nopadding">
    
         <%--<div class="expander" > <a class="arrow" style="text-decoration:none;" ><img title="Click here to Generate - New Ticket" src="images/Arrow_icon_3.png" style="margin-left: 24px;height: 17px;width: 18px;margin-bottom: 2px;" /><p title="Click here to Generate - New Ticket" style="text-decoration:none; font-family:Arial; color:#555; font-size:16px; margin-top:-20px; margin-left:50px; margin-bottom:15px;"><strong>New Ticket</strong></p> </a>
            </div> class="text" --%>
              <div class="clear" style="padding-top:5px;"></div>

             <table  width="100%" style="min-width: 650px; " cellpadding="0" cellspacing="0" border="0">
             
                <tr>
                    <td align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" style="font:11px arial;"  runat="server" ForeColor="Red" ShowMessageBox="True" ShowSummary="False" />
                       
                     <asp:Label ID="Label1" runat="server" style="font:11px arial;margin-top: -27px;" Text=""></asp:Label>

                    <table width="87%" cellpadding="0px" cellspacing="5px" align="left" border="0" style="margin-left:10px; margin-top: -10px;">
                        <tr>
                            <td width="10%" class="ac_label-text01 control-label form_font">From :</td>
                            
                            <td width="85%" class="ac_label-value01">
                                <asp:TextBox ID="sendfrom"  CssClass="textfieldstyle1-asp" style="font:12px arial;" Width="100%" Height="20px" runat="server"></asp:TextBox>
                            </td>
                            <td width="2%" class="ac_label-value01"> 
                    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ErrorMessage="From Address Not Entered" SetFocusOnError="True" ControlToValidate="sendfrom" 
                                            ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ErrorMessage="Invalid Email Address" ControlToValidate="sendfrom" 
                                            ForeColor="Red" SetFocusOnError="True" 
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                       <tr>
                            <td class="ac_label-text01 control-label form_font">To :</td>
                           
                            <td class="ac_label-value01">
                                    <asp:TextBox ID="TextsendTo" CssClass="textfieldstyle1-asp" style="font:12px arial;" Width="100%" Height="20px" runat="server"></asp:TextBox>
                            </td>
                            <td class="ac_label-value01"> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ErrorMessage="To Email cant be empty" ControlToValidate="TextsendTo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                    ErrorMessage="Invalid Email Address" ControlToValidate="TextsendTo" ForeColor="Red" SetFocusOnError="True" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                
                        <tr>
                            <td class="ac_label-text01 control-label form_font">CC :</td>
                            
                            <td class="ac_label-value01">
                                <asp:TextBox ID="CC1" CssClass="textfieldstyle1-asp" Width="25%" Height="20px" style="font:12px arial;" runat="server"></asp:TextBox>

                                <asp:TextBox ID="CC2" placeholder="Additional Email 1" CssClass="textfieldstyle1-asp" Width="24%" Height="20px" style="font:12px arial; 
                                                           " runat="server"></asp:TextBox>

                                <asp:TextBox ID="CC3" placeholder="Additional Email 2" CssClass="textfieldstyle1-asp" Width="23.5%" Height="20px" style="font:12px arial; 
                                                           " runat="server"></asp:TextBox>

                                <asp:TextBox ID="CC4" placeholder="Additional Email 3" CssClass="textfieldstyle1-asp" Width="23%" Height="20px" style="font:12px arial; 
                                                           " runat="server"></asp:TextBox>

                            </td>
                            <td class="ac_label-value01">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                    ErrorMessage="Invalid Email address" ControlToValidate="CC1" ForeColor="Red" SetFocusOnError="True" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                    ErrorMessage="Invalid Email address" ControlToValidate="CC2" ForeColor="Red"  SetFocusOnError="True" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                                    ErrorMessage="Invalid Email address" ControlToValidate="CC3" ForeColor="Red"  SetFocusOnError="True" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                    ErrorMessage="Invalid Email address" ControlToValidate="CC4" ForeColor="Red"  SetFocusOnError="True" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ac_label-text01 control-label form_font">Bcc :</td>
                        
                            <td class="ac_label-value01">
                                 <asp:TextBox ID="BCC" CssClass="textfieldstyle1-asp" Width="100%" Height="20px" style="font:12px arial;" runat="server"></asp:TextBox>
                            </td>
                            <td class="ac_label-value01">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                    ErrorMessage="Invalid Email address" ControlToValidate="BCC" ForeColor="Red" SetFocusOnError="True" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ac_label-text01 control-label form_font">Subject :</td>
                           
                            <td class="ac_label-value01">
                                <asp:TextBox ID="TextSubject" CssClass="textfieldstyle-asp" placeholder="Subject here" Width="100%" Height="20px" style="font:12px arial; 
                                    " runat="server"></asp:TextBox>
                            </td>
                            <td class="ac_label-value01"> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ErrorMessage="subject cant be empty" ControlToValidate="TextSubject" 
                                            ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
        
                        <tr>
                            <td class="ac_label-text01 control-label form_font">Type of Concern :</td>
                            
                            <td class="ac_label-value01">
                               
                                    <asp:DropDownList ID="ddl_Concern" runat="server" CssClass="textfieldstyle-asp"  Width="25%" Height="25px" style="font:12px arial;">
                                    
                                   
                                    </asp:DropDownList>
                            </td>
                            <td class="ac_label-value01"> 
                                    <asp:RequiredFieldValidator ControlToValidate="ddl_Concern" ID="RequiredFieldValidator4"
                                                     CssClass="errormesg" ErrorMessage="Please select type of concern"
                                                     ForeColor="Red" SetFocusOnError="true" InitialValue="0" runat="server">*
                                                    </asp:RequiredFieldValidator>
                            </td>

                        </tr>
               
                    </table>
                    <table width="12%" cellpadding="0" cellspacing="0" align="left" border="0">
                        <tr>
                            <td>
                                <div style="float:left; margin-top:-20px;margin-left: 26px;">
                                        <asp:ImageButton ID="btnsave" runat="server" ImageUrl="Images/send_icon.png" ToolTip="Send Mail" style="border-width:0px" Text="Save" /> 
                                        &nbsp;&nbsp;

                                        <asp:ImageButton ID="btndraft" CausesValidation="false"  runat="server" ImageUrl="Images/draft-mail-btn.png"  
                                                        style="border-width:0px" Text="Save Draft" visible="false" /> 
                                      
                                        <asp:ImageButton ID="btncancel" runat="server"  ImageUrl="Images/cance_icon.png"  CausesValidation="false"  
                                        style="border-width:0px; margin-left: 3px;" Text="Cancel" ToolTip="Cancel"  />
                                </div>
                            </td>
                        </tr>
                    </table>


                    </td>
           
                </tr>

                <tr >
                        <td style="padding-top:0px;">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-left:15px;">
                                <tr>
                                    <td style="padding-top:0px;" width="86%" class="control-label">
                                        <FTB:FreeTextBox  ID="FTB_Msg" Width="99%" Height="230" Focus="true" StartMode="HtmlMode" runat="server"></FTB:FreeTextBox>
                                    </td>
                                    <td valign="top" style="padding-top:1px;" width="14%">
                               
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                             
                                               
                                                <tr valign="top">
                                                    <td style="padding:3px 22px 5px 5px; text-align:center;">
                                                        <asp:ImageButton ID="imgbtn_AddAttachment" ImageUrl="~/images/attach_files.png" 
                                                            style="margin-top:-80px;height: 40px;width: 80px;" AlternateText="Add Attachment" ToolTip="Attach File" CausesValidation="false" runat="server" />
                                                    </td>
                                                    
                                                </tr>
                                

                                                <tr valign="top">
                                                    <td style="padding:0px 5px 5px 5px; line-height:20px;">
                                                    
                                                   
                                                           <asp:Panel ID="Panel2" style=" text-align:center; <%--background:rgba(116, 220, 246, 0.1)--%>; 
                                                                      min-height:100px; padding-top:6px; padding-top:43px;" Visible="false" runat="server">
                                                                      
                                                           <asp:FileUpload ID="FileUpload1" runat="server" style="font:9px arial; width:135px;"/>
                                                           <b>&nbsp;&nbsp;Attachment :</b> <span style="font:10px arial; color:#999;">(upto 20MB)</span>
                                                           <asp:ImageButton ID="imgbtn_Upload_attachment" ImageUrl="~/images/upload_btn.png" AlternateText="Upload Attachment" 
                                                                            style="margin:10px 0; margin-left:8px; width:80px; height:30px; float:left;" runat="server" CausesValidation="false"  />
                                                            </asp:Panel>
                                                            
                            
                                                    </td>
                                                </tr>
                                   
                                                <tr valign="top">
                                                    <td style="padding:0px 5px 5px 5px;">
                                                         

                                                         <asp:Panel ID="Panel1" Visible="true" runat="server" style="margin-top: 47px;">
                                                         <asp:Label ID="Label7" style="font:bold 11px arial;" Visible="false" runat="server"  Text="List of Attachments :"></asp:Label>
                                                             <asp:Label ID="Label6" runat="server"  Text=""></asp:Label>
                                                             
                                                              <asp:DataList ID="DataList1" runat="server" Width="100%">
                                                                    <ItemStyle CssClass="attched-sty" />
                                                                    <ItemTemplate >
                                                                        <%--<img src='<%#getimgicn(e %>' alt='<%#Eval("file") %>' width="40" height="40" />--%>
                                                                        <%#Eval("file") %>  
                                                                        <asp:ImageButton ID="imagbtn_AttachCancel" CssClass="attched-ctrl-ico" CausesValidation="false" 
                                                                                CommandName="delete" ImageUrl="~/images/cancel20x20.png" CommandArgument='<%#Eval("path") %>'
                                                                                runat="server" />   <br />
                                                                    </ItemTemplate>
                                                            </asp:DataList>
                                                            
                                                        </asp:Panel>
                                                   
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                   
                                    </td>
                                </tr>
                            </table>
                  
                        </td>
                    </tr>

                    
            </table>
        
           <div class="clear" style="padding-top:3px;"></div>
            <div class="expander1"> <a class="arrow" style="text-decoration:none;"> <img src="images/Arrow_icon_3.png" title="Click here to see - Old Tickets" style="margin-left: 24px;height: 17px;width: 18px;margin-bottom: 2px;"/> <p title="Click here to see - Old Tickets" style="text-decoration:none; font-family:Arial; color:#555; font-size:16px; margin-top:-23px; margin-left:50px; margin-bottom:15px;"><strong>Old Tickets</strong></p> </a>    
            </div>  
             <table  class="text1" width="100%" style="min-width: 650px; margin-top: 3px;display:none;" cellpadding="0" cellspacing="0" border="0">
                <tr >
                         <td style="padding-top:0px;">
                          <%--  <div id="Old_Tkts" runat="server" > groupingtext="Old Mails"--%>
                                    <asp:Panel ID="pnl_Old_Tkts" runat="server"  height="100%" style="margin-left: 22px;">
                            
                                    </asp:Panel>
                            <%--</div>--%>
                         </td>
                    </tr>
            </table>
            
            </div>
        </div>
        </div>
        </div>
    </form>
</div>
</body>
</html>
