<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cand_Applied_List.aspx.vb" Inherits="Default2" %>
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
   /* height:96%;*/
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
<div class="container-fluid">
  
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
    function ShowPopup() {
       
     $(document).ready(function () {
         $("#btnShowPopup").click();
         Setgrid();
    });
 }
 function ShowPopup1() {

     $(document).ready(function () {
         $("#btnShowPopup1").click();
        
     });
 }
function Setgrid() {


    var hght = screen.height;
    var hght1 = screen.height - 300;
    var grd = document.getElementById('<%=RadGrid1.ClientID %>');
    grd.style.height = hght1 + "px";
    var frm = document.getElementById('<%=Ifrmfollowup.ClientID %>');
    var hght2 = hght - (hght * 20 / 100);
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
    <div class="row-fluid">
      <div class="span12">
       
     
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List of Records</h5> 
          
             <table cellpadding="3px">
              <tr valign="middle" >
              <td valign="middle"><label for="Employer" style="margin-bottom: 0px;">Employer</label></td>
              <td valign="middle">
           
                       <asp:DropDownList ID="ddl_Employer" width="200px" style="margin-bottom:-3px;margin-left:7px;margin-right: 8px;" 
                                       AppendDataBoundItems="true" class="span12" AutoPostBack="true" runat="server" CausesValidation="false" >
                       <asp:ListItem Value="" >Show All</asp:ListItem>
                       </asp:DropDownList>
              </td>
              <td valign="middle"> <label for="Agent" style="margin-bottom: 0px;">BDC</label> </td>
              <td valign="middle" >
              
                        <asp:DropDownList ID="ddl_BDC" width="200px" style="margin-bottom:-3px;margin-left:7px;margin-right: 8px;" 
                                       AppendDataBoundItems="true" class="span12" runat="server" CausesValidation="false" Enabled="false"  >
                            <asp:ListItem Value="" >Show All</asp:ListItem>
                       </asp:DropDownList>

              </td>
              <td valign="middle"><label for="JOStatus" style="margin-bottom: 0px;">JO Status</label>    </td>
               <td valign="middle">
            
                        <asp:DropDownList ID="ddl_JO_Status" width="250px" style="margin-bottom:-3px;margin-left:7px;margin-right: 8px;" 
                                       AppendDataBoundItems="true" class="span12" AutoPostBack="false" runat="server" CausesValidation="false" >
                            <asp:ListItem Value="" >Show All</asp:ListItem>
                       </asp:DropDownList>
              </td>
              <td valign="middle">
                  <asp:ImageButton ID="Button2"  runat="server" ToolTip="Refresh" 
                     ImageUrl="~/images/refresh16x16.png"   CausesValidation="False" />
              </td>
              </tr>
          </table>
           
          </div>
          
         
          
           <div class="widget-content nopadding">
    
          <asp:Panel ID="Panel1" runat="server" Visible="false">
                  <div class="alert alert-info" >
              <button class="close" data-dismiss="alert">×</button>
              <strong></strong> 
               <asp:Label ID="lbl_EmployerFilter" runat="server" ></asp:Label> </div>
               </asp:Panel>
            <telerik:RadGrid ID="RadGrid1" EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered"  
                    AllowPaging="True" CellSpacing="0" GridLines="Both"    
                     AllowSorting="True" ShowHeader="true" PageSize="15"   AllowFilteringByColumn="true"  runat="server">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView DataKeyNames="jobsecuredid" NoMasterRecordsText="No records to display" CommandItemDisplay="Top" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    <div style="padding: 5px 5px;">
                      
                        <asp:LinkButton ID="LinkButton4" Visible="False" runat="server" CommandName="refresh"><img style="display:none;border:0px;vertical-align:middle;" alt="" src="Images/refresh-grey-btn.jpg"/></asp:LinkButton>
                     
                      

                       <telerik:RadButton ID="BuiltinIconsButton2" Visible="false" CommandName="allowfilter"  runat="server" ButtonType="ToggleButton"
                                ToggleType="CustomToggle"  EnableViewState="true" AutoPostBack="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/line.png"  Value="T"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState PrimaryIconUrl="Img/hue.png" selected Value="F"></telerik:RadButtonToggleState>
                        </ToggleStates>
                        
                    </telerik:RadButton>

                         <asp:DropDownList ID="ddl_Employer" Visible="False" width="200px" style="margin-bottom:-3px;" AppendDataBoundItems="true" class="span11"  runat="server">
                               <asp:ListItem Value="" >Show All</asp:ListItem>
                         </asp:DropDownList>&nbsp;&nbsp; 
                         <asp:LinkButton ID="LinkButton1" Visible="False" runat="server" CommandName="EmployerFilter">
                         <img src="images/refresh16x16.png" style="display:none" border="0" alt="" /></asp:LinkButton>


                    <%--<div class="button" style="float:right;">
                       <a id="add-event"  data-toggle="modal" href="#addnewrecord" class="btn btn-mini"><img style="border:0px;vertical-align:middle; float:right;width:100px;" alt="" src="images/add-new-record-solid.jpg"/></a>
                      </div>--%>
                     
                    
                </CommandItemTemplate>
          
                        <Columns>
                          <telerik:GridTemplateColumn UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                           
                           
                             <asp:ImageButton ID="btnView"   runat="server" width="21px" Height="21px"   border="0" />&nbsp;&nbsp;
                              <asp:ImageButton ID="btn_AddCandidate"   runat="server" width="21px"  Height="21px" border="0" />&nbsp;&nbsp;
                          
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        </Columns>
           <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                    <PagerStyle AlwaysVisible="true"  Mode="NumericPages" PageSizeLabelText="Page Size: " PageSizes="{1, 10, 20, 50, 100, 200, 250}"></PagerStyle>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="12px"  Font-Bold="true" Wrap="false"  Font-Names="Arial" />
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
                  <h3 style="font-size:x-large;font-style:normal;font-weight:lighter;">Add New Job Secured</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm"  frameborder="0" width="100%" height="567px" ></iframe>
             
                </div>
              <%--  <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>

              <div class="modal hide"  id="addnewfollowup">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:x-large;font-style:normal;font-weight:lighter;"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
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
              
              <div class="modal hide"  id="addremarks">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="Ifrm"  frameborder="0" width="100%" height="567px" ></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>

              <button type="button" style="display: none;" id="btnShowPopup1" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addremarks">
                Launch demo modal
            </button>   

        </div>
      </div>
    </div>
    <%--</form>--%>
  </div>



</asp:Content>

