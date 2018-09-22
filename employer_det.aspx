<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="employer_det.aspx.vb" Inherits="_Default" %>

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

<style type="text/css">
    .divTable{
	display: table;
	width: 100%;
}
.divTableRow {
	display: flex;

border-bottom: 1px solid #e4e3e3;
}
.divTableHeading {
	background-color: #EEE;
	display: table-header-group;
}
.divTableCell, .divTableHead {
	border: 0px solid #ccc;
display: table-cell;
padding: 3px 10px;
width: 60%;
}
.divTableHeading {
	background-color: #EEE;
	display: table-header-group;
	font-weight: bold;
}
.divTableFoot {
	background-color: #EEE;
	display: table-footer-group;
	font-weight: bold;
}
.divTableBody {
	display: table-row-group;
}
</style>


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style>
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.modal {
    position: fixed;
    top: 3%;
    left: 5%;
    right:5%;
    z-index: 1050;
    width: 90%;
    height:300px;
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.modal-body{max-height: 300px !important; padding: 0px !important; overflow-y: hidden !important;}


.modal1 {
    position: fixed;
    top: 3%;
    left: 5%;
    right:5%;
    z-index: 1050;
    width: 90%;
    height:700px;
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.modal-body1{max-height: 700px !important; padding: 0px !important; overflow-y: hidden !important;}
.que_drop {width: 100%;}
.que_txtbox {width: 97%;}
.aspNetDisabled {width: 100%;}
.ico_clr {color:#0081cc;}
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
          <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>

       <div class="row-fluid">
    <div class="span12" style="margin-top: 5px; margin-bottom: 5px;">
    <div class="span4">
         <div class="control-group">
              <label class="control-label" style="width: 85px;">Scope:<span style="color:Red;">*</span>  </label>
              <div class="controls" style="margin-left: 100px;">
                  <asp:DropDownList ID="DropDownList3"   AppendDataBoundItems="true" class="span11" runat="server">
               <asp:ListItem Value="" >Choose Scope</asp:ListItem>
                <asp:ListItem Value="P" >Direct Prospect (B2C)</asp:ListItem>
                 <asp:ListItem Value="A" >Further Agency/Agent (B2B)</asp:ListItem>
                  </asp:DropDownList>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="validationposition"
                  runat="server" ErrorMessage="Scope is required" ControlToValidate="DropDownList3" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
         </div>
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
</div></div>
<div class="row-fluid">
    <div class="span6">
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Personal-Info</h5>
        </div>
        <div class="widget-content">
      
            <div class="control-group">
              <label class="control-label">First Name:<span style="color:Red;">*</span></label>
              <div class="controls">
                  <asp:TextBox ID="TextBox1" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="First name" runat="server"
                      MaxLength="99" onblur="textBoxOnBlur(this);"></asp:TextBox>
                  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                  runat="server" ErrorMessage="First Name is required" ControlToValidate="TextBox1" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
             <div class="control-group">
              <label class="control-label">Middle Name:</label>
              <div class="controls">
              <asp:TextBox ID="TextBox2" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Middle name" runat="server"
                    MaxLength="99" onblur="textBoxOnBlur(this);"></asp:TextBox>
               
              </div>
            </div>
            <div class="control-group">
              <label class="control-label">Last Name:<span style="color:Red;">*</span></label>
              <div class="controls">
              <asp:TextBox ID="TextBox3" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Last name" runat="server"
                     MaxLength="99" onblur="textBoxOnBlur(this);"></asp:TextBox>
                 <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Last Name is required" ControlToValidate="TextBox3" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="control-group">
              <label class="control-label">Designation: </label>
              <div class="controls">
                
                   <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="true" class="span10"  runat="server">
                   <asp:ListItem Value="" >Choose Designation</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp; 
                 
                 
                 
                           <a id="add-event" data-toggle="modal" href="#addnewrecord" >
                           <img style="border:0px;vertical-align:middle;" alt="" src="images/add-new24x24.png"/></a>
                      <asp:ImageButton ID="Btnref"    runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png"   CausesValidation="False" />

              
                 <%--  <asp:ImageButton  ID="imgbtn_AddDesignation" runat="server"  
                   ImageUrl="~/images/add-new24x24.png" CausesValidation="false" ToolTip ="Designation"  />--%>
                      
                    <%-- <a href="#"><img src="images/add-new24x24.png" border="0" alt="" /></a>--%>
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                  runat="server" ErrorMessage="Designation is required" ControlToValidate="DropDownList1" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="control-group">
              <label class="control-label">Tel. No.:<span style="color:Red;">*</span></label>
              <div class="controls">
               <asp:TextBox ID="TextBox4" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  class="span11" placeholder="Telephone Number" runat="server"
                         MaxLength="20" onblur="textBoxOnBlur(this);"></asp:TextBox>

                         <span id="Span1" runat="server" style="color:Red;" visible="false" ></span><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="validationposition"
                  runat="server" ErrorMessage="Tel. No. is required" ControlToValidate="TextBox4" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator> 
              </div>
            </div>
            <div class="control-group">
              <label class="control-label">Mobile No.:</label>
              <div class="controls">
               <asp:TextBox ID="TextBox5" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  class="span11" placeholder="Mobile Number" runat="server"
                    MaxLength="20" onblur="textBoxOnBlur(this);" ></asp:TextBox>
             
              </div>
            </div>
               <div class="control-group">
              <label for="normal" class="control-label">Email Id:<span style="color:Red;">*</span></label>
              <div class="controls">
                <asp:TextBox ID="TextBox11" onmouseout="ValidateEmail(this)"  class="span10" placeholder="Email" runat="server" AutoPostBack="true" 
                       MaxLength="50" onblur="textBoxOnBlur(this);"  ></asp:TextBox>
                        <a id="a1" runat="server" style="display:none;" data-toggle="modal" href="#addnewfollowup" >
                           <img style="border:0px;vertical-align:middle;" alt="" src="images/add-remarks16x16.png"/></a>
               <br />
               <span id="EmlMsg" runat="server" style="color:Red;" visible="false" ></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validationposition"
                  runat="server" ErrorMessage="Email is required" ControlToValidate="TextBox11" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator> &nbsp;&nbsp;<asp:RegularExpressionValidator   
                      ID="CompareValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Invalid Email Format" 
                      ControlToValidate="TextBox11"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                  
              </div>
            </div>
            <%--<div class="control-group" style="visibility:hidden;">
              <label class="control-label">a</label>
              <div class="controls">
               
              </div>
            </div>--%>
           
           
           
         
        </div>
      </div>
     
    </div>
    <div class="span6">
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Business-Info</h5>
        </div>
        <div class="widget-content">
       
           <div class="control-group">
              <label class="control-label">Industry Sector:</label>
              <div class="controls">
               <asp:DropDownList ID="DropDownList2"  AppendDataBoundItems="true" class="span10" runat="server">
               <asp:ListItem Value="" >Choose Industry Sector</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;
                   <a id="add-event_ind"  data-toggle="modal" href="#addnewrecord_Ind">
                           <img style="border:0px;vertical-align:middle;" alt="" src="images/add-new24x24.png"/></a>
<asp:ImageButton ID="Btnref2"    runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png"   CausesValidation="False" />
               <%--   <asp:ImageButton  ID="imgbtn_AddIndustry" runat="server"  
                   ImageUrl="~/images/add-new24x24.png" ToolTip ="Industry"  />--%>
              <%--    <a href="#"><img src="images/add-new24x24.png" border="0" alt="" /></a>--%>
                   <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                  runat="server" ErrorMessage="Industry Sector is required" ControlToValidate="DropDownList2" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="control-group">
              <label class="control-label">Company Name:<span style="color:Red;">*</span></label>
              <div class="controls">
                <asp:TextBox ID="TextBox6" onkeypress="return IsAlphaNumeric(event);" class="span11" placeholder="Company name" runat="server"
                   MaxLength="199" onblur="textBoxOnBlur(this);" AutoPostBack="True"></asp:TextBox>
                  <br />
                  <span id="CmpMsg" runat="server" style="color:Red;" visible="false" ></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                  runat="server" ErrorMessage="Company Name is required" ControlToValidate="TextBox6" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="control-group">
              <label for="normal" class="control-label">Office Telephone:<span style="color:Red;">*</span></label>
              <div class="controls">
                <asp:TextBox ID="TextBox7" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  class="span11" placeholder="Office No." runat="server"
                    MaxLength="40" onblur="textBoxOnBlur(this);"></asp:TextBox>
                    <br />
                     <span id="OffTel" runat="server" style="color:Red;" visible="false" ></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" CssClass="validationposition"
                  runat="server" ErrorMessage="Office Telephone is required" ControlToValidate="TextBox7" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="control-group">
              <label for="normal" class="control-label">Fax No.:</label>
              <div class="controls">
                <asp:TextBox ID="TextBox8" onKeyPress="return isNumberKey(event)" class="span11" placeholder="Fax No." runat="server"
                   MaxLength="40" onblur="textBoxOnBlur(this);" ></asp:TextBox>
               
              </div>
            </div>
            <div class="control-group">
              <label for="normal" class="control-label">Website:<span style="color:Red;">*</span></label>
              <div class="controls">
                <asp:TextBox ID="TextBox9"  class="span11" placeholder="Website" runat="server" AutoPostBack="true" 
                    MaxLength="200"   ></asp:TextBox>
             <br />   
              <span id="WebMsg" runat="server" style="color:Red;" visible="false" ></span>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validationposition"
                  runat="server" ErrorMessage="Website is required" ControlToValidate="TextBox9" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

             <asp:RegularExpressionValidator   
                      ID="RegularExpressionValidator1" CssClass="validationposition"
                  runat="server" ErrorMessage="Invalid Website Format" 
                      ControlToValidate="TextBox9"
                    ForeColor="Red" Display="Dynamic"
                      ValidationExpression="([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" ></asp:RegularExpressionValidator>
              </div>
            </div>
             <div class="control-group">
              <label class="control-label">Address:</label>
              <div class="controls">
              <asp:TextBox ID="TextBox10" style="height: 101px !important; resize:none;" onkeypress="return IsAlphaNumeric(event);" 
                    TextMode="MultiLine" class="span11" placeholder="Address" runat="server"
                     MaxLength="3000" onblur="textBoxOnBlur(this);"></asp:TextBox>
               
              </div>
            </div>
          
          
        </div>
      </div>
    
    </div>
  </div>
 <div class="row-fluid">
    <div class="widget-box" style="margin-top:0px;">
      
      <div class="widget-content">
        <div class="control-group">
     
          <div class="span2">
           <label for="normal" class="control-label" style="text-align:left; margin-bottom: 0px;">Contacted By:</label>
              
          </div>
          <div class="span2">
           <label for="normal" class="control-label" style="text-align:left; margin-bottom: 0px;">Initial Contact:</label>
              
          </div>
          <div class="span4">
          <label for="normal" class="control-label" style="text-align:left; margin-bottom: 0px;">Result:<span style="color:Red;">*</span></label>
          </div>
     
                    
        
           
           </div>
           <div class="control-group">
     
          <div class="span2">
           
              <div class="control-label" style="text-align:left;padding-top:0px; padding-bottom:1px;">
                <asp:Label ID="label2"  class="span12" text="" style="font-weight:600;" runat="server"></asp:Label>
                 </div>
          </div>
          <div class="span2">
           
              <div class="control-label" style="padding-top:0px; text-align:left; padding-bottom:1px;">
              
                                           <telerik:RadDateTimePicker ID="dtSubmission" class="span12 control" runat="server"
                                   MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy" 
                      Culture="en-US" ResolvedRenderMode="Classic"  >
<TimeView CellSpacing="-1" Interval="00:01:00" ></TimeView>

                                   <TimePopupButton Visible="false" />
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" >
<ItemStyle CssClass="rcToday"></ItemStyle>
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
<DateInput DisplayDateFormat="dd-MMM-yyyy" DateFormat="dd-MMM-yyyy" LabelWidth="40%">
<%--<DateInput DisplayDateFormat="dd-MMM-yyyy hh:mm:ss" DateFormat="dd-MMM-yyyy hh:mm:ss" LabelWidth="40%">--%>
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>
                 <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationposition"
                  runat="server" ErrorMessage="Contacted Date is required" ControlToValidate="dtSubmission" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                 </div>
          </div>
          <div class="span4">
          
              <div class="control-label" style="text-align:left; padding-top:0px; padding-bottom:1px; width:95%;">
                  <asp:DropDownList ID="DropDownList4" AppendDataBoundItems="true" CssClass="span12" runat="server">
                    <asp:ListItem Value="" >Choose Result</asp:ListItem>
                  </asp:DropDownList>
                  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validationposition"
                  runat="server" ErrorMessage="Result is required" ControlToValidate="DropDownList4" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator><br />
                 </div>
          </div>
     
                    
        
           
           </div>
                <div class="modal hide" id="addnewrecord">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3>Desposition Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Ifrm1"  frameborder="0" width="100%" height="400px"></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>
             
             
               <div class="modal hide" id="addnewrecord_Ind">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3>Disposition Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Ifrm_ind"  frameborder="0" width="100%" height="299px"></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>
              
               
          <div class="modal hide"  id="addnewpop1">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Ifrm12"  frameborder="0" width="100%" height="299px" ></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>
              <button type="button" style="display: none;" id="btnShowPopup1" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewpop1">
                Launch demo modal
            </button>  

            <div class="modal1 hide"  id="addnewfollowup">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;"><asp:Label ID="Label4" runat="server" Text="Existing Followups"></asp:Label></h3>
                </div>
                <div class="modal-body1">
                <iframe runat="server" id="Ifrmfollowup"  frameborder="0" width="100%" height="700px"  ></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>
             


      </div>
    </div>
  </div>

  <div class="row-fluid" style="margin-top:0px; margin-bottom:10px; background: #fff;" id="quest" runat ="server">
            <div class="widget-box" style="margin-bottom: 5px;">
                <div class="widget-title">
                    <span class="icon"><i class="icon-align-justify"></i></span>
                    <h5>Additional Information <%--<i class="icon-angle-right ico_clr"></i>--%></h5>
                     <div class="">          
                           <asp:Button ID="Button3" Visible="false" class="btn btn-success" CausesValidation="false"  runat="server" Text="Save" />                                            
                     </div>
                 </div>
             </div>
                
               <div class="widget-content" style="height: 200px; overflow: scroll; position: relative; display: block; overflow-x: hidden;">
                       <div class="divTable" id="panel_s1a" runat ="server">
                            <div class="divTableBody" id="pnlTable" runat="server" style="width:100%;">

                            </div>
                       </div>
               </div>

</div>

  <div class="row-fluid" id="Pnl_Rating" runat ="server" visible ="false" >
  <div class="span6">
    <div class="widget-box" style="margin-top:0px;">
         <div class="widget-title">
                    <span class="icon"><i class="icon-align-justify"></i></span>
                    <h5>Screening </h5>
          
                 </div>
      
      <div class="widget-content">
        <div class="control-group">
     
          <div class="span2">
           <label for="normal" class="control-label" style="text-align:left; margin-bottom: 0px;">Screening By:</label>
              
          </div>
          <div class="span4">
           <label for="normal" class="control-label" style="text-align:left; margin-bottom: 0px;">Date:</label>
              
          </div>

          <div class="span5">
          <label for="normal" class="control-label" style=" margin-bottom: 0px; text-align:left;">Choose Rating:<span style="color:Red;">*</span></label>
          </div>           
           
           </div>

           <div class="control-group">
     
          <div class="span2">
           
              <div class="control-label" style="text-align:left;padding-top:0px; padding-bottom:1px;">
                <asp:Label ID="label3"  class="span12" text="" style="font-weight:600;" runat="server"></asp:Label>
                 </div>
          </div>
          <div class="span4">
           
              <div class="control-label" style="padding-top:0px; text-align:left; padding-bottom:1px;">
              
                                           <telerik:RadDateTimePicker ID="dtSubmission2" class="span12 control" runat="server"
                                   MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy" 
                      Culture="en-US" ResolvedRenderMode="Classic"  >
<TimeView CellSpacing="-1" Interval="00:01:00" ></TimeView>

                                   <TimePopupButton Visible="false" />
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" >
<ItemStyle CssClass="rcToday"></ItemStyle>
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
<DateInput DisplayDateFormat="dd-MMM-yyyy" DateFormat="dd-MMM-yyyy" LabelWidth="40%">
<%--<DateInput DisplayDateFormat="dd-MMM-yyyy hh:mm:ss" DateFormat="dd-MMM-yyyy hh:mm:ss" LabelWidth="40%">--%>
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>
                 <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="validationposition"
                  runat="server" ErrorMessage="Contacted Date is required" ControlToValidate="dtSubmission" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                 </div>
          </div>
          <div class="span5">
          
              <div class="control-label" style="padding-top:0px; padding-bottom:1px; width:95%; text-align:right;">
                 
                    <telerik:RadRating RenderMode="Lightweight" ID="RadRating1" runat="server" ItemCount="5"
                    Value="0" SelectionMode="Continuous" Precision="Half" Orientation="Horizontal">
                </telerik:RadRating>
<br /><asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="validationposition"
                  runat="server" ErrorMessage="Rating is required" ControlToValidate="RadRating1" 
                    ForeColor="Red" Display="Dynamic">
                    
                    </asp:RequiredFieldValidator>

                 </div>
          </div>
  
        <div class="span10">

                <asp:TextBox ID="txtRatRem" style="height: 80px !important; resize:none; width:100%;" onkeypress="return IsAlphaNumeric(event);" 
                    TextMode="MultiLine" class="span11" placeholder="Remarks" runat="server"
                     MaxLength="1000" onblur="textBoxOnBlur(this);"></asp:TextBox>
              </div>
         </div>
           
         
              
      </div>
      </div>
    </div>
 <%--   grid1--%>
    <div class="span6">
     <div class="widget-box" style="margin-top:0px;">
      <div class="widget-title">
                    <span class="icon"><i class="icon-align-justify"></i></span>
                    <h5>Rating </h5>
          
                 </div>
      <div class="widget-content">


               <telerik:RadGrid ID="RadGrid1" runat="server" EnableViewState="true"  Skin="Metro" PageSize="4" CSSClass="table table-bordered"  
            AllowPaging="true" ShowStatusBar="true" AutoGenerateColumns="true" Width="100%"  CellSpacing="0" GridLines="none"  
             AllowSorting="true"   AllowFilteringByColumn="false"  Height="150px" >
                <%-- <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false" />--%>
             
                  <MasterTableView ShowHeadersWhenNoRecords="true" EnableHeaderContextMenu="false" CommandItemDisplay="None">
                     <NoRecordsTemplate>
                        <div>There are no records to display</div>
                    </NoRecordsTemplate>
                 <CommandItemSettings ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                       <Columns>
                   <telerik:GridTemplateColumn HeaderStyle-Width="120px" ItemStyle-Width="120px" AllowFiltering="false" HeaderText="RATING" UniqueName="Rating">
                                        <ItemTemplate>
                                              <telerik:RadRating RenderMode="Lightweight" ID="RadRating1" runat="server" AutoPostBack="false" Value='<%# Convert.ToDouble(Eval("Rating1")) %>'
                                Precision="Exact" ReadOnly="true">
                            </telerik:RadRating>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                     </Columns> 
     <%-- <PagerStyle AlwaysVisible="true" Position="Top"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>--%>
                    </MasterTableView>
                  <ItemStyle Wrap="false" CssClass="font"/>   
             <AlternatingItemStyle Wrap="false" CssClass="font"/>
                 <HeaderStyle Wrap="false" Width="100px" BackColor="CadetBlue" ForeColor="White"  CssClass="fontbold" HorizontalAlign="Center"   />
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False">
                            <Selecting AllowRowSelect="True"/>
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                            <Scrolling AllowScroll="true"   UseStaticHeaders="true" SaveScrollPosition="true" />
                     </clientsettings>
         </telerik:RadGrid>



      </div>
      </div>
    </div>

  </div>

  
   </form>
</div>

</asp:Content>

