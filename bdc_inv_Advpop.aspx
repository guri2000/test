<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="bdc_inv_Advpop.aspx.vb" Inherits="_Default" %>

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

<style>
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
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
          <h5>Advertisement-Info</h5>
         
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
              <div class="span6" style="margin-left: 0px;">
               <div class="control-group">
                      <label class="control-label" style="width: 110px;">Choose Vacancy:<span style="color:Red;">*</span> </label>
                      <div class="controls" style="margin-left: 120px;">
                  <div  style="margin-left: 0px;">
                      <asp:DropDownList ID="ddl_Vacancy"  class="span12"  runat="server">
                  
                          </asp:DropDownList>

                           <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Vacancy is required" ControlToValidate="ddl_Vacancy" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                          </div>
                  </div>
                  </div>
                  </div>
          </div>


             <div class="control-group">
                  <div class="span4" style="margin-left: 0px;">
                     <div class="control-group">
                      <label class="control-label" style="width: 110px;">Approved By:<span style="color:Red;">*</span> </label>
                      <div class="controls" style="margin-left: 120px;">
             

                                 <asp:DropDownList ID="ctr_dtAppBy"  class="span12"  runat="server">
                  
                          </asp:DropDownList>

                          <br />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                          runat="server"  InitialValue=""  ErrorMessage="Approved  by required" ControlToValidate="ctr_dtAppBy" 
                            ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                      </div>
                    </div>
                  </div>
                  <div class="span4" style="margin-left: 0px;">
               <div class="control-group">
              <label class="control-label" style="width: 100px;">Adv. Date: <span style="color:Red;">*</span> </label>
              <div class="controls" style="margin-left: 110px;">
                
                 <telerik:RadDateTimePicker ID="ctr_dtadv" class="span12 control" runat="server"
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
                  runat="server" ErrorMessage="Advertisement Date is required" ControlToValidate="ctr_dtadv" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    
                   
              </div>
            </div>
           </div>
                  <div class="span4" style="margin-left: 0px;">
                       <div class="control-group">
                      <label class="control-label" style="width:110px" > Approval Date:<span style="color:Red;">*</span>  </label>
                      <div class="controls" style="margin-left: 120px;">
                
                         <telerik:RadDateTimePicker ID="ctr_appDt" class="span12 control" runat="server"
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
                          runat="server" ErrorMessage="Approval Date is required" ControlToValidate="ctr_appDt" 
                            ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    
                   
                      </div>
                    </div>
           
           
                   </div>
             </div>
             <div class="control-group">
                  <div class="span4" style="margin-left: 0px;">

                    <div class="control-group">
              <label class="control-label" style="width: 110px;">Currency:<span style="color:Red;">*</span>  </label>
              <div class="controls" style="margin-left: 120px;">
                
              
                   <asp:DropDownList ID="ddl_Curr" class="span12"  runat="server">
                  
                    
                  </asp:DropDownList>
                 
                 
              
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Currency is required" ControlToValidate="ddl_Curr" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
            </div>

           </div>
                  <div class="span4" style="margin-left: 0px;">
                      <div class="control-group">
                      <label class="control-label" style="width: 100px;">Expense:<span style="color:Red;">*</span> </label>
                      <div class="controls" style="margin-left: 110px;">
                                  <asp:TextBox ID="ctr_txtexpense" onKeyPress="return isNumberKey(event)"  onkeyup="limitCharLengthAddress(this)"   class="span12" placeholder="Expense" runat="server"
                                 MaxLength="9" onblur="textBoxOnBlur(this);"></asp:TextBox>

                            <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                          runat="server"  InitialValue=""  ErrorMessage="Expense is required" ControlToValidate="ctr_txtexpense" 
                            ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                      </div>
                    </div>
         
                   </div>
                   <div class="span4" style="margin-left: 10px;">
                      <div class="control-group">
                      <label class="control-label" style="width: 100px;">Province:<span style="color:Red;">*</span> </label>
                      <div class="controls" style="margin-left: 110px;">
                           <asp:DropDownList ID="ctr_dpProv" class="span12"  runat="server">
                          </asp:DropDownList>
                            <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                          runat="server"  InitialValue=""  ErrorMessage="Province is required" ControlToValidate="ctr_dpProv" 
                            ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                   </div>
             </div>


            <div class="span12" style="margin-left: 0px; margin-bottom: 3px;">
             <div class="control-group">
              <label class="control-label" style="width: 110px;">Adv. Description:<span style="color:Red;">*</span> </label>
              <div class="controls" style="margin-left: 120px;">
              <asp:TextBox ID="TextBox10" style="height: 101px !important; resize:none; width:95%;" onkeypress="return IsAlphaNumeric(event);" 
                    TextMode="MultiLine" class="span12" placeholder="Remarks" runat="server"
                     MaxLength="2000" onblur="textBoxOnBlur(this);"></asp:TextBox>
                       <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                  runat="server"  InitialValue=""  ErrorMessage="Description is required" ControlToValidate="TextBox10" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
               
              </div>
            </div>
           
         </div>


         
           <div class="span12" style="margin-left: 0px; margin-bottom: 15px;">
             <div class="control-group">
              <label class="control-label" style="width: 110px;">Adv Image:</label>
              <div class="controls" style="margin-left: 120px;"> 
                   <asp:FileUpload ID="fupl" runat="server" />
                   <%--<asp:Label ID="lblFname" runat="server" vi></asp:Label>  --%>

                    <asp:TextBox ID="lbl_Fname" runat="server" Visible="false"   style="vertical-align: bottom;color:#215165;background-color: #f9f9f9;width: 58%; margin-left:-50px;" ReadOnly="true"   BorderStyle="None"></asp:TextBox> &nbsp;&nbsp;
                    <br />
                    <label class="control-label" style="width:200px;margin-left:-100px;color:#c0c0c0" > Only jpg/png; Max file size 3mb;</label>

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

