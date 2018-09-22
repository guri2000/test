<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="bdc_inv_Vacpop.aspx.vb" Inherits="_Default" %>

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
<style type="text/css">
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.riContSpinButtons {
    padding-right: 50px !important;
} 
.fb-group .riSingle{
    display: inline-block !important;
}
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
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <div class="widget-title" style="margin-top: -5px;"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Vacancy-Info</h5>
         
             <div class="" style="float: right;margin: 4px 10px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">            
               <asp:Button ID="Button1" Enabled="false" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"  Enabled="true"   runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" style="margin-right: 5px;" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />

            </div>
                    <b><Label ID="status" class="" runat="server"></Label> </b>
             
        </div>
        <div class="row-fluid widget-content">
         <div class="control-group">  
        <div class="span12">
            <div class ="control-group ">
        <label class="control-label" style="width: 100px;">Vacancy Title: <span style="color:Red;">*</span> </label>
      <div class="controls" style="margin-left: 110px;">
       <asp:TextBox ID="txtVname" onkeypress="return IsAlphaNumeric(event);"   class="span12" style="width:97%;"  placeholder="Vacancy Title" runat="server"
                          onblur="textBoxOnBlur(this);" ></asp:TextBox>
                             <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Vacany Name is required" ControlToValidate="txtVname" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
   
      </div>
      
      </div>
      
      </div>


      
      </div>


         <div class="control-group">  

         <div class="span2">
            <div class ="control-group ">
        <label class="control-label" style="width: 100px;">No. of Opening:<span style="color:Red;">*</span> </label>
      <div class="controls" style="width: 100%; margin-left: 110px;">

      <telerik:RadNumericTextBox RenderMode="Lightweight" ShowSpinButtons="true" CssClass="span12"  placeholder="Number of Opening" MinValue="0" MaxValue="100"   IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" 
               runat="server" ID="txtOpening">
                 <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
            </telerik:RadNumericTextBox>

                             <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Number of Opening required" ControlToValidate="txtkskill" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    
      </div>
      
      </div>
      
      </div>

      <div class="span4" style="margin-left: 30px;">         
              <div class="control-group">
              <label class="control-label" style="width: 100px;">Location: <span style="color:Red;">*</span> </label>
              <div class="controls" style="margin-left: 110px;">               
              
                   <asp:DropDownList ID="DDLOC" class="span12"  runat="server">
                  
                    
                  </asp:DropDownList>

                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Location is required" ControlToValidate="DDLOC" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
         
           </div>

           <div class="span3" style="margin-left: 0px;">
               <div class="control-group">
              <label class="control-label" style="width: 100px;"> Opening Date:<span style="color:Red;">*</span> </label>
              <div class="controls" style="margin-left: 110px;">
                
                 <telerik:RadDateTimePicker ID="dt_open" class="span12 control" runat="server"
                                   MinDate="2010-01-01" DateInput-DateFormat="dd-MMM-yyyy" 
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
                  runat="server" ErrorMessage="Vacancy Opening Date required" ControlToValidate="dt_open" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    
                   
              </div>
            </div>
           </div>

           <div class="span3" style="margin-left: 0px;">
               <div class="control-group">
              <label class="control-label" style="width: 130px;"> Tentative Closing Date:<span style="color:Red;">*</span> </label>
              <div class="controls" style="margin-left: 140px;">
                
                 <telerik:RadDateTimePicker ID="dt_close" class="span12 control" runat="server"
                                   MinDate="2010-01-01" DateInput-DateFormat="dd-MMM-yyyy" 
                      Culture="en-US" ResolvedRenderMode="Classic"  >
<TimeView CellSpacing="-1" Interval="00:01:00" ></TimeView>

                                   <TimePopupButton Visible="false" />
                                    <Calendar ID="Calendar2" runat="server">
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
                 <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                  runat="server" ErrorMessage="Closing Date required" ControlToValidate="dt_close" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    
                   
              </div>
            </div>
           
           
           </div>

       
         
         <div class="span12" style="margin-left: 0px;">
            <div class ="control-group ">
        <label class="control-label" style="width: 100px;">Key Skills: <span style="color:Red;">*</span></label>
      <div class="controls" style="margin-left: 110px;">
       <asp:TextBox ID="txtkskill" textmode="MultiLine"  onkeypress="return IsAlphaNumeric(event);"   class="span12" style="height: 101px !important; resize:none; width:97%;"  placeholder="Key skills" runat="server"
                          onblur="textBoxOnBlur(this);" ></asp:TextBox>
                             <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Key skill is required" ControlToValidate="txtkskill" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    
      </div>
      
      </div>
      
      </div>
            <div class="span12" style="margin-left: 0px;">
             <div class="control-group">
              <label class="control-label" style="width: 100px;">Remarks:<span style="color:Red;">*</span></label>
              <div class="controls" style="margin-left: 110px;">
              <asp:TextBox ID="TextBox10" style="height: 101px !important; resize:none; width:97%;" onkeypress="return IsAlphaNumeric(event);" 
                    TextMode="MultiLine" class="span12" placeholder="Remarks" runat="server"
                     MaxLength="2000" onblur="textBoxOnBlur(this);"></asp:TextBox>
                       <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Remarks is required" ControlToValidate="TextBox10" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
               
              </div>
            </div>
           
         </div>

          <div class="span12" style="margin-left:0px;">         
              <div class="control-group">
              <label class="control-label" style="width: 100px;">Scope: <span style="color:Red;">*</span></label>
              <div class="controls" style="margin-left: 110px;">               
              
                   <asp:DropDownList ID="ddl_scope" class="span7"  runat="server">                  
                    
                  </asp:DropDownList>

                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Scope is required" ControlToValidate="ddl_scope" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>
         
           </div>
     </div>

         
           <div class="span12" style="margin-left: 0px;display:none;">
             <div class="control-group">
              <label class="control-label" style="width: 110px;">Adv Image:</label>
              <div class="controls" style="margin-left: 120px;"> 
                   <asp:FileUpload ID="fupl" runat="server" />
                   <%--<asp:Label ID="lblFname" runat="server" vi></asp:Label>  --%>

                    <asp:TextBox ID="lbl_Fname" runat="server" Visible="false"   style="vertical-align: bottom;color:#215165;background-color: #f9f9f9;width: 58%; margin-left:-50px;" ReadOnly="true"   BorderStyle="None"></asp:TextBox> &nbsp;&nbsp;


              </div>
            </div>
        
           </div>

        </div>
      </div>
     

     

        <div class="modal hide" id="addnewrecord">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3>Category Record</h3>
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
                  <h3>Sub-Category Record</h3>
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
              .
              <button type="button" style="display: none;" id="btnShowPopup1" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewpop1">
                Launch demo modal
            </button> 
    </form>
</div>

</asp:Content>

