 <%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="bdc_inv_Resultpop.aspx.vb" Inherits="_Default" %>

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
select[disabled]
{
    width:160px !important;
    }
textarea[disabled] {width: 97%;}
</style>
<link rel="stylesheet" type="text/css" href="css/style.css" />

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
    function openModal() {
        $('#addnewpop1').modal('show');
    }
</script>

<div class="candidate_rounnd_details">
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

    <div class="widget-box" style="margin-top: 10px;">
     <div class="widget-box">
        <div class="widget-title" style="margin-top: 0px;"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Candidate-Round Info</h5>   
             <div class="" style="float: right;margin: 4px 10px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">            
               <asp:Button ID="Button1" Enabled="false" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"  Enabled="true"   runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" style="margin-right: 5px;" ToolTip ="Save"/>
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />

            </div>      
    </div> </div> 

    <div class="widget-content">
<div class="row-fluid">
          <div class="span12 choose-result">
               <div class="span3">
               <label class="control-label-list">Name:</label>
                  <div class="controls-list highlight"><asp:Label ID="lblName" runat="server"></asp:Label></div>
               </div>
               <div class="span3">
                 <label class="control-label-list">Phone:</label>
                  <div class="controls-list highlight"><asp:Label ID="Label2" runat="server"></asp:Label></div>
               </div>
               <div class="span3" style="margin-left: 0px; margin-top: 5px;">
                     <label class="control-label-list"> Email:</label><asp:Label ID="lblemail" runat="server"></asp:Label>
               </div>

                <div class="span3" style="margin-left: 0px; margin-top: 5px;">
                   <label class="control-label-list">  Vacancy: </label><asp:Label ID="lblVac" runat="server"></asp:Label>
               </div>
          </div>
      </div>
<div class="row-fluid">
          <div class="span12 choose-result">
           <div class="span4">
   <label class="control-label-list">Choose Round: <span style="color:Red;">*</span></label>
     <div class="controls-list">
                               <asp:DropDownList ID="ddl_Round" AppendDataBoundItems="true" class="span12"  runat="server">
                               <asp:ListItem Value="" >Choose Round</asp:ListItem>
                              </asp:DropDownList>
                                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                              runat="server"  InitialValue=""  ErrorMessage="Choose Round" ControlToValidate="ddl_Round" 
                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                         </div>
                  
               </div>

<div class="span4">
                   <label class="control-label-list">Conducted By:<span style="color:Red;">*</span> </label>
                         <div class="controls-list">
                               <asp:DropDownList ID="ddlConduct" AppendDataBoundItems="true" class="span12"  runat="server">
                               <asp:ListItem Value="" >Choose Conducted By</asp:ListItem>
                              </asp:DropDownList>
                                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                              runat="server"  InitialValue=""  ErrorMessage="Conducted by required" ControlToValidate="ddlConduct" 
                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                         </div>
              </div>

              <div class="span4">
                
                         <label class="control-label-list" >Conducted On: <span style="color:Red;">*</span></label>
                         <div class="controls-list" >
                            
                              <telerik:RadDateTimePicker ID="dt_schedule" class="span12" runat="server"
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


                                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validationposition"
                              runat="server"  InitialValue=""  ErrorMessage="Schedule date required" ControlToValidate="dt_schedule" 
                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                         </div>
                 
               </div>
 

 

             </div>  </div>
          
          
          
          
          <div class="row-fluid">
          <div class="span12 choose-result">
          <div class="span4">
     <label class="control-label-list">Choose Result: <span style="color:Red;">*</span></label>
      <div class="controls-list">
       <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="true" class="span12"  runat="server"  AutoPostBack="true" >
        <asp:ListItem Value="" >Choose Result</asp:ListItem>
        </asp:DropDownList> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
           runat="server"  InitialValue=""  ErrorMessage="Choose Result" ControlToValidate="DropDownList1" 
            ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
              </div>
                    </div>

                    <div class="span4" id="pnl1" runat="server" >
                    
                         <label class="control-label-list">Next Round Date: <span style="color:Red;">*</span></label>
                         <div class="controls-list">
                            
                              <telerik:RadDateTimePicker ID="dt_schedule2" class="span12 control" runat="server"
                                           MinDate="2010-01-01" DateInput-DateFormat="dd-MMM-yyyy" 
                              Culture="en-US" ResolvedRenderMode="Classic"  >
        <TimeView CellSpacing="-1" Interval="00:01:00" ></TimeView>

                                           <TimePopupButton Visible="false" />
                                            <Calendar ID="Calendar3" runat="server">
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


                                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validationposition"
                              runat="server"  InitialValue=""  ErrorMessage="Schedule date required" ControlToValidate="dt_schedule2" 
                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                       
                    </div>
               </div>


               <div class="span4" id="pnl2" runat="server">
                  
                         <label class="control-label-list">Schedule For:<span style="color:Red;">*</span> </label>
                         <div class="controls-list" >
                               <asp:DropDownList ID="ddl_roundby2" AppendDataBoundItems="true" class="span12"  runat="server">
                               <asp:ListItem Value="" >Choose Schedule For</asp:ListItem>
                              </asp:DropDownList>
                                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validationposition"
                              runat="server"  InitialValue=""  ErrorMessage="Schedule for required" ControlToValidate="ddl_roundby2" 
                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                       
                    </div>
               </div>
          
      
              </div></div>
           
            <div class="row-fluid"  id="pnlReason" runat="server" visible=false >
          <div class="span12 choose-result">

          
     <div id="Div1" class="span4" runat="server" >
                         <label class="control-label-list">Choose Reason:<span style="color:Red;">*</span> </label>
                         <div class="controls-list">
                               <asp:DropDownList ID="ddlReason" AppendDataBoundItems="true" class="span12"  runat="server">
                               <asp:ListItem Value="" >Choose Reason</asp:ListItem>
                              </asp:DropDownList>
                                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                              runat="server"  InitialValue=""  ErrorMessage="Choose Reason" ControlToValidate="ddlReason" 
                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                     
                    </div>
               </div>
               
          </div>
          </div>
        <div class="row-fluid">
          <div class="span12 choose-result">
     <label class="control-label-textarea" >Feedback:<span style="color:Red;">*</span></label>
      <div class="controls-textarea">
         <asp:TextBox ID="TextBox1" TextMode="MultiLine"   onkeypress="return IsAlphaNumeric(event);" placeholder="Feedback" runat="server"
                                     MaxLength="1000" onblur="textBoxOnBlur(this);" style="resize:none;"></asp:TextBox>
                               <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                                   runat="server"  InitialValue=""  ErrorMessage="Feedback is required" ControlToValidate="TextBox1" 
                                      ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                         </div>
                    </div>
              
          </div>
       </div> </div>

      <div class="row-fluid">
       <div class="span12" style="margin-bottom:10px;margin-top:0px; ">

       <telerik:RadGrid ID="RadGrid1"  EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="True" CellSpacing="0" GridLines="none"    
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="false"  runat="server" Height="200px"  Width="100%"  >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" DataKeyNames="LOGID,CANDID"  NoMasterRecordsText="No records to display" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
          <%--          <div style="padding: 5px 5px;">
                      
                        <asp:LinkButton ID="LinkButton4" Visible="False" runat="server" CommandName="refresh"><img style="display:None;border:0px;vertical-align:middle;" alt="" src="Images/refresh-grey-btn.jpg" /></asp:LinkButton>
                    <a id="addlink" runat="server"  href="employer_det.aspx"  class="pop3" style="cursor: pointer; float: right;"><img style="border:0px;vertical-align:middle;" alt="" src="Images/add-new-record-solid.png"/></a> &nbsp;&nbsp;
                    

                       <telerik:RadButton ID="BuiltinIconsButton2" Visible="false" CommandName="allowfilter"  runat="server" ButtonType="ToggleButton"
                                ToggleType="CustomToggle"  EnableViewState="true" AutoPostBack="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/line.png"  Value="T"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/hue.png" selected Value="F"></telerik:RadButtonToggleState>
                        </ToggleStates>
                        
                    </telerik:RadButton>

               <asp:DropDownList ID="ddl_Employer" width="200px" Visible="False" style="margin-bottom:-3px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="0" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;       
                  
<asp:LinkButton ID="LinkButton1" Visible="False" runat="server" CommandName="EmployerFilter"><img src="images/refresh16x16.png" style="display:none;" border="0" alt="" /></asp:LinkButton>
                      <div class="button" style="float:right;display:none;">
                       <a id="add-event"  data-toggle="modal" href="#addnewrecord" class="btn btn-mini"><img style="border:0px;vertical-align:middle; float:right" alt="" src="images/add-new-record-solid.jpg"/></a>
                      </div>
                    </div>--%>
                    
                </CommandItemTemplate>
              <%-- <ColumnGroups>
                            <telerik:GridColumnGroup Name="GeneralInformation" HeaderText="General Information"
                                HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridColumnGroup Name="SpecificInformation" HeaderText="Specific Information"
                                HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridColumnGroup Name="BookingInformation" HeaderText="Booking Information"
                                HeaderStyle-HorizontalAlign="Center" />
                        </ColumnGroups>--%>
                        <Columns>
                                 <%--    <telerik:GridTemplateColumn UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                      <asp:ImageButton ID="btnedit"   runat="server"  Width="16" border="0" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnint"  Width="20"  runat="server"  border="0" />&nbsp;&nbsp;
                              <asp:ImageButton ID="btnjob"   runat="server" Width="20" border="0" />&nbsp;&nbsp;
                      
                            <asp:ImageButton ID="btnget"  runat="server" Width="16"  border="0" />&nbsp;&nbsp;                            
                             </ItemTemplate>  </telerik:GridTemplateColumn>
                           --%>                                             



                   <%--         <telerik:GridTemplateColumn UniqueName="res"  HeaderText="Resume">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowResume" runat="server"  Text="View Resume" CommandName="viewaddresume" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                          
                
                        </Columns>
            <%-- <CommandItemSettings AddNewRecordText="Add new record" AddNewRecordImageUrl="Images/AddRecord.png"
                    RefreshText="Refresh" RefreshImageUrl="Images/Refresh.png"></CommandItemSettings>--%>
                   <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle BackColor="#d4d2d2" Font-Size="12px"  Font-Bold="true" Wrap="false" Font-Names="Arial"/>
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                        </clientsettings>
            </telerik:RadGrid>


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
        <button type="button" style="display: none;" id="btnShowPopup1" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewpop1">
                Launch demo modal
            </button> 
            
    </form>
</div>
  </div>
    
</asp:Content>

