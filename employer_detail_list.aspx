<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="employer_detail_list.aspx.vb" Inherits="Default2" %>
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
  /*height :96%;*/
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
 <script>
     // var oldvalue = $(lbl).text();

     function show(lbl) {
         //alert($(lbl).text());
         // alert($(lbl).attr("class"));
         var newNum = $(lbl).attr("ori");
         $(lbl).text(newNum).css("color", "red").css('font-weight', "bold");


     }

     function MouseOut(lbl) {
         //alert($(lbl).attr("alter"));
         $(lbl).text($(lbl).attr("alter")).css("color", "black").css('font-weight', "normal");

     }
            </script>
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

    var frm = document.getElementById('<%=Ifrmfollowup.ClientID %>');
    var hght2 = hght - (hght * 26 / 100);
    frm.height = hght2 + "px";
    $(".modal").css('height', hght2);
    $(".modal-body").css('max-height', hght2);
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
<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>

    <div class="row-fluid" style="margin-top: 10px;">
      <div class="span12">
       
     
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List of Records:    </h5><asp:DropDownList ID="ddl_Employer" width="200px" style="margin-bottom:-3px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="Button2"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png" style="margin-top: 5px;"   CausesValidation="False" /> 
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
          <asp:Panel ID="Panel1" runat="server" Visible="false">
                  <div class="alert alert-info" >
              <button class="close" data-dismiss="alert">×</button>
              <strong></strong> 
               <asp:Label ID="lbl_EmployerFilter" runat="server" ></asp:Label> </div>
               </asp:Panel>
            <telerik:RadGrid ID="RadGrid1" EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered"  
            AllowPaging="True" CellSpacing="0" GridLines="none"  
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView DataKeyNames="empid,agntid" CommandItemDisplay="Top" NoMasterRecordsText="No records to display" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    <div style="padding: 1px 5px;">
                      
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

               <asp:DropDownList ID="ddl_Employer" width="200px" Visible="False" style="margin-bottom:-3px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;       
                  
<asp:LinkButton ID="LinkButton1" Visible="False" runat="server" CommandName="EmployerFilter"><img src="images/refresh16x16.png" style="display:none;" border="0" alt="" /></asp:LinkButton>
                      <div class="button" style="float:right;">
                       <a id="add-event"  data-toggle="modal" href="#addnewrecord"><img style="border:0px;vertical-align:middle; float:right" alt="" src="images/add-new-record-solid.jpg"/></a>
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
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:ImageButton ID="btnint" style="cursor:default" Width="16"  runat="server"  border="0" Enabled="false" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnget"  runat="server" Width="16"  border="0" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnedit"   runat="server"  Width="16" border="0" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnjob"   runat="server" Width="16" border="0" />&nbsp;&nbsp;
                           
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                         <telerik:GridTemplateColumn HeaderText="EMAIL" UniqueName="eid"  SortExpression="EMAIL">
                                                    <ItemTemplate>  
                                                                                                          
                                                        <asp:LinkButton ID="lnkeml" CommandName="sndeml" CommandArgument='<%#Eval("EMAIL")%>' runat="server"  Text='<%#Eval("Employer_Email")%>' style="text-transform:lowercase"   onmouseover="show(this)" onmouseout="MouseOut(this)" alter='<%#Eval("Employer_Email")%>'  ori='<%#Eval("EMAIL")%>' ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                 <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Screening" UniqueName="Screening">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkScreening" runat="server" Text="Screening" CommandName="addscreening"
                                                CommandArgument='<%#Eval("empid") %>' ForeColor="Blue"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="120px" ItemStyle-Width="120px" AllowFiltering="false" HeaderText="Rating" UniqueName="Rating">
                                        <ItemTemplate>
                                              <telerik:RadRating RenderMode="Lightweight" ID="RadRating1" runat="server" AutoPostBack="false" Value='<%# Convert.ToDouble(Eval("SCRNRATING")) %>'
                                Precision="Exact" ReadOnly="true">
                            </telerik:RadRating>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                        </Columns>
            <%-- <CommandItemSettings AddNewRecordText="Add new record" AddNewRecordImageUrl="Images/AddRecord.png"
                    RefreshText="Refresh" RefreshImageUrl="Images/Refresh.png"></CommandItemSettings>--%>
                   <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="12px"  Font-Bold="true" Wrap="false" Font-Names="Arial"/>
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
                  <h3 style="font-size:14px;font-style:normal;font-weight:400;">New Lead</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm"  frameborder="0" width="100%" height="567px" src="employer_det.aspx"></iframe>
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
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
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

