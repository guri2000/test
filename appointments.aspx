<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="appointments.aspx.vb" Inherits="Default2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<style>
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.modal {
    position: fixed;
    top: 1%;
    left: 5%;
    right:5%;
    z-index: 1050;
    width: 90%;
 
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.font
{
    font-family:Arial;
    font-size:11px;
  
}
.modal-body{ padding: 0px !important;
overflow-y: hidden !important;
}

</style>

<%--<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  <link rel="stylesheet" type="text/css" href="fancy/jquery.fancybox.css"
        media="screen" />--%>
        
<script type="text/javascript">
    function ShowPopup() {
       
     $(document).ready(function () {
         $("#btnShowPopup").click();
         Setgrid();
    });
}
function Setgrid() {


    var hght = screen.height;
    var hght1 = screen.height - 230;
	var hght3 = screen.height - 400;
    var grd = document.getElementById('<%=RadGrid1.ClientID %>');
    grd.style.height = hght1 + "px";
    var frm = document.getElementById('<%=Ifrmfollowup.ClientID %>');
    var hght2 = hght - (hght * 26 / 100);
    frm.height = hght2 + "px";
	
	
    $(".modal").css('height', hght3);
    $(".modal-body").css('max-height', hght3);
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
        $('#addnewfollowup').modal('show');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="" style="margin-left: 2px; margin-right: 5px;">


<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>

    <div class="row-fluid" style="margin-top: 10px;">
      <div class="span12">
       
     
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List of Records:    </h5><asp:DropDownList ID="ddl_Category" width="200px" style="margin-bottom:0px; margin-top:0px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;
                  
                 <span style="font-size:11px; font-weight: 600;">Next Followup Period:</span> &nbsp;            <telerik:RadDatePicker ID="nxtdt" class="span12 control" runat="server"
                                   MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy" 
                      Culture="en-US" ResolvedRenderMode="Classic"  >

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
                                </telerik:RadDatePicker>
                                 <asp:RequiredFieldValidator ControlToValidate="nxtdt" ID="RequiredFieldValidator1" ValidationGroup="Valid1" 
                                CssClass="errormesg" ErrorMessage="Please Select From  Date " ForeColor="Red"  
                                InitialValue="" runat="server"  Display="Dynamic">
                                </asp:RequiredFieldValidator>        
                                &nbsp; &nbsp; 
                                           <telerik:RadDatePicker ID="nxtdate21" class="span12 control" runat="server"
                                   MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy" 
                      Culture="en-US" ResolvedRenderMode="Classic"  >

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
                                </telerik:RadDatePicker>
                                   <asp:RequiredFieldValidator ControlToValidate="nxtdate21" ID="RequiredFieldValidator2" ValidationGroup="Valid1" 
                                CssClass="errormesg" ErrorMessage="Please Select To Date " ForeColor="Red"  
                                InitialValue="" runat="server"  Display="Dynamic">
                                </asp:RequiredFieldValidator>        
                  &nbsp; &nbsp; 
                    <asp:ImageButton ID="Button2" OnClientClick="if(Page_ClientValidate('Valid1')) ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png" Width="18px" Height="18px"  CausesValidation="true" />
           <%--      <a id="upload_excel1"  data-toggle="modal" href="#upload_excel" class="btn_add" style="float: right;">
                      <i class="icon-plus-sign-alt"></i> Load File           
                 </a> 
                 <a id="add-event"  data-toggle="modal" href="#addnewrecord" class="btn_add" style="float: right;">
                      <i class="icon-plus-sign-alt"></i> Add New                 
                 </a>

                       <a id="sendbulkmail1"   runat="server"  data-toggle="modal" href="#sendbulkmail" class="btn_add" style="float: right;">
                      <i class="icon-plus-sign-alt"></i> Mass Mailing    
                 </a>
                       <a id="sendbulkmsg1"  runat="server" data-toggle="modal" href="#sendbulkmsg" class="btn_add" style="float: right;">
                      <i class="icon-plus-sign-alt"></i> Mass SMS     
                 </a>--%>

          </div>
           <div class="widget-content nopadding">
          <asp:Panel ID="Panel1" runat="server" Visible="false">
                  <div class="alert alert-info" >
              <button class="close" data-dismiss="alert">×</button>
              <strong></strong> 
               <asp:Label ID="lbl_EmployerFilter" runat="server" ></asp:Label> </div>
               </asp:Panel>
            <telerik:RadGrid ID="RadGrid1" EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered"  
            AllowPaging="True" CellSpacing="0" GridLines="None"    
             AllowSorting="True" ShowHeader="true" PageSize="25" AllowFilteringByColumn="true"  runat="server">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView DataKeyNames="EMPID" CommandItemDisplay="Top" NoMasterRecordsText="No records to display" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    <div style="padding: 1px 5px; display:none;">
                      
                        <asp:LinkButton ID="LinkButton4" Visible="False" runat="server" CommandName="refresh"><img style="display:None;border:0px;vertical-align:middle;" alt="" src="Images/refresh-grey-btn.jpg" /></asp:LinkButton>
                      <%-- <a id="addlink" runat="server"  href="employer_det.aspx"  class="pop3" style="cursor: pointer; float: right;"><img style="border:0px;vertical-align:middle;" alt="" src="Images/add-new-record-solid.png"/></a> &nbsp;&nbsp;
                      --%>

                       <telerik:RadButton ID="BuiltinIconsButton2" Visible="false" CommandName="allowfilter"  runat="server" ButtonType="ToggleButton"
                                ToggleType="CustomToggle"  EnableViewState="true" AutoPostBack="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/line.png"  Value="T"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/hue.png" selected Value="F"></telerik:RadButtonToggleState>
                        </ToggleStates>
                        
                    </telerik:RadButton>

              
                  
                        
                  
<asp:LinkButton ID="LinkButton1" Visible="False" runat="server" CommandName="EmployerFilter"><img src="images/refresh16x16.png" style="display:none;" border="0" alt="" /></asp:LinkButton>
                      <div class="button" style="float:right; display:none;">
                       <%--<a id="add-event"  data-toggle="modal" href="#addnewrecord" class="btn btn-mini"><img style="border:0px;vertical-align:middle; float:right" alt="" src="images/add-new-record-solid.jpg"/></a>--%>
                      </div>
                     
                    
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


                          <telerik:GridTemplateColumn UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="40px" ItemStyle-Width="40px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:ImageButton ID="btnint" style="cursor:default" Width="0"  runat="server"  border="0" Enabled="false" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnget" OnClientClick="ShowProgress();" runat="server" Width="16"  border="0" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnedit"   runat="server"  Width="0" border="0" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnjob" Visible="false"  runat="server" Width="0" border="0" />&nbsp;&nbsp;
                           
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                           <telerik:GridTemplateColumn UniqueName="chk" AllowFiltering="false" HeaderStyle-Width="0" ItemStyle-Width="0" >
                            <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
							  <HeaderTemplate>
                         
                          </HeaderTemplate>
                            </telerik:GridTemplateColumn>
                         
                        </Columns>
            <%-- <CommandItemSettings AddNewRecordText="Add new record" AddNewRecordImageUrl="Images/AddRecord.png"
                    RefreshText="Refresh" RefreshImageUrl="Images/Refresh.png"></CommandItemSettings>--%>

                   <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="11px"  Font-Bold="true" Wrap="false" CssClass="grid_header" ForeColor="White" Font-Names="Arial"/>
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                        </clientsettings>
            </telerik:RadGrid>



                          <div class="modal hide" id="addnewrecord">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3>New Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm"  frameborder="0" width="100%" height="400px" src="coldb_det.aspx"></iframe>
                </div>
              </div>
                <div class="modal hide" id="upload_excel">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3>Upload File</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Iframe1"  frameborder="0" width="100%" height="400px" src="upload_excel.aspx"></iframe>
                </div>
              </div>

                <div class="modal hide" id="sendbulkmail">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3>Send Bulk Email</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Iframe2"  frameborder="0" width="100%" style="background: #fff;" height="550px" src="send_email01.aspx?fsn=1&mod=any&tmp=200&emp=159"></iframe>
                </div>
              </div>

                <div class="modal hide" id="sendbulkmsg">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3>Send Bulk Message</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Iframe3"  frameborder="0" width="100%" height="400px" src="#"></iframe>
                </div>
              </div>


              <div class="modal hide"  id="addnewfollowup">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Ifrmfollowup"  frameborder="0" width="100%"  ></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>

              <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewfollowup">
                Launch demo modal
            </button>    
        </div>
      </div>
    </div>
    <%--</form>--%>
  </div>



    </div>
</asp:Content>

