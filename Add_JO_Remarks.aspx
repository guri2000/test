<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="Add_JO_Remarks.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <script language="javascript">

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

    </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">

      <form  runat="server" method="post" class="form-horizontal" >
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
      <div class="row-fluid">
    <div class="span12">
    <div class="widget-title" 
            style="float: right;margin: 10px 0 -13px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
             
               <asp:Button ID="Button1" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"    runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />

            </div>
</div>
</div>

     <div class="row-fluid">
    <div class="span12">
<span id="error" style="color: Red; display: none">* Special Characters not allowed</span>
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Secured Job Order Remarks-Info</h5>
        </div>
        </div></div></div>
  <div class="row-fluid">
    <div class="span12">
      <div class="widget-box" style="margin-top:0px;">
        
        <div class="widget-content nopadding">
        
            <div class="control-group">
            
            <div class="row-fluid">
               <div class="span12">
    
          
            <div class="control-group">
              <label class="control-label">Remarks :</label>
              <div class="controls">
              <asp:TextBox ID="TextBox10" onkeypress="return IsAlphaNumeric(event);" 
                          TextMode="MultiLine" class="span11" placeholder="Enter Remarks" runat="server" Width="97%" Rows="13" style="max-width:97%;"
                          oncopy="return false" oncut="return false" onpaste="return false"
                            onmousedown="DisableRightClick(event)" onkeydown="return DisableCtrlKey(event)"
                            ></asp:TextBox>
                  <br /> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Remarks is required." ControlToValidate="TextBox10" 
                    ForeColor="Red" ></asp:RequiredFieldValidator>
              </div>
            </div>
          
       
    
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

