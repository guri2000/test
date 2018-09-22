<%@ Page Title="" Language="VB"  MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BDC_Candidate.aspx.vb" Inherits="Default2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style>
        
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.modal {
    position: fixed;
    top: 15%;
    left: 5%;
    right:5%;
    z-index: 1050;
    width: 90%;
    height:400px;
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.modal-body{max-height: 400px !important; padding: 0px !important;
overflow-y: hidden !important;}

.cls-res {color: #bd1920 !important; font-size: 11px; text-decoration: underline;}
.cls-res:hover {color: #a10d13 !important;}


</style>
<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  <link rel="stylesheet" type="text/css" href="fancy/jquery.fancybox.css"
        media="screen" />
        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-left:2px; margin-right:5px;">
  
    <%--<form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
       <%-- <script type="text/javascript">
            $(document).ready(function () {
            Ifrmfollowup.src="foolowuplist.aspx"
                $("#addnewfollowup").modal('show');

            });
</script>
        <script type="text/javascript" >
             function onfollow() {
                 
                  alert(idx.get_commandName());
                Ifrmfollowup.src = "followup_list.aspx?empid=" + empid;
                alert(Ifrmfollowup.src);
            }
</script>--%>
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
        var grd = document.getElementById('<%=RadGrid1.ClientID %>');
        grd.style.height = hght1 + "px";
       
   
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
 <telerik:RadWindowManager ID="RadWindowManager1" runat="server" >
    </telerik:RadWindowManager>

    <div class="row-fluid" style="margin-top: 10px;">
      <div class="span12">
       
     
        <div class="widget-box">
         <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List of Records:    </h5><asp:DropDownList ID="ddl_Employer" width="200px" style="margin-bottom:-3px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="" >Show All</asp:ListItem>
                   <asp:ListItem Value="1" >Candidate in Poll</asp:ListItem>
                      <asp:ListItem Value="2" >ShortListed Candidate</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="Button2"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png" style="margin-top: 5px;"   CausesValidation="False" /> 
          </div>
           <div class="widget-content nopadding">

            <telerik:RadGrid ID="RadGrid1" EnableViewState="true"   Skin="Metro" PageSize="18"   CSSClass="table table-bordered" 
            AllowPaging="True" CellSpacing="0" GridLines="none"  
             AllowSorting="True" ShowHeader="true"     AllowFilteringByColumn="True"  runat="server" >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView DataKeyNames="CANDID" NoMasterRecordsText="No records to display" CommandItemDisplay="Top" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate >
                    <div style="padding: 1px 5px;display:none;">
                      
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="refresh"><img style="border:0px;vertical-align:middle;width:96px;" alt="" src="Images/refresh-grey-btn.jpg"/></asp:LinkButton>
                      <%-- <a id="addlink" runat="server"  href="employer_det.aspx"  class="pop3" style="cursor: pointer; float: right;"><img style="border:0px;vertical-align:middle;" alt="" src="Images/add-new-record-solid.png"/></a> &nbsp;&nbsp;
                      --%>
                      
                      <div class="button" style="float:right;">
                       <a id="add-event"  data-toggle="modal" href="#addnewrecord" ><img style="border:0px;vertical-align:middle; float:right;width:100px;" alt="" src="images/add-new-record-solid.jpg"/></a>
                      </div>
                     
                    
                </CommandItemTemplate>
               <ColumnGroups>
                            <telerik:GridColumnGroup Name="GeneralInformation" HeaderText="General Information"
                                HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridColumnGroup Name="SpecificInformation" HeaderText="Specific Information"
                                HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridColumnGroup Name="BookingInformation" HeaderText="Booking Information"
                                HeaderStyle-HorizontalAlign="Center" />
                        </ColumnGroups>
                        <Columns>
                         

                           <telerik:GridTemplateColumn UniqueName="res"  HeaderText="RESUME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowResume" runat="server"  Text="View Resume" CommandName="viewaddresume" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>


                     
                        </Columns>
            <%-- <CommandItemSettings AddNewRecordText="Add new record" AddNewRecordImageUrl="Images/AddRecord.png"
                    RefreshText="Refresh" RefreshImageUrl="Images/Refresh.png"></CommandItemSettings>--%>
                      <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
                 <%--<PagerTemplate>
                     <telerik:RadComboBox ID="RadComboBox1" runat="server">
                     </telerik:RadComboBox>
                 </PagerTemplate>--%>
                   </MasterTableView>
                  
                  <ItemStyle Wrap="false" CssClass="font"/> 
                  <AlternatingItemStyle Wrap="false" CssClass="font"/>
                 <HeaderStyle Font-Size="12px" Font-Names="Arial"  Font-Bold="true"  />
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
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;font-family:Arial;">Add New Vacancy Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm" scrolling="no"  frameborder="0" width="100%" height="400px" src="bdc_Inv_Vacpop.aspx"></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>

              <div class="modal hide"  id="addnewfollowup">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 runat="server" id="edtti" style="font-size:14px;font-style:normal;font-weight:600;font-family:Arial;">Edit Agent Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Ifrmfollowup" scrolling="no" frameborder="0" width="100%" height="400px"  ></iframe>
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
    </div>
    <%--</form>--%>
  </div>
<%-- <script src="js/jquery.min.js"></script> 
<script src="js/jquery.ui.custom.js"></script>
<script src="js/bootstrap.min.js"></script> 
<script src="js/jquery.uniform.js"></script> 
<script src="js/select2.min.js"></script> 
<script src="js/jquery.dataTables.min.js"></script> 
<script src="js/matrix.js"></script> 
<script src="js/matrix.tables.js"></script>--%> 


</asp:Content>

