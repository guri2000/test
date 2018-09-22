<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="desgmaster_detail_list.aspx.vb" Inherits="Default2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style>
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.modal {
    position: fixed;
    top: 25%;
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
.font
{
    font-family:Arial;
    font-size:11px;
  
}
.modal-body{max-height: 300px !important; padding: 0px !important;
overflow-y: hidden !important;}
</style>
<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  <link rel="stylesheet" type="text/css" href="fancy/jquery.fancybox.css"
        media="screen" />
        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="" style="margin-left: 2px; margin-right:5px;">
  
   <%-- <form id="Form1" runat="server">
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
//    $(function () {
//        $(".close").on('click', function () {

//            $('#addnewrecord').modal('hide');
//            window.location.reload(true);


//        });
//    });
//    $('.modal.in').modal('hide') {
//        $(document).ready(function () {
//            alert("rahul");
//            document.getElementById("addnewfrm").contentDocument.location.reload(true)
//        });
//        }
    
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
    function openModal() {

        $('#addnewfollowup').modal('show');

    }
</script>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>

    <div class="row-fluid" style="margin-top: 10px;">
      <div class="span12">
       
     
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List of Records</h5>
          </div>
           <div class="widget-content nopadding">
         <%-- <div class="dataTables_filter" id="example_filter"><label>Search: <input type="text" aria-controls="example"></label></div>
          <div class="widget-content nopadding">
              <asp:GridView ID="GridView1" runat="server">
              <Columns>
              <asp:TemplateField>
              <HeaderTemplate>
               <table class="table table-bordered data-table">
              <thead>
                <tr>
                  <th>Employer Name</th>
                  <th>Company Name</th>
                  <th>Company Tel. No</th>
                  <th>Result</th>
                  <th>Contracted Date</th>
                </tr>
              </thead>
              <tbody>
              </HeaderTemplate>
              <ItemTemplate>
                   <tr class="gradeX">
                  <td><%#Eval("empname") %></td>
                  <td><%#Eval("empcmpnam") %></td>
                  <td><%#Eval("empcmptelno") %></td>
                  <td class="center"><%#Eval("empcontdt") %></td>
                    <td><%#Eval("result") %></td>
                </tr>
    
              </ItemTemplate>
              <FooterTemplate>
              </tbody>
            </table>
              </FooterTemplate>
              </asp:TemplateField>
              
              
              </Columns>
              </asp:GridView>
           
           
              
          </div>--%>
          
            <telerik:RadGrid ID="RadGrid1" EnableViewState="true"   Skin="Metro" PageSize="20"   CSSClass="table table-bordered" 
            AllowPaging="True" CellSpacing="0" GridLines="none"  
             AllowSorting="True" ShowHeader="true"   AllowFilteringByColumn="true"  runat="server">
              <GroupingSettings CaseSensitive="false"  />
              <MasterTableView DataKeyNames="desgid" NoMasterRecordsText="No records to display" CommandItemDisplay="Top" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    <div style="padding: 1px 5px;">
                      
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="refresh"><img style="border:0px;vertical-align:middle;width:96px;" alt="" src="Images/refresh-grey-btn.jpg"/></asp:LinkButton>
                      <%-- <a id="addlink" runat="server"  href="employer_det.aspx"  class="pop3" style="cursor: pointer; float: right;"><img style="border:0px;vertical-align:middle;" alt="" src="Images/add-new-record-solid.png"/></a> &nbsp;&nbsp;
                      --%>
                       <telerik:RadButton ID="BuiltinIconsButton2" CommandName="allowfilter"  runat="server" ButtonType="ToggleButton"
                                ToggleType="CustomToggle" Visible="false" EnableViewState="true" AutoPostBack="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/line.png"  Value="T"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/hue.png" selected Value="F"></telerik:RadButtonToggleState>
                        </ToggleStates>
                        
                    </telerik:RadButton>
                      <div class="button" style="float:right;">
                       <a id="add-event"  data-toggle="modal" href="#addnewrecord"><img style="border:0px;vertical-align:middle; float:right;width:100px;" alt="" src="images/add-new-record-solid.jpg"/></a>
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
                          <telerik:GridTemplateColumn UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="40px" ItemStyle-Width="40px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                             <%--<asp:ImageButton ID="btnget"  runat="server"  border="0" />&nbsp;&nbsp;--%>
                              <asp:ImageButton ID="btnedit" ImageUrl="Images/edit-ico-small.png" CommandName="editform" Width="16px" runat="server"  border="0" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
            <%-- <CommandItemSettings AddNewRecordText="Add new record" AddNewRecordImageUrl="Images/AddRecord.png"
                    RefreshText="Refresh" RefreshImageUrl="Images/Refresh.png"></CommandItemSettings>--%>
                    </MasterTableView>
              <ItemStyle Wrap="false" CssClass="font"/>   
             <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="12px"  Font-Bold="true" Font-Names="Arial"  />
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False">
                                                            <Selecting AllowRowSelect="True" />
                                                             <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                        </clientsettings>
            </telerik:RadGrid>
                          <div class="modal hide"  id="addnewrecord">
                <div class="modal-header">
                  <button type="button"  class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">Add New Designation Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm"  frameborder="0" scrolling="no" width="100%" height="300px" src="desg_det.aspx"></iframe>
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
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">Edit Designation Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Ifrmfollowup"  frameborder="0" width="100%"  scrolling="no" height="300px" ></iframe>
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
   <%-- </form>--%>
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

