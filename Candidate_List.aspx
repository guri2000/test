<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Candidate_List.aspx.vb" Inherits="Candidate_List" %>
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
#ctl00_ContentPlaceHolder1_RadGrid1_ctl00_ctl02_ctl03_RNTBF_FSN_wrapper{width:73px !important;}
.modal-body{ padding: 0px !important;
overflow-y: hidden !important;}
select, input[type="file"] {
    height: 28px;
    line-height: 28px;
}
</style>

<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  <link rel="stylesheet" type="text/css" href="fancy/jquery.fancybox.css"
        media="screen" />
        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
<div class="" style="margin-left: 2px; margin-right:5px;">
  
  
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
 function ShowPopup4() {

     $(document).ready(function () {
         $("#btn_editcand").click();
         Setgrid1();
     });
 }
 
function Setgrid() {


    var hght = screen.height;
    var hght1 = screen.height - 230;
    var grd = document.getElementById('<%=RadGrid1.ClientID %>');
    grd.style.height = hght1 + "px";
    var frm = document.getElementById('<%=Ifrmfollowup.ClientID %>');
    var hght2 = hght - (hght * 20 / 100);
    frm.height = hght2 + "px";
    $(".modal").css('height', hght2);
    $(".modal-body").css('max-height', hght2);
}

function Setgrid1() {
    var hght = screen.height;
    var hght1 = screen.height - 230;
    var grd = document.getElementById('<%=RadGrid1.ClientID %>');
    grd.style.height = hght1 + "px";
    var frm = document.getElementById('<%=IfrEditcand.ClientID %>');
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

    <div class="row-fluid" style="margin-top: 10px;">
      <div class="span12">
       
     
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List of Records</h5> 
          
             <table cellpadding="0px">
              <tr valign="middle" >
              <td valign="middle"><label for="Employer" style="font-size:11px; font-weight:600; margin-bottom: 10px;">Employer:</label></td>
              <td valign="middle">
           
                       <asp:DropDownList ID="ddl_Employer" width="200px" style="margin-top: 0px; margin-left:7px;margin-right: 8px;" 
                                       AppendDataBoundItems="true" class="span12" AutoPostBack="true" runat="server" CausesValidation="false" >
                       <asp:ListItem Value="" >Show All</asp:ListItem>
                       </asp:DropDownList>
              </td>
              <td valign="middle"> <label for="Agent" style="font-size:11px; font-weight:600; margin-bottom: 10px;">BDC:</label> </td>
              <td valign="middle" >
              
                        <asp:DropDownList ID="ddl_BDC" width="200px" style="margin-top: 0px; margin-left:7px;margin-right: 8px;" 
                                       AppendDataBoundItems="true" class="span12" runat="server" CausesValidation="false" Enabled="false"  >
                            <asp:ListItem Value="" >Show All</asp:ListItem>
                       </asp:DropDownList>

              </td>
              <td valign="middle"><label for="JOStatus" style="font-size:11px; font-weight:600; margin-bottom: 10px;">JO Status:</label>    </td>
               <td valign="middle">
            
                        <asp:DropDownList ID="ddl_JO_Status" width="250px" style="margin-top: 0px; margin-left:7px;margin-right: 8px;" 
                                       AppendDataBoundItems="true" class="span12" AutoPostBack="true" runat="server" CausesValidation="false" >
                            <asp:ListItem Value="" >Show All</asp:ListItem>
                       </asp:DropDownList>
              </td>
              <td valign="middle">
                  <asp:ImageButton ID="Button2"  runat="server" ToolTip="Refresh" 
                     ImageUrl="~/images/refresh16x16.png" width="18px" style="vertical-align: baseline;" CausesValidation="False" />
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
                    AllowPaging="True" CellSpacing="0" GridLines="none"    FilterMenu-Width="1px"
                     AllowSorting="True" ShowHeader="true" PageSize="15"   AllowFilteringByColumn="true"  runat="server">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView DataKeyNames="TransId" NoMasterRecordsText="No records to display" CommandItemDisplay="Top" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    <div style="padding: 1px 5px;">
                      
                        <asp:LinkButton ID="LinkButton4" Visible="False" runat="server" CommandName="refresh"><img style="display:none;border:0px;vertical-align:middle;" alt="" src="Images/refresh-grey-btn.jpg"/></asp:LinkButton>
                        <asp:Label ID="Label3" runat="server" Text="Against Specific Company"></asp:Label>
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
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="86px" ItemStyle-Width="86px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                           
                              <asp:ImageButton ID="imgbtnedit"  runat="server"  border="0" ImageAlign="AbsMiddle" style="width: 16px;height:16px;"
                                    AlternateText="Edit"     ImageUrl="images/edit-ico-small.png" ToolTip="Edit Candidate Detail" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btn_JobSwitch"  runat="server"  border="0" ImageAlign="AbsMiddle" style="width: 20px;height: 20px;"
                                    AlternateText="Job Switch"     ImageUrl="images/switch_icon.png" ToolTip="Job Switch" />&nbsp;&nbsp;
                            <asp:ImageButton ID="btn_ViewResume"   runat="server" width="20px"  Height="20px" ToolTip="View Resume" 
                                    ImageUrl="images/View_Resume1.png"    AlternateText='<%# Eval("resume_path").ToString() %>' border="0" />&nbsp;&nbsp;
                          
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
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">Add New Job Secured</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm"  frameborder="0" width="100%" height="567px" ></iframe>
             
                </div>
              <%--  <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
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
             
            <div class="modal hide" id="editcandidate">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;font-family:Arial;">
                      <asp:Label ID="lbl_tag" runat="server" ></asp:Label></h3>
                </div>
                <div class="modal-body">
                        <iframe runat="server" id="IfrEditcand" scrolling="yes"   frameborder="0" width="100%" height="500px" ></iframe>
                </div>
           </div>
           <button type="button" style="display: none;" id="btn_editcand" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#editcandidate">
                Launch demo modal
            </button>




        </div>
      </div>
    </div>
    <%--</form>--%>
  </div>



</asp:Content>

