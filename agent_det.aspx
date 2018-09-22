<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="agent_det.aspx.vb" Inherits="_Default" %>

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
                
                if ((currentChar != '<') && (currentChar != '=') && (currentChar != '>') && (currentChar != '&') && (currentChar != '~') && (currentChar != '`') && (currentChar != '!') && (currentChar != '^') && (currentChar != '(') && (currentChar != ')') && (currentChar != '_') && (currentChar != '{') && (currentChar != '}') && (currentChar != ':') && (currentChar != ';') && (currentChar != '""') && (currentChar != '|') && (currentChar != '?') && (currentChar != '[') && (currentChar != ']') )
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
    <div class="span12" style="margin-top: 5px; margin-bottom: 5px;">
    <div class="span4">
        <%-- <div class="control-group">
              <label class="control-label" style="width: 85px;">Lead Scope:<span style="color:Red;">*</span>  </label>
              <div class="controls" style="margin-left: 100px;">
                  <asp:DropDownList ID="DropDownList1"   AppendDataBoundItems="true" class="span11" runat="server">
               <asp:ListItem Value="" >Choose Lead Scope</asp:ListItem>
                <asp:ListItem Value="P" >B2C/Prospect</asp:ListItem>
                 <asp:ListItem Value="A" >B2B/Further Agent</asp:ListItem>
                  </asp:DropDownList>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="validationposition"
                  runat="server" ErrorMessage="Lead Scope is required" ControlToValidate="DropDownList1" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
         </div>--%>
   </div>
    <div class="" style="float: right;margin: 6px 0 -13px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
             
               <asp:Button ID="Button1" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"    runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />

            </div>
</div>
</div>
<div class="row-fluid" style="display:none;">
    <div class="span12">
<span id="error" style="color: Red; display: none"> ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? Special Characters not allowed</span>
</div>
</div>
  <div class="row-fluid">
    <div class="span6">
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Agent Personal-Info</h5>
        </div>
        <div class="widget-content">
      
            <div class="control-group">
          
              <label class="control-label">Name:<span style="color:Red;">*</span>  </label>
              <div class="controls">
                  <asp:TextBox ID="TextBox1" onkeypress="return IsAlphaNumeric(event);" onblur="textBoxOnBlur(this);"  class="span11" placeholder="Name" runat="server" MaxLength="100"
                     ></asp:TextBox>
                  <br /> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Name is required" ControlToValidate="TextBox1" 
                    ForeColor="Red" Display="Dynamic" ></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="control-group">
              <label class="control-label">Mobile No.:<span style="color:Red;">*</span> </label>
              <div class="controls">
               <asp:TextBox ID="TextBox5"  onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  onblur="textBoxOnBlur(this);" class="span11" placeholder="Mobile Number" runat="server"
                         MaxLength="15" AutoPostBack="true"></asp:TextBox>
               <br />
                <span id="MobMsg" runat="server" style="color:Red;" visible="false" ></span>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                  runat="server" ErrorMessage="Mobile No. is required" ControlToValidate="TextBox5" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
               <div class="control-group">
              <label for="normal" class="control-label">Email Id:<span style="color:Red;">*</span> </label> <span id="emlerror" style="color: Red; display: none">* Invalid Email Address</span> 
              <div class="controls">
                <asp:TextBox ID="TextBox11"  onkeypress="ValidateEmail(this)"  class="span11" placeholder="Email" runat="server"
                       MaxLength="50" AutoPostBack="true"></asp:TextBox>
                <br /> 
                
                <span id="EmlMsg" runat="server" style="color:Red;" visible="false" ></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                  runat="server" ErrorMessage="Email is required" ControlToValidate="TextBox11" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator   
                      ID="CompareValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Invalid Email Format" 
                      ControlToValidate="TextBox11"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
              </div>
            </div>
              <div class="control-group">
              <label class="control-label">Address:<span style="color:Red;">*</span>  </label>
              <div class="controls">
              <asp:TextBox ID="TextBox10" onkeypress="return IsAlphaNumeric(event);" onblur="textBoxOnBlur(this);"
                    TextMode="MultiLine" class="span11" placeholder="Address" runat="server"
                     MaxLength="3500"
                     style="min-width:92%;width: 92%;max-width:94%;max-height:64px;height:64px; resize:none;"></asp:TextBox>
               <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                  runat="server" ErrorMessage="Address is required" ControlToValidate="TextBox10" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <br />
              </div>
            </div>
            
              
           
             
              
              



        </div>
      </div>
     
    </div>
    <div class="span6">
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Agent Login-Info</h5>
        </div>
        <div class="widget-content">

            <div class="control-group">
              <label class="control-label">Login Name:<span style="color:Red;">*</span> </label>
              <div class="controls">
                <asp:TextBox ID="TextBox6" onkeypress="return IsAlphaNumeric(event);"  onblur="textBoxOnBlur(this);" Text="" class="span11"   placeholder="Login name" runat="server" MaxLength="100"
                       ></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                  runat="server" ErrorMessage="Login Name is required" ControlToValidate="TextBox6" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="control-group">
              <label for="normal" class="control-label">Password:<span style="color:Red;">*</span>  </label>
              <div class="controls">
                <asp:TextBox ID="TextBox7" onkeypress="return IsAlphaNumeric1(event);" onblur="validate(this);" Text="" TextMode="Password"   class="span11 mask text" placeholder="Password" runat="server" MaxLength="100"
                         ></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationposition"
                  runat="server" ErrorMessage="Password is required" ControlToValidate="TextBox7" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                   <%-- <asp:RegularExpressionValidator ID="ReqValContactPerson_SpecialChars" runat="server" CssClass="changecolor" 
                    ControlToValidate="TextBox7" Display="Dynamic" 
                    ErrorMessage=" ~ ` ! ^ & ( ) _ { } [ ] | \ : ; ' < > ? Special Characters not allowed" SetFocusOnError="True" 
                    ValidationExpression="[\%\/\\\&\?\,\'\;\:\!\-\~\`\#\$\^\;\*\(\)\{\}\[\]\>\<]+"  ForeColor="Red"
                    ></asp:RegularExpressionValidator>--%>
              </div>
            </div>
            <div class="control-group">
              <label for="normal" class="control-label">Confirm Password:<span style="color:Red;">*</span>  </label>
              <div class="controls">
                <asp:TextBox ID="TextBox8" onkeypress="return IsAlphaNumeric1(event);" onblur="validate(this);" TextMode="Password"   class="span11 mask text" placeholder="Confirm Password"  runat="server" MaxLength="100"
                        ></asp:TextBox>
               <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validationposition"
                  runat="server" ErrorMessage="Confirm Password is required" ControlToValidate="TextBox8" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator  ID="RequiredFieldValidator10" CssClass="validationposition"
                  runat="server" ErrorMessage="Password does not match." ControlToValidate="TextBox8"  ControlToCompare="TextBox7"
                    ForeColor="Red" Display="Dynamic"></asp:CompareValidator>

              </div>
            </div>
            <div class="control-group">
              <label for="normal" class="control-label">Account Status:<span style="color:Red;">*</span>  </label>
              <div class="controls">
                <asp:DropDownList ID="DropDownList2"   AppendDataBoundItems="true" class="span11" runat="server">
               <asp:ListItem Value="" >Choose Account Access</asp:ListItem>
                <asp:ListItem Value="A" >Active</asp:ListItem>
                 <asp:ListItem Value="I" >In-Active</asp:ListItem>
                  </asp:DropDownList>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validationposition"
                  runat="server" ErrorMessage="Account Status is required" ControlToValidate="DropDownList2" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
             <div class="control-group">
              <label for="normal" class="control-label">Account Scope:<span style="color:Red;">*</span> </label>
              <div class="controls">
                <asp:DropDownList ID="DropDownList3"  AppendDataBoundItems="true" class="span11" runat="server">
               <asp:ListItem Value="" >Choose Account Roll</asp:ListItem>
              <asp:ListItem Value="S">Adminstrator</asp:ListItem>
                 <asp:ListItem Value="U" >User</asp:ListItem>
                  </asp:DropDownList>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validationposition"
                  runat="server" ErrorMessage="Account Scope is required" ControlToValidate="DropDownList3" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <br />
              </div>
            </div>
          
          
        </div>
      </div>
    
    </div>

    


  </div>
  <div class="span12" style="display: flex;">

     <asp:CheckBox ID="chk" runat="server"  Text="" /> &nbsp; <span style="padding-top: 5px; padding-left: 5px;">Intimate user about credentials?</span>

    </div>
<%-- <div class="row-fluid">
    <div class="widget-box">
      
      <div class="widget-content">
        <div class="control-group">
     
          <div class="span4">
           <label for="normal" class="control-label" style="text-align:left;">Agent Name</label>
              
          </div>
          <div class="span4">
           <label for="normal" class="control-label" style="text-align:left;">Contracted Date</label>
              
          </div>
          <div class="span4">
          <label for="normal" class="control-label" style="text-align:left;">Result</label>
          </div>
     
                    
        
           
           </div>
           <div class="control-group">
     
          <div class="span4">
           
              <div class="control-label" style="text-align:left;padding-top:5px; padding-bottom:5px;">
                <asp:Label ID="label2"  class="span12" text="" runat="server"></asp:Label>
                 </div>
          </div>
          <div class="span4">
           
              <div class="control-label" style="padding-top:5px; text-align:left; padding-bottom:5px;">
                 <telerik:RadDatePicker ID="dtSubmission" class="span12 control" runat="server" CssClass="txt"
                                    Width="100%" MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy"   ShowAnimation-Type="Slide">
                                    <Calendar ID="Calendar2" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                 
                 </div>
          </div>
          <div class="span4">
          
              <div class="control-label" style="text-align:left;padding-top:5px; padding-bottom:5px;">
                  <asp:DropDownList ID="DropDownList4" AppendDataBoundItems="true" CssClass="span12" runat="server">
                    <asp:ListItem Value="" >Choose Result</asp:ListItem>
                  </asp:DropDownList>
                 </div>
          </div>
     
                    
        
           
           </div>
          

        
      </div>
    </div>
  </div>--%>
   </form>
</div>

</asp:Content>

