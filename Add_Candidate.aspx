<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="Add_Candidate.aspx.vb" Inherits="default2" %>
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

</style>


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

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="container-fluid">
                <form id="Form1"  runat="server" method="post" class="form-horizontal">
                   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                   <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

                     <div class="row-fluid">
                         <div class="span12">
                             <div class="" style="float: right;margin: 6px 0 -13px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
                                <asp:Button ID="btn_Save" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
                                <asp:ImageButton  ID="ImgBtn_Save"    runat="server" ImageUrl="~/images/save20x20.png" AlternateText="Save" Height="20" Width="20" ToolTip ="Save"  />
                                <asp:ImageButton ID="ImgBtn_Refresh"    runat="server" ToolTip="Reset" ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />
                            </div>
                        </div>
                    </div>

                     <div class="row-fluid">
                         <div class="span12">
                             <div class="widget-box" style="margin-bottom:0px;">
                                 <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
                                     <h5>Candidate Record</h5>
                                 </div>
                             </div>
                         </div>
        
                     </div>

    <div class="row-fluid">
    <div class="span12">
      <div class="widget-box" >
       <div class="widget-content inner-class">
       
          <div class="row-fluid" style="text-align:center; display: none;" >
                <div class="span12">
                    <span id="error" style="color: Red;display: none; "> ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? Special Characters not allowed</span>
                </div>
             </div>
            <div class="row-fluid row-margin">
                <div class="span12">
                    <div class="span4">
                      <label class="control-label-list full-width">Agent:</label>
                      <div class="controls-list full-width-content">
                          <asp:DropDownList ID="ddl_Agent" runat="server"  AutoPostBack="true">
                            
                          </asp:DropDownList>
                           <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                              runat="server" ErrorMessage="Agent is required" ControlToValidate="ddl_Agent" 
                                ForeColor="Red" InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="span4">
                      <label class="control-label-list full-width">Employer:</label>
                      <div class="controls-list full-width-content"">
                          <asp:DropDownList ID="ddl_Employer" runat="server" AutoPostBack="true">
                          
                          </asp:DropDownList>
                          <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validationposition"
                              runat="server" ErrorMessage="Employer is required" ControlToValidate="ddl_Employer" 
                                ForeColor="Red" InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>
                       </div>
                    </div>
                    <div class="span4">
                     <label class="control-label-list full-width">Position:</label>
                      <div class="controls-list full-width-content"">
                           <asp:DropDownList ID="ddl_Position" runat="server"  AutoPostBack="true">
                            
                          </asp:DropDownList>
                          <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationposition"
                              runat="server" ErrorMessage="Job Position is required" ControlToValidate="ddl_Position" 
                                ForeColor="Red" InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>
                       </div>
                    </div>
          </div>
            </div>

            <div class="row-fluid row-margin">
            <div class="span12">
           <div class="span4">
                  <label class="control-label-list">FSN:<span style="color:Red;">*</span></label>
                  <div class="controls-list">
                       <asp:TextBox ID="txt_FSN" onkeypress="return isNumberKey(event);" MaxLength="25"  class="span11" placeholder="Fsn Number" runat="server"></asp:TextBox>
                      
                       <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                                                  runat="server" ErrorMessage="FSN is required" ControlToValidate="txt_FSN" 
                                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                   
                  </div>
            </div>
            <div class="span4">
            <asp:ImageButton ID="Imgbtn_Get_Cand_Data"    runat="server" ToolTip="Search FSN" ImageUrl="~/images/refresh16x16.png" Width="16px"   CausesValidation="False" />
            </div>
            </div></div>

             <div class="row-fluid row-margin">
            <div class="span12">
            <div class="span3" >
                    <label class="control-label-list" >  
                   Name:
                    </label>
                      <div class="controls-list">
                         <%-- <asp:TextBox ID="TextBox2" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Date of Birth" runat="server"></asp:TextBox><br />--%>
                           <asp:Label ID="lbl_Name" runat="server" Height="20" Width="120" CssClass="border"></asp:Label>
                      </div>
                </div>

                <div class="span3">
                 <label class="control-label-list">DOB:</label>
                  <div class="controls-list">
                     <%-- <asp:TextBox ID="TextBox2" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Date of Birth" runat="server"></asp:TextBox><br />--%>
                       <asp:Label ID="lbl_DOB" runat="server" Height="20" Width="120" CssClass="border"></asp:Label>
                  </div>
                </div>
                <div class="span3">
                 <label class="control-label-list">Location:</label>
                  <div class="control-list">
                     <%--<asp:TextBox ID="TextBox5" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Location" runat="server"></asp:TextBox><br />--%>
                    <asp:Label ID="lbl_Location" runat="server" Height="20" Width="120" CssClass="border"></asp:Label>
                  </div>
                </div>
                <div class="span3">
                  <label class="control-label-list">Imm Class:</label>
                  <div class="control-list">
                     <%--<asp:TextBox ID="TextBox3" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Imm Class" runat="server"></asp:TextBox><br />--%>
                    <asp:Label ID="lbl_ImmClass" runat="server" Height="20" Width="120"  CssClass="border"></asp:Label>
                  </div>
            </div>
            </div>
            </div> 
          
        
            <div class="row-fluid row-margin" style="display:none;">
            <div class="span12">
            <div class="span4" >
              <label class="control-label-list">Category:</label>
              <div class="controls-list">
                  <%--<asp:TextBox ID="TextBox4" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Category" runat="server"></asp:TextBox><br />--%>
                  <asp:Label ID="lbl_Category" runat="server" Height="20" CssClass="border"></asp:Label>
              </div>
            </div>
             </div> 
            </div>

            <div class="row-fluid row-margin">
             <div class="span12">
             <div class="span4">
    <label class="control-label-list full-width-one">Experience:<span style="color:Red;">*</span></label>
              <div class="controls-list full-width-content-one">
                  <asp:TextBox ID="txt_Exp"   onkeypress="return IsAlphaNumeric(event);" onblur="textBoxOnBlur(this);"
                         MaxLength="201" class="span11" placeholder="Experience" runat="server" ></asp:TextBox>
                  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Experience is required" ControlToValidate="txt_Exp" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txt_Exp" ID="RegularExpressionValidator2" ForeColor="Red" ValidationExpression = "^[\s\S]{0,200}$" runat="server" ErrorMessage="Maximum 200 characters allowed."></asp:RegularExpressionValidator>
         </div>
         </div>
   <div class="span4">
              <label for="normal" class="control-label-list full-width-one">Highest Education:<span style="color:Red;">*</span></label>
              <div class="controls-list full-width-content-one"">
                 <asp:TextBox ID="txt_EDU"  onkeypress="return IsAlphaNumeric(event);" onblur="textBoxOnBlur(this);"
                       MaxLength="251" class="span11" placeholder="Highest Education" runat="server"  ></asp:TextBox>
                 <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                  runat="server" ErrorMessage="Education is required" ControlToValidate="txt_EDU" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txt_EDU" ID="RegularExpressionValidator1" ForeColor="Red" ValidationExpression = "^[\s\S]{0,250}$" runat="server" ErrorMessage="Maximum 250 characters allowed."></asp:RegularExpressionValidator>
              </div>
            </div>
             <div class="span4 resume">
              <label class="control-label-resume">Resume:<span style="color:Red;">*</span></label>
              <div class="controlsall">
                  <asp:FileUpload ID="Resume_Upload"  placeholder="Browse" runat="server" style="height:28px;width: 80%;margin-left: 0px;"  ToolTip="Upload Resume"  /> &nbsp;&nbsp;
                  <asp:ImageButton ID="imgbtn_Upload_Resume" ImageUrl="~/images/Upload_btn.jpg" AlternateText="Upload Attachment" 
                        style="margin:0; width:34px; height:28px;vertical-align: bottom;" runat="server" CausesValidation="false"  />
                  <asp:Label ID="lbl_ResumeFileName" runat="server"  ReadOnly="true"  BorderStyle="None"></asp:Label> 
                  <asp:Label ID="lbl_FullPath" runat="server" Visible="false"   ></asp:Label> 
                 
                  
               
                 <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validationposition"
                  runat="server" ErrorMessage="Upload Resume is required." ControlToValidate="lbl_ResumeFileName" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
             
              </div>

             </div>
             </div>
            </div>
            <div class="row-fluid row-margin" style="display:none;" >
             <div class="span12">
             <div class="span3">
              <label class="control-label-list">Interview:</label>
              <div class="control-list">
                 <telerik:RadDatePicker ID="Interview_Date" class="datepicker span11" runat="server" CssClass="txt"
                                    width="170px" Height="30px" MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy" ShowAnimation-Type="Slide" Enabled="false"  >
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                        </SpecialDays>
                                    </Calendar>
                </telerik:RadDatePicker>
                                    <br />
              </div>
            </div>
          
            </div> 
            </div> 

             <div class="row-fluid row-margin">
            <div class="span10">
            <label class="control-label full-width-remarks">Notes / Remarks:<span style="color:Red;">*</span></label>

           </div>
           <div class="span2">
            <asp:ImageButton  ID="View_Resume" runat="server" ToolTip="View Resume" AlternateText="View Resume" CausesValidation="false" /> 
                  <asp:ImageButton ID="imgbtn_Delete_Resume" runat="server" ToolTip="Delete Resume" ImageUrl="~/images/delete_btn.png" Width="20px" Height="20px"   CausesValidation="False" OnClientClick = "Confirm()" />
           </div>
           </div>

            <div class="row-fluid row-margin">
            <div class="span12">                       
              <div class="controlstext-area">
              <asp:TextBox ID="txt_Feedback"  onkeypress="return IsAlphaNumeric(event);" TextMode="MultiLine" class="span11" 
                            placeholder="Feedback" MaxLength="2001" runat="server" Rows="4" Width="100%" style="max-width:100%;resize:none;" onblur="textBoxOnBlur(this);"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                  runat="server" ErrorMessage="Feedback is required" ControlToValidate="txt_Feedback" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txt_Feedback" ID="RegularExpressionValidator3" ForeColor="Red" ValidationExpression = "^[\s\S]{0,2000}$" runat="server" ErrorMessage="Maximum 2000 characters allowed."></asp:RegularExpressionValidator>
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

