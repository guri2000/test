<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="Candidate_Feedback.aspx.vb" Inherits="Candidate_Feedback" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
.date
{
    width:150px;
    height:30px;
}

.border
{
    border-bottom:1px solid #ccc;
    
    transition: border 0.2s linear 0s, box-shadow 0.2s linear 0s;
}
 .red-star {
    color: red;
}   
.required input:after { content:"*"; }
.asterisk_input:after {
content:" *"; 
color: #e32;
position: absolute; 
margin: 0px 0px 0px 0px; 
font-size:23px; 
padding: 0 3px 0 0; }


.RadListBox .rlbCheckAllItemsCheckBox, .RadListBox .rlbCheck { margin-top: 0px !important; padding-top: 0px !important; margin-left: 4px !important; margin-right: 7px !important;vertical-align: middle !important;}
    
    
</style>

<%--<script language="javascript">

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

                if ((currentChar != '<') && (currentChar != '=') && (currentChar != '>') && (currentChar != '&') && (currentChar != '~') && (currentChar != '`') && (currentChar != '!') && (currentChar != '^') && (currentChar != '(') && (currentChar != ')') && (currentChar != '_') && (currentChar != '{') && (currentChar != '}') && (currentChar != ':') && (currentChar != ';') && (currentChar != '""') && (currentChar != '|') && (currentChar != '?') && (currentChar != '[') && (currentChar != ']' && (currentChar != '"')))
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

                    if ((currentChar != '<') && (currentChar != '=') && (currentChar != '>') && (currentChar != '&') && (currentChar != '~') && (currentChar != '`') && (currentChar != '!') && (currentChar != '^') && (currentChar != '(') && (currentChar != ')') && (currentChar != '_') && (currentChar != '{') && (currentChar != '}') && (currentChar != ':') && (currentChar != ';') && (currentChar != '""') && (currentChar != '|') && (currentChar != '?') && (currentChar != '[') && (currentChar != ']') && (currentChar != '#') && (currentChar != '$') && (currentChar != '^') && (currentChar != '*') && (currentChar != '%') && (currentChar != '"'))
                        newValue += currentChar;
                }

                elementRef.value = newValue;
                return false;
            }

            return true;
        }

       

</script>

    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to delete this resume?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
       <script type="text/javascript">
           $(document).ready(function () {
               var MaxLength = 3000;
               $('#txt_Feedback_BDC').keypress(function (e) {
                   if ($(this).val().length >= MaxLength) {
                       e.preventDefault();
                   }
               });
           });

           $(document).ready(function () {
               var MaxLength = 3000;
               $('#txt_Feedback_Emp').keypress(function (e) {
                   if ($(this).val().length >= MaxLength) {
                       e.preventDefault();
                   }
               });
           });

           $('txt_Feedback_BDC').on('paste', function () {
               var element = $(this);
               setTimeout(function () {
                   element.val(element.val().replace(/['"]/g, ''));
               }, 1);
           });

           $('#txt_Feedback_BDC').on('paste', function (e) {
               if (e.shiftKey && e.keyCode == 222 || e.keyCode == 222) {
                   e.stopPropagation();
               }
           });

           $('txt_Feedback_Emp').on('paste', function () {
               var element = $(this);
               setTimeout(function () {
                   element.val(element.val().replace(/['"]/g, ''));
               }, 1);
           });

           $('#txt_Feedback_Emp').on('paste', function (e) {
               if (e.shiftKey && e.keyCode == 222 || e.keyCode == 222) {
                   e.stopPropagation();
               }
           });
    </script>  
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="container-fluid">
                <form id="Form1"  runat="server" method="post" class="form-horizontal">
                   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                   <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

                     <div class="row-fluid">
                         <div class="span12">
                             <div class="widget-title" style="float: right;margin: 10px 0 -13px;  border-bottom-style: none; 
                                    border-bottom-color: inherit; border-bottom-width: medium;">
                                <asp:Button ID="btn_Save" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
                                <asp:ImageButton  ID="ImgBtn_Save"    runat="server" ImageUrl="~/images/save20x20.png" AlternateText="Save" Height="20" Width="20" ToolTip ="Save"  />
                                <asp:ImageButton ID="ImgBtn_Refresh"    runat="server" ToolTip="Reset" ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />
                            </div>
                        </div>
                    </div>

                     <%--<div class="row-fluid">
                         <div class="span12">
                             <div class="widget-box">
                                 <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
                                     <h5>Candidate Feedback Record</h5>
                                 </div>
                             </div>
                         </div>
                     </div>--%>

                     <div class="row-fluid">
    <div class="span12">
    
      <div class="widget-box" >
       
        <div class="widget-content nopadding">
       
            <div class="control-group" style="margin-top:-1px;">
            
              <div class="row-fluid" style="text-align:center;" >
                <div class="span12">
                    <span id="error" style="color: Red;display: none; "> ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? Special Characters not allowed</span>
                </div>
             </div>
            <div class="row-fluid">
                <div class="span12">
                    <div class="span5">
                      <label class="control-label">Agent :</label>
                      <div class="controls">
                           <%--<asp:TextBox ID="txt_Agent"  Width="200px" MaxLength="25"  class="span11" 
                           placeholder="BDC Name" runat="server" Enabled="false" BorderStyle="None" BackColor="#f9f9f9">
                           </asp:TextBox>--%>
                          <asp:Label ID="lbl_Agent" runat="server" class="span11" style="margin-top: 6px;font-size: 14px;"></asp:Label>
                      </div>
                    </div>
                   
                    <div class="span6" style="float:right;margin-right: 1%;">
                     <label class="control-label">Job Position :</label>
                      <div class="controls">
                           <asp:DropDownList ID="ddl_Position" runat="server" class="span11" AutoPostBack="true">
                           </asp:DropDownList>
                          <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationposition"
                              runat="server" ErrorMessage="Job Position is required" ControlToValidate="ddl_Position" 
                                ForeColor="Red" InitialValue="" ></asp:RequiredFieldValidator>
                       </div>
                    </div>
                </div>
            </div>

             <div class="row-fluid">
                <div class="span12">
                    <div class="span5">
                      <label class="control-label">Interview Date :</label>
                      <div class="controls">
                          <telerik:RadDatePicker ID="Interview_Date" class="datepicker span11" runat="server" CssClass="txt"
                                   Calendar-CalendarTableStyle-VerticalAlign="Middle"  width="170px" Height="28px" MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy" ShowAnimation-Type="Slide" >
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                        </SpecialDays>
                                    </Calendar>
                         </telerik:RadDatePicker>
                         <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                              runat="server" ErrorMessage="Interview Date is required" ControlToValidate="Interview_Date" 
                                ForeColor="Red" InitialValue="" ></asp:RequiredFieldValidator>
                      </div>
                    </div>
                   
                    <div class="span6" style="float:right;margin-right: 1%;">
                     <label class="control-label">Final Status :</label>
                      <div class="controls">
                           <asp:DropDownList ID="ddl_Fin_status" runat="server" class="span11" >
                           </asp:DropDownList>
                          <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                              runat="server" ErrorMessage="Candidate Final Status is required" ControlToValidate="ddl_Fin_status" 
                                ForeColor="Red" InitialValue="" ></asp:RequiredFieldValidator>
                       </div>
                    </div>
                </div>
            </div>

         <div class="row-fluid">
            <div class="span12">
            <div class="span5">
            <div class="control-group">
                      <label class="control-label">Interview Panel :<span class="asterisk_input"></span><br /><small>(Max. Members would be 5)</small></label>
                          <div class="controls">
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                      <telerik:RadListBox RenderMode="Lightweight" ID="Lst_IntPanel" runat="server"  CheckBoxes="true" ShowCheckAll="false" CssClass="span11"
                                                        AutoPostBack="true" CausesValidation="false"  Height="276px" Width="129%">
                                        <Items>
                                        </Items>
                
                                    </telerik:RadListBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                           <br />
                          
                          </div>
                    </div>
                </div> 

                <div class="span6">
                    <div class="control-group">
                      <label class="control-label">BDC Remarks :<span class="asterisk_input">  </span></label>
                          <div class="controls">
                          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                              <asp:TextBox ID="txt_Feedback_BDC"  onkeypress="return IsAlphaNumeric(event);" TextMode="MultiLine" class="span11" AutoPostBack="true" 
                                            placeholder="Feedback"  MaxLength="3001" runat="server" Width="115%" Rows="5" style="max-width:115%;
                                            resize:none;" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                             </ContentTemplate>
                            </asp:UpdatePanel>
                       
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                                  runat="server" ErrorMessage="BDC Feedback is required" ControlToValidate="txt_Feedback_BDC" Display="Dynamic" 
                                  style="margin-top: 0%;"  ForeColor="Red" ></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txt_Feedback_BDC" ID="RegularExpressionValidator1" 
                                    style="margin-top: -2%;"    ForeColor="Red" ValidationExpression = "^[\s\S]{0,3010}$" runat="server" ErrorMessage="Maximum 3000 characters allowed."></asp:RegularExpressionValidator>
                          </div>
                    </div>

                    <div class="control-group" >
                      <label class="control-label">Employer Remarks :<span class="asterisk_input">  </span></label>
                          <div class="controls">
                          <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                              <asp:TextBox ID="txt_Feedback_Emp"  onkeypress="return IsAlphaNumeric(event);" TextMode="MultiLine" class="span11" AutoPostBack="true" 
                                            placeholder="Feedback" MaxLength="3001" runat="server" Width="115%" Rows="5" style="max-width:115%;
                                            resize:none;" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                                 </ContentTemplate>
                            </asp:UpdatePanel>
                           
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition" style="margin-top: 0%;"
                                  runat="server" ErrorMessage="Employer Feedback is required" ControlToValidate="txt_Feedback_Emp" Display="Dynamic" 
                                    ForeColor="Red" ></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txt_Feedback_Emp" ID="RegularExpressionValidator3"  AutoPostBack="true" 
                                      style="margin-top: -2%;"  ForeColor="Red" ValidationExpression = "^[\s\S]{0,3010}$" runat="server" ErrorMessage="Maximum 3000 characters allowed."></asp:RegularExpressionValidator>
                          </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="span5">
                    
                </div> 
                   
            </div>
        </div>

        
           <div class="clear" style="margin-bottom:10px;"></div>
         </div>
            
         
        </div>
      </div>
     
    </div>
  </div>
                </form>
            </div>
</asp:Content>

