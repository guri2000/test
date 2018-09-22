<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="cat_det.aspx.vb" Inherits="_Default" %>

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
    <div class="container-fluid">

      <form  runat="server" method="post" class="form-horizontal" >
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
      <div class="row-fluid">
    <div class="span12">
    <div class="" style="float: right;margin: 8px 0 -13px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
             
               <asp:Button ID="Button1" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"    runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="18" Width="18" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"  Width="18"  CausesValidation="False" />

            </div>
</div>
</div>

     <div class="row-fluid" style="display: none;">
    <div class="span12">
<span id="error" style="color: Red; display: none"> ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? Special Characters not allowed</span>
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Categories-info</h5>
        </div>
        </div></div></div>
  <div class="row-fluid">
    <div class="span12">
      <div class="widget-box" style="margin-top:0px">
        
        <div class="widget-content">
      
            <div class="control-group">
              <label class="control-label">Category:<span style="color:Red;">*</span></label>
              <div class="controls">
                  <asp:TextBox ID="TextBox1" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Name" runat="server"
                          onblur="textBoxOnBlur(this);" MaxLength="500"></asp:TextBox>
                   <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                  runat="server" ErrorMessage="Category is required" ControlToValidate="TextBox1" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
         
          <div class="control-group">
              <label class="control-label">Sub Category:<span id="sct" runat="server" style="color:Red;">*</span></label>
              <div class="controls">
                  <asp:TextBox ID="TextBox2" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Name" runat="server"
                          onblur="textBoxOnBlur(this);" MaxLength="500"></asp:TextBox>
                   <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                  runat="server" ErrorMessage="Sub-Category is required" ControlToValidate="TextBox2" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
                      
            <div class="control-group">
              <label for="normal" class="control-label">Document Status:<span style="color:Red;">*</span></label>
              <div class="controls">
                <asp:DropDownList ID="DropDownList2"   AppendDataBoundItems="true" class="span11" runat="server">
               <asp:ListItem Value="" >Choose  Status</asp:ListItem>
                <asp:ListItem Value="A" >Active</asp:ListItem>
                 <asp:ListItem Value="I" >In-Active</asp:ListItem>
                  </asp:DropDownList>
                  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Status is required" ControlToValidate="DropDownList2" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
              

            </div>
          
            
         
        </div>
      </div>
     
    </div>
 
 
  </div>

   </form>
</div>

</asp:Content>

