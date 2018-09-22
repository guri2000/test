<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="colddb_det.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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
    height:200px;
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.modal-body{max-height: 200px !important; padding: 0px !important; overflow-y: hidden !important;}
label {
    display: inline !important;
    margin-bottom: 0px;
    font-size: 11px;
}
select, textarea, input[type="text"], input[type="password"], input[type="datetime"], input[type="datetime-local"], input[type="date"], input[type="month"], input[type="time"], input[type="week"], input[type="number"], input[type="email"], input[type="url"], input[type="search"], input[type="tel"], input[type="color"], .uneditable-input {
    padding: 1px 6px;
    font-size: 11px;
}
select, input[type="file"] {
    height: 28px;
    line-height: 28px;
}
.row-fluid [class*="span"] {min-height: 28px;}
select {
    background-color: #fff;
    border: 1px solid #ccc;
    border-radius: 3px !important;
}
html body .RadInput_Default .riTextBox, html body .RadInputMgr_Default {
    border-color: #ccc #ccc #ccc #ccc !important;
    background: #fff;
    color: #555 !important;
    font: 11px Arial !important;
    border-radius: 3px;
    height: 28px;
    padding: 2px 8px !important;
}
body {
    line-height: 0px;
}
form {
    margin-bottom: 15px !important;
}
.validationposition {
    line-height: 14px;
}
.form-margin {margin-top: -5px;}
.widget-title span.icon {
    padding: 9px 10px 9px 11px;
}
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="container-fluid">
    <form id="Form1"  runat="server" method="post" class="form-horizontal" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
		  <asp:ValidationSummary ID="vs" ValidationGroup="basicdetails" ShowMessageBox="true" ShowSummary="false" runat="server" />
  <div class="row-fluid" style="margin-top:1%;">
    <div class="widget-box" >      
      <div class="widget-content" style="padding-top:2px;">
         <div class="control-group">     
            <div class="span3">
                <label for="normal" class="control-label" style="text-align:left;">Owner: <span style="color:red"> * </span></label>
            </div>
            <div class="span3">
                <label for="normal" class="control-label" style="text-align:left;">Category: <span style="color:red"> * </span></label>
            </div>
            <div class="span3">
                <label for="normal" class="control-label" style="text-align:left;">Gen. Date: <span style="color:red"> * </span></label>
            </div>
            <div class="span3">
                  <div class="span12">
                        <div class="widget-title1" style="float: right;margin: 5px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
                                <button type="button" style="display: none;" id="Button3" class="btn btn-primary btn-lg"
                                    data-toggle="modal" data-target="#addnewpop1">
                                    Launch demo modal
                                </button>  
                                   <asp:Button ID="Button4" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
                                   <asp:ImageButton  ID="ImageButton2" OnClientClick="if(Page_ClientValidate('basicdetails')) ShowProgress();"    runat="server" ImageUrl="~/images/save20x20.png" Height="20" Width="20" ToolTip ="Save"  />
                                   <asp:ImageButton ID="ImageButton3"    runat="server" ToolTip="Reset" ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />
                        </div>
                  </div>
            </div>
         </div>
         <div class="control-group">     
            <div class="span3">           
              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                  <asp:DropDownList ID="SRCOWN" AppendDataBoundItems="true" CssClass="span12" runat="server">
                    <asp:ListItem Value="" >Select Owner</asp:ListItem>
                  </asp:DropDownList>
                  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="validationposition"
                  runat="server" Display="Dynamic" ValidationGroup="basicdetails" ErrorMessage="Owner is required" ControlToValidate="SRCOWN" 
                    ForeColor="Red" ></asp:RequiredFieldValidator>
                 </div>
          </div>
            <div class="span3">        
              <div class="control-label form-margin" style="text-align:left; padding-top:0px; padding-bottom:5px; width:100%;">
                  <asp:DropDownList ID="SRCCAT" AppendDataBoundItems="true" CssClass="span12" runat="server">
                    <asp:ListItem Value="" >Select Category</asp:ListItem>
                  </asp:DropDownList>
                  <br /><asp:RequiredFieldValidator ValidationGroup="basicdetails" ID="RequiredFieldValidator24" CssClass="validationposition"
                  runat="server" Display="Dynamic" ErrorMessage="Category is required" ControlToValidate="SRCCAT" 
                    ForeColor="Red" ></asp:RequiredFieldValidator>
                 </div>
          </div>
            <div class="span3">
           
              <div class="control-label form-margin" style="padding-top:0px; text-align:left; padding-bottom:0px;">
           
                                           <telerik:RadDateTimePicker ID="SRCGENDAT" class="span12 control" runat="server"
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
                  runat="server" Display="Dynamic" ValidationGroup="basicdetails" ErrorMessage="Gen. Date is required" ControlToValidate="SRCGENDAT" 
                    ForeColor="Red" ></asp:RequiredFieldValidator>
                 </div>
          </div>
            <div class="span3" style="display:none;">
                  <div class="row-fluid">
    <div class="span12">
    <div class="widget-title1" style="float: right;margin: 0px 0px 0px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
                    <button type="button" style="display: none;" id="btnShowPopup1" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewpop1">
                Launch demo modal
            </button>  
                <asp:Button ID="Button1" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"  OnClientClick="if(Page_ClientValidate('basicdetails')) ShowProgress();"   runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />
           

            </div>
</div>
</div>
          
                
                </div>                       
         </div>               
         
      </div>
    </div>
  </div>
  <div class="row-fluid" style="display: none">
      <div class="span12">
           <span id="error" style="color: Red; display: none"> ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? Special Characters not allowed</span>
      </div>
  </div>
  <div class="row-fluid">
      <div class="span12">
          <div class="widget-box">
               <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
                   <h5>Contacted-Info</h5>
               </div>
               <div class="widget-content" style="padding-top:2px;">
                    <div class="control-group">
                         <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Source Name: <span style="color:red"> * </span></label>
                         </div>
                         <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Contact Person1 Name: <span style="color:red"> * </span></label>
                         </div>
                         <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Contact Person2 Name: </label>
                         </div>
                      <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Mobile No. (Primary): <span style="color:red"> * </span></label>
                         </div>
                         <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Mobile. No. (Secondary): </label>
                         </div>
                    </div>
                    <div class="control-group">
                         <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:5px; width: 100%;">
                                   <asp:TextBox ID="SRCNAME" onkeypress="return RestrictSpace1(event);" class="span12" placeholder="Source name" runat="server"
                                          MaxLength="99" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                      <br /><asp:RequiredFieldValidator ValidationGroup="basicdetails" ID="RequiredFieldValidator2" CssClass="validationposition"
                                      runat="server" Display="Dynamic" ErrorMessage="Source Name is required" ControlToValidate="SRCNAME" 
                                        ForeColor="Red" ></asp:RequiredFieldValidator>
                              </div>
                         </div>
                         <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                                   <asp:TextBox ID="SRCCNTPRSN1" onkeypress="return RestrictSpace1(event);" class="span12" placeholder="Contact Peroson1 name" runat="server"
                                        MaxLength="99" onblur="textBoxOnBlur(this);"></asp:TextBox>  <br />
                                        <asp:RequiredFieldValidator ValidationGroup="basicdetails" ID="RequiredFieldValidator7" CssClass="validationposition"
                                      runat="server" Display="Dynamic" ErrorMessage="Contact Peroson1 Name is req." ControlToValidate="SRCCNTPRSN1" 
                                        ForeColor="Red" ></asp:RequiredFieldValidator>
                              </div>
                         </div>
                         <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                                   <asp:TextBox ID="SRCCNTPRSN2" onkeypress="return RestrictSpace1(event);" class="span12" placeholder="Contact Peroson2 name" runat="server"
                                         MaxLength="99" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                     
                              </div>
                         </div>
                         <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                                   <asp:TextBox ID="SRCMBL1" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  class="span12" placeholder="Primary Mobile Number" runat="server"
                                            MaxLength="20"  onblur="textBoxOnBlur(this);" ></asp:TextBox> <br />
                                      <asp:RequiredFieldValidator ValidationGroup="basicdetails" ID="RequiredFieldValidator20" CssClass="validationposition"
                                          runat="server" Display="Dynamic" ErrorMessage="Primary Mobile No is required" ControlToValidate="SRCMBL1" 
                                            ForeColor="Red" ></asp:RequiredFieldValidator>
                              </div>
                         </div>
                         <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                                   <asp:TextBox ID="SRCMBL2" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  class="span12" placeholder="Secondary Mobile Number" runat="server"
                                            MaxLength="20"  onblur="textBoxOnBlur(this);" ></asp:TextBox> <br />
                                      
                              </div>
                         </div>
                    </div>
                    <div class="control-group">
                         <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Email Id  (Primary): <span style="color:red"> * </span></label>
                         </div>
                          <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Email Id (Secondary): </label>
                         </div>
                         <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Phone No. (Primary): </label>
                         </div>
                        <div class="span2">
                             <label for="normal" class="control-label" style="text-align:left;">Phone No. (Secondary): </label>
                         </div>
                    </div>
                    <div class="control-group">
                         <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:5px; width: 100%;">
                                   <asp:TextBox ID="SRCEML1"  onmouseout="ValidateEmail(this)"  class="span12" placeholder="Email" runat="server" AutoPostBack="false" 
                                           MaxLength="50" onblur="textBoxOnBlur(this);"  ></asp:TextBox>
                                           <br />
                                    <asp:RequiredFieldValidator ValidationGroup="basicdetails" Display="Dynamic"  ID="RequiredFieldValidator8" CssClass="validationposition"
                                      runat="server" ErrorMessage="Email is required" ControlToValidate="SRCEML1" 
                                        ForeColor="Red" ></asp:RequiredFieldValidator> 
                                        <asp:RegularExpressionValidator   
                                          ID="CompareValidator1" Display="Dynamic" ValidationGroup="basicdetails" CssClass="validationposition"
                                      runat="server" ErrorMessage="Invalid Email Format" 
                                          ControlToValidate="SRCEML1"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="Red" ></asp:RegularExpressionValidator>
                              </div>
                         </div>
                          <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                                   <asp:TextBox ID="SRCEML2" onmouseout="ValidateEmail(this)"  class="span12" placeholder="Email" runat="server" AutoPostBack="false" 
                                           MaxLength="50" onblur="textBoxOnBlur(this);"  ></asp:TextBox>
                                           <br />
                        
                                        <asp:RegularExpressionValidator   
                                          ID="RegularExpressionValidator2" ValidationGroup="basicdetails" Display="Dynamic" CssClass="validationposition"
                                      runat="server" ErrorMessage="Invalid Email Format" 
                                          ControlToValidate="SRCEML2"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="Red" ></asp:RegularExpressionValidator>
                              </div>
                         </div>
                           <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                                   <asp:TextBox ID="SRCPHN1" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  class="span12" placeholder=" Primary Phone Number" runat="server"
                                         MaxLength="20" onblur="textBoxOnBlur(this);"></asp:TextBox>
                              </div>
                         </div>
                           <div class="span2">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:0px; width: 100%;">
                                   <asp:TextBox ID="SRCPHN2" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"  class="span12" placeholder=" Secondary Phone Number" runat="server"
                                         MaxLength="20" onblur="textBoxOnBlur(this);"></asp:TextBox>
                              </div>
                         </div>
                    </div>
               </div>
          </div>
      </div>
  </div>
  <div class="row-fluid">
      <div class="span12">
          <div class="widget-box">
               <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
                   <h5>Address-Info</h5>
               </div>
               <div class="widget-content" style="padding-top:2px;">
                    <div class="control-group">
                         <div class="span12">
                              <label for="normal" class="control-label" style="text-align:left;">Address: </label>
                         </div>
                        
                    </div>
                    <div class="control-group">
                         <div class="span12">           
                              <div class="control-label form-margin" style="text-align:left;padding-top:0px; padding-bottom:10px; width: 100%;">
                                    <asp:TextBox ID="SRCADD" style="height: 80px !important; resize:none;" onkeypress="return isAdd(event);" 
                                           TextMode="MultiLine" class="span12" placeholder="Address" runat="server" MaxLength="3000" onblur="textBoxOnBlur(this);"></asp:TextBox>
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