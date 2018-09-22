<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="bdc_inv_Agrpop.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<%-- <script language="javascript">

     function DisableRightClick(event) {

         //For mouse right click

         if (event.button == 2) {

             alert("Right Clicking not allowed!");

         }

     }

     function DisableCtrlKey(e) {

         var code = (document.all) ? event.keyCode : e.which;

         var message = "Ctrl key functionality is disabled!";

         // look for CTRL key press

         if (parseInt(code) == 17) {

             alert(message);

             window.event.returnValue = false;

         }

     }

    </script>--%>

 <script type="text/javascript">
<!--
     function textBoxOnBlur(elementRef) {
         var checkValue = new String(elementRef.value);
         var newValue = '';

         // 1<2,3>4&56789
         for (var i = 0; i < checkValue.length; i++) {
             var currentChar = checkValue.charAt(i);

             if ((currentChar != '<') && (currentChar != '=') && (currentChar != '>') && (currentChar != '&') && (currentChar != '~') && (currentChar != '`') && (currentChar != '!') && (currentChar != '^') && (currentChar != '(') && (currentChar != ')') && (currentChar != '_') && (currentChar != '{') && (currentChar != '}') && (currentChar != ':') && (currentChar != ';') && (currentChar != '""') && (currentChar != '|') && (currentChar != '?') && (currentChar != '[') && (currentChar != ']'))
                 newValue += currentChar;
         }

         elementRef.value = newValue;
     }
     // -->

     function validate(elementRef) {
         var TCode = elementRef.value;

         if (/[^a-zA-Z0-9\-\@\/]/.test(TCode)) {
             alert(' ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? these Special Characters are not allowed');
             var checkValue = new String(elementRef.value);
             var newValue = '';

             // 1<2,3>4&56789
             for (var i = 0; i < checkValue.length; i++) {
                 var currentChar = checkValue.charAt(i);

                 if ((currentChar != '<') && (currentChar != '=') && (currentChar != '>') && (currentChar != '&') && (currentChar != '~') && (currentChar != '`') && (currentChar != '!') && (currentChar != '^') && (currentChar != '(') && (currentChar != ')') && (currentChar != '_') && (currentChar != '{') && (currentChar != '}') && (currentChar != ':') && (currentChar != ';') && (currentChar != '""') && (currentChar != '|') && (currentChar != '?') && (currentChar != '[') && (currentChar != ']') && (currentChar != '#') && (currentChar != '$') && (currentChar != '^') && (currentChar != '*') && (currentChar != '%'))
                     newValue += currentChar;
             }

             elementRef.value = newValue;
             return false;
         }

         return true;
     }
</script>


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css">
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.btn-cus {
    background: #bd1920;
    color: #fff;
    font-size: 12px;
    border: 1px solid rgba(0, 0, 0, 0.1);
    cursor: pointer;
    padding: 1px 10px 1px 10px;
    margin: 3px 3px 3px 3px;
    border-radius: 3px;
    font-weight: 600;
    line-height: 20px;
}
.btn-cus:hover, .btn-cus:focus {
    background: #a10d13;
    color: #fff;
    box-shadow: 0 2px 5px 0px #333;
    text-decoration: underline;
}
</style>

<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  
  <script type="text/javascript">
      function ShowPopup() {
         // alert("fired");
          $(document).ready(function () {
              $("#btnShowPopup1").click();
          });
      }
    </script>
   
    <script type="text/javascript">
        function Validate() {
            if (Page_ClientValidate()) {
                return true;
            }
            return false;
        }
</script>


<script type="text/javascript">
    //    $(function () {
    //        $(".close").on('click', function () {

    //            $('#addnewrecord').modal('hide');
    //            window.location.reload(true);


    //        });
    //    });
    function openModal() {
        $('#addnewpop1').modal('show');
    }
</script>

    <div class="container-fluid">

      <form  runat="server" method="post" class="form-horizontal" >
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
              <telerik:RadWindowManager ID="RadWindowManager1" runat="server" RenderMode="Lightweight" Skin="Windows7" DestroyOnClose ="true" EnableShadow="true" AutoSize="true" >
    </telerik:RadWindowManager>
     <asp:ValidationSummary ID="vs" ValidationGroup="basicdetails1" ShowMessageBox="true" ShowSummary="false" runat="server" />



  <div class="row-fluid" style="display:none;">
    <div class="span12">
<span id="error" style="color: Red; display: none"> ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? Special Characters not allowed</span>
</div></div>
<div class="row-fluid">
    <div class="span12">
     <div class="widget-box" style="margin-top: 10px;">
        <div class="widget-title" style="margin-top: 0px;"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Candidate-Agreement Info</h5>   
             <div class="" style="float: right;margin: 0px 10px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">            
               <asp:Button ID="Button1" Enabled="false" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:Button  ID="ImageButton1"  Enabled="true"   runat="server" CssClass="btn-cus" Text="Generate Agreement" ToolTip ="Generate Agreement"  OnClientClick="if(Page_ClientValidate('basicdetails')) ShowProgress();" />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" style="vertical-align: middle;" />

            </div>      
    </div>

     <div class="row-fluid widget-content" style="padding-bottom: 10px;">
          <div class="span4">
               <div class="span12">
                    <div class="control-group">
                           <div class="span12" style="margin-left: 0px; margin-top: 5px;">
                                <div class="control-group">
                                     <label class="control-label" style="width: 100px;">BDC Name:</label>
                                     <div class="controls" style="margin-left: 110px;">
                                           <asp:TextBox ID="txtBname"    onkeypress="return IsAlphaNumeric(event);"   class="span12" placeholder="Witness Name" runat="server"
                                                 MaxLength="60" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                           <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                                               runat="server"  InitialValue=""  ErrorMessage="BDC Name is required" ControlToValidate="txtBname" 
                                                  ForeColor="Red" Display="Dynamic" ValidationGroup="basicdetails"></asp:RequiredFieldValidator>
                                     </div>
                                </div>
                           </div>
                           <div class="span12" style="margin-left: 0px; margin-top: 5px;">
                               <div class="control-group" style="margin-left: 0px; margin-top: 5px;">
                                     <label class="control-label" style="width: 100px;">BDC Report to: </label>
                                     <div class="controls" style="margin-left: 110px;">
                                           <asp:DropDownList ID="ddl_Report" AppendDataBoundItems="true" class="span12"  runat="server">
                                           <asp:ListItem Value="" >Choose Sub Category</asp:ListItem>
                                          </asp:DropDownList>
                                            <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                                          runat="server"  InitialValue=""  ErrorMessage="Choose BDC Report to" ControlToValidate="ddl_Report" 
                                            ForeColor="Red" Display="Dynamic" ValidationGroup="basicdetails"> </asp:RequiredFieldValidator>
                                     </div>
                                </div>
                           </div>
                           <div class="span12" style="margin-left: 0px; margin-top: 5px;">
                               <div class="control-group">
                                     <label class="control-label" style="width: 100px;">BDC Address:</label>
                                     <div class="controls" style="margin-left: 110px;">
                                           <asp:TextBox ID="txtBAdd"   TextMode="MultiLine"  onkeypress="return IsAlphaNumeric(event);"  style="height: 80px; resize:none;"   class="span12" placeholder="BDC Address" runat="server"
                                                 MaxLength="1000" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                           <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationposition"
                                               runat="server"  InitialValue=""  ErrorMessage="BDC Address is required" ControlToValidate="txtBAdd" 
                                                  ForeColor="Red" Display="Dynamic"  ValidationGroup="basicdetails"></asp:RequiredFieldValidator>
                                     </div>
                                </div>
                           </div>            
                    </div>
               </div>
          </div>
          <div class="span4">
               <div class="span12">
                    <div class="control-group">
                          <div class="span12"  style="margin-left: 0px; margin-top: 5px;">
                                    <div class="control-group" style="margin-left: 0px; margin-top: 5px;">
                                         <label class="control-label" style="width: 100px;">Witness Name:</label>
                                         <div class="controls" style="margin-left: 110px;">
                                               <asp:TextBox ID="txtWname"    onkeypress="return IsAlphaNumeric(event);"   class="span12" placeholder="Witness Name" runat="server"
                                                     MaxLength="60" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                               <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                                                   runat="server"  InitialValue=""  ErrorMessage="Witness Name is required" ControlToValidate="txtWname" 
                                                      ForeColor="Red" Display="Dynamic" ValidationGroup="basicdetails"></asp:RequiredFieldValidator>
                                         </div>
                                    </div>
                               </div>
                          <div class="span12" style="margin-left: 0px; margin-top: 5px;">
                                <div class="control-group " style="margin-left:0px; margin-top: 5px;">
                                        <label class="control-label" style="width: 100px;">Witness Address:</label>
                                        <div class="controls" style="margin-left: 110px;">
                                            <asp:TextBox ID="txtWAdd" TextMode="MultiLine"   onkeypress="return IsAlphaNumeric(event);" class="span12" placeholder="Witness Address" runat="server"
                                                    MaxLength="1000" onblur="textBoxOnBlur(this);" style="resize:none; height: 114px;"></asp:TextBox>
                                            <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                                                runat="server"  InitialValue=""  ErrorMessage="Witness Address is required" ControlToValidate="txtWAdd" 
                                                    ForeColor="Red" Display="Dynamic" ValidationGroup="basicdetails"></asp:RequiredFieldValidator>
                                        </div>
                                </div>
                          </div>
                    </div>
               </div>
          </div>
          <div class="span4">
               <div class="span12">
                    <div class="control-group">
                           <div class="span12"  style="margin-left: 0px; margin-top: 5px;">
                                <div class="control-group" style="margin-left: 0px; margin-top: 5px;">
                                     <label class="control-label" style="width: 120px;">Agreement City:</label>
                                  <div class="controls" style="margin-left: 130px;">
                                    <asp:TextBox ID="txtAcity"   onkeypress="return IsAlphaNumeric(event);"   class="span11" placeholder="CITY" runat="server"
                                                 MaxLength="60" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                           <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validationposition"
                                               runat="server"  InitialValue=""  ErrorMessage="Agreement city is required" ControlToValidate="txtAcity" 
                                                  ForeColor="Red" Display="Dynamic" ValidationGroup="basicdetails"></asp:RequiredFieldValidator>

                                     </div>
                                </div>
                           </div>
                           <div class="span12" style="margin-left: 0px; margin-top: 5px;">
                                <div class="control-group " style="margin-left:0px; margin-top: 5px;">
                                     <label class="control-label" style="width: 120px;">Agreement Proviance:</label>
                                     <div class="controls" style="margin-left: 130px;">
                                             <asp:DropDownList ID="DropDownList2" AppendDataBoundItems="true" class="span11"  runat="server">
                                           <asp:ListItem Value="" >Choose Sub Category</asp:ListItem>
                                          </asp:DropDownList>
                                            <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validationposition"
                                          runat="server"  InitialValue=""  ErrorMessage="Choose Agreement Proviance" ControlToValidate="DropDownList2" 
                                            ForeColor="Red" Display="Dynamic" ValidationGroup="basicdetails"></asp:RequiredFieldValidator>
                                     </div>
                                </div>
                           </div>
                    </div>
               </div>
          </div>

          
          
          
     </div>

    </form>
</div>

</asp:Content>

