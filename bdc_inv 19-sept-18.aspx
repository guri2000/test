<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="bdc_inv.aspx.vb" Inherits="Default2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<style type="text/css">
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
    height: 450px !important;
   
}

.font
{
    font-family:Arial;
    font-size:11px;
  
}
.modal-body{ padding: 0px !important;
overflow-y: hidden !important;}
.cls-res {color: #bd1920 !important; font-size: 11px; text-decoration: underline;}
.cls-res:hover {color: #a10d13 !important;}

.btn-cus {
    background: #bd1920;
    color: #fff;
    font-size: 12px;
    border: 1px solid rgba(0, 0, 0, 0.1);
    cursor: pointer;
    padding: 1px 10px 1px 10px;
    margin: 4px 3px 3px 3px;
    border-radius: 3px;
    font-weight: 600;
    line-height: 20px;
}
.btn-cus:hover, .btn-cus:focus {
    background: #a10d13;
    color: #fff;
    box-shadow: 0 2px 5px 0px #333;
    text-decoration: underline;
}

ul.tab {
    list-style-type: none;
    margin: 0;
    padding: 0;
    overflow: hidden;
    border: 1px solid #ccc;
    background-color: #f1f1f1;
}

/* Float the list items side by side */
ul.tab li {float: left;}

/* Style the links inside the list items */
ul.tab li a {
    display: inline-block;
    color: black;
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
    transition: 0.3s;
    font-size: 17px;
}

/* Change background color of links on hover */
ul.tab li a:hover {background-color: #ddd;}

/* Create an active/current tablink class */
ul.tab li a:focus, .active {background-color: ;}

/* Style the tab content */
.tabcontent {
    display: none;
    padding: 6px 12px;
    border: 1px solid #ccc;
    border-top: none;
}

.tab_border
{
    border-left: 1px solid #000;
    border-right: 1px solid #000;
    border-top: 1px solid #000;
}
.btn_gen {
    background: #bd1920;
color: #fff;
font-size: 12px;
border: 1px solid rgba(0, 0, 0, 0.1);
cursor: pointer;
padding: 2px 15px 2px 15px;
margin: 0px 0px 0px 10px;
border-radius: 3px;
font-weight: 600;
line-height: 20px;
}
.btn_gen:hover, .btn_gen:focus {
    background: #a10d13;
    color: #fff;
    box-shadow: 0 2px 5px 0px #333;
    text-decoration: underline;
}
.RadTabStrip_Office2007 .rtsLevel1 {
    background-color: #b8b9ba !important;
    background-image: none !important;
}
.RadTabStrip_Office2007 .rtsLevel1 .rtsLink {
    color: #333 !important;
}
.RadTabStrip_Office2007 .rtsLevel1 .rtsSelected, .RadTabStrip_Office2007 .rtsLevel1 .rtsSelected:hover {
    color: #bd1920 !important;
    font-weight: 600 !important;
}

</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="" style="margin-left: 2px; margin-right: 5px;">
  
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

 function ShowPopupUpload() {

     $(document).ready(function () {
         $("#Button22").click();
         Setgrid();
     });
 }
function Setgrid() {

    var hght = screen.height;
    var hght1 = screen.height - 500;
    var hght2 = screen.height - 550;
    var hght3 = screen.height - 550;
    var grd = document.getElementById('<%=RadGrid1.ClientID %>');
    grd.style.height = hght1 + "px";
    var grd2 = document.getElementById('<%=RadGrid2.ClientID %>');
    grd2.style.height = hght2 + "px";
    var grd3 = document.getElementById('<%=RadGrid3.ClientID %>');
    grd3.style.height = hght2 + "px";
    var grd4 = document.getElementById('<%=RadGrid4.ClientID %>');
    grd4.style.height = hght1 + "px";
    var frm = document.getElementById('<%=Ifrmfollowup.ClientID %>');
//    var hght2 = hght - (hght * 26 / 100);
//    frm.height = hght2 + "px";
//    $(".modal").css('height', hght2);
//    $(".modal-body").css('max-height', hght2);
}
    </script>
    <script>


  
        // var oldvalue = $(lbl).text();

        function show(lbl) {
            //alert($(lbl).text());
            // alert($(lbl).attr("class"));
           // var newNum = $(lbl).attr("ori");
            $(lbl).css("color", "red").css('font-weight', "bold");


        }

        function MouseOut(lbl) {
            //alert($(lbl).attr("alter"));
            $(lbl).css("color", "black").css('font-weight', "normal");

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



    





    <div class="row-fluid" style="margin-top: 7px;">






      <div class="span12">
       


           <telerik:RadTabStrip ID="RadTabStrip2"  RenderMode="Lightweight"   runat="server" SelectedIndex="0" MultiPageID="RadMultiPage2"
                                CssClass="tabgrid" ResolvedRenderMode="Classic" Skin="Office2007" EnableViewState="true"   >
                                <Tabs>                                   
                                    <telerik:RadTab PageViewID="AdvTAB" Text="Advertisement Detail" Value="1" >                                     
                                    </telerik:RadTab>
                                   <telerik:RadTab PageViewID="SchTAB" Text="My Schedule" Value="2">
                                    </telerik:RadTab>
									 
                                </Tabs>
                            </telerik:RadTabStrip>


<telerik:RadMultiPage ID="RadMultiPage2"  runat="server" SelectedIndex="0">
                                <telerik:RadPageView ID="AdvTAB"  Height="400px"  runat="server" Style="padding-top: 3px;">
                                 <div class="row-fluid">
                                
                                    <div class="span12">
                                
             <div class="widget-box" style="margin-top :3px; margin-bottom: 3px;">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>Advertisement Details    </h5><asp:DropDownList ID="ddl_Employer" width="200px" style="margin-bottom:-3px;display:none;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="0" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="Button2" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png"   CausesValidation="False" Visible ="false" /> 
                                  &nbsp;&nbsp;
                    <div class="button" style="float:right; margin-top: 4px; margin-right: 30px;">
                    <asp:ImageButton ID="ImageButton3" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/reload20x20.png" style="vertical-align: top;"  CausesValidation="False" Visible ="true" /> 

                 <%--<a id="a3"  data-toggle="modal" href="#addnewvac" class="btn_gen" title="Add New Vacancy"  >New Vac.</a>--%>
           <%--      <a id="a3"  data-toggle="modal" href="#addnewrecord" class="btn_gen" title="Add New Advertisement">New Adv.</a>--%>

                 <asp:LinkButton  id="ADDVAC"   class="btn_gen" title="Add New Vacancy"   runat="server"  OnClientClick="Showprogress();"  >New Vac.</asp:LinkButton>
                  <asp:LinkButton  id="AddAdv"   class="btn_gen" title="Add New Advertisement"   runat="server">  New Adv.</asp:LinkButton>
                   <asp:LinkButton  id="AddSch"   class="btn_gen" title="My Schedule"   runat="server" Visible="false">  My Schedule</asp:LinkButton>
                        
                      </div>
          </div>
           <div class="widget-content nopadding">
         
         
            <telerik:RadGrid ID="RadGrid1"  EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="True" CellSpacing="0" GridLines="none" Height="400px" style="height: 400px !important;"   
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server"  >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" DataKeyNames = "advid,vac_id"  NoMasterRecordsText="No records to display"  ShowHeadersWhenNoRecords="true" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
          
                    
                </CommandItemTemplate>
              
                        <Columns>
                                     <telerik:GridTemplateColumn  UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                             <asp:ImageButton ID="btnedit"   runat="server"  Width="16" border="0" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnget"  runat="server" Width="20"  border="0" />&nbsp;&nbsp;   
                              <asp:ImageButton ID="btnCand"  Width="20"  runat="server"  border="0" />&nbsp;&nbsp;

                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                               <telerik:GridTemplateColumn UniqueName="rpst1"  HeaderText="View">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowpost" runat="server"  Text="View Ad." CommandName="viewadv" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                
                        </Columns>

                   <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="12px"  Font-Bold="true" Wrap="false" Font-Names="Arial"/>
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False"  EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                           
                                                        </clientsettings>
            </telerik:RadGrid>
               
        </div>
      </div>

      </div>
      </div>
      </telerik:RadPageView>

      
                                <telerik:RadPageView ID="SchTAB"  Height="400px"  runat="server" Style="padding-top: 3px;">
                                 <div class="row-fluid">
                                
                                    <div class="span12">
                                    <div class="widget-box" style="margin-top :3px; margin-bottom: 3px;">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>My Schedule    </h5><asp:DropDownList ID="DropDownList3" width="200px" style="margin-bottom:-3px;display:none;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="0" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="ImageButton4" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png"   CausesValidation="False" Visible ="false" /> 
                                  &nbsp;&nbsp;
                    <div class="button" style="float:right; margin-top: 4px; margin-right: 30px;">
                    <asp:ImageButton ID="ImageButton5"   runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/reload20x20.png" style="vertical-align: top;"  CausesValidation="False" Visible ="false" /> 

                 <%--<a id="a3"  data-toggle="modal" href="#addnewvac" class="btn_gen" title="Add New Vacancy"  >New Vac.</a>--%>
           <%--      <a id="a3"  data-toggle="modal" href="#addnewrecord" class="btn_gen" title="Add New Advertisement">New Adv.</a>--%>

               <%--  <asp:LinkButton  id="LinkButton1"   class="btn_gen" title="Add New Vacancy"   runat="server"  OnClientClick="Showprogress();"  >New Vac.</asp:LinkButton>
                  <asp:LinkButton  id="LinkButton2"   class="btn_gen" title="Add New Advertisement"   runat="server">  New Adv.</asp:LinkButton>
                   <asp:LinkButton  id="LinkButton3"   class="btn_gen" title="My Schedule"   runat="server">  My Schedule</asp:LinkButton>
                       --%> 
                      </div>
          </div>
           <div class="widget-content nopadding">
         
         
            <telerik:RadGrid ID="RadGrid4"  EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="false" CellSpacing="0" GridLines="none" Height="400px" style="height: 400px !important;"   
             AllowSorting="false" ShowHeader="true" PageSize="12" AllowFilteringByColumn="false"  runat="server"  >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" DataKeyNames = "advid,CANDID,logid"  NoMasterRecordsText="No records to display"  ShowHeadersWhenNoRecords="true" >
               <NoRecordsTemplate>
      <div>
        No Interview Schedule</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
          
                    
                </CommandItemTemplate>
              
                        <Columns>
                                     <telerik:GridTemplateColumn  UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                         <asp:ImageButton ID="btnPrvRnd"   runat="server"  Width="16" border="0" Visible="true" im />&nbsp;&nbsp;
                               <asp:ImageButton ID="btnNxtRnd"  Width="20"  runat="server"  border="0" Visible="true"/>&nbsp;&nbsp;

                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                           
                
                        </Columns>

                   <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="12px"  Font-Bold="true" Wrap="false" Font-Names="Arial"/>
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False"  EnablePostBackOnRowClick="false">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                           
                                                        </clientsettings>
            </telerik:RadGrid>
               
        </div>
      </div>

                                    </div>
                                    </div>
                                    </telerik:RadPageView>
                               </telerik:RadMultiPage>

   
        <!-- TABs -->                
       <telerik:RadTabStrip ID="RadTabStrip1"  RenderMode="Lightweight"   runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1"
                                CssClass="tabgrid" ResolvedRenderMode="Classic" Skin="Office2007">
                                <Tabs>                                   
                                    <telerik:RadTab PageViewID="TAB1" Text="Publish Detail">
                                     
                                    </telerik:RadTab>

                                   <telerik:RadTab PageViewID="TAB2" Text="Candidates Detail">
                                    </telerik:RadTab>
									 
                                </Tabs>
                            </telerik:RadTabStrip>

                          
                             <telerik:RadMultiPage ID="RadMultiPage1"  runat="server" SelectedIndex="0">
                                <telerik:RadPageView ID="TAB1"  Height="300px"  runat="server" Style="padding-top: 3px;">
                                 <div class="row-fluid">
                                
                                    <div class="span12">

                                            <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>Publishing Details   </h5>
            <asp:DropDownList ID="DropDownList1" width="200px" style="margin-bottom:-3px;"  Visible="false" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="0" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="ImageButton1" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png"   CausesValidation="False"  Visible ="false"/> 
                                  &nbsp;&nbsp;
                    <div class="button" style="float:right; margin-top: 4px; margin-right: 30px;">
                    <asp:ImageButton ID="ImageButton2" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" Visible ="true"   /> 
                       <a id="a1"  data-toggle="modal" href="#modePostdetail" class="btn btn-mini" title="Add New Advertisement" style="display:none;" ><img  title="Add New Advertisement" style="border:0px;vertical-align:middle; float:right" alt="" src="images/add-new-record-solid.jpg"/></a>
                      </div>
          </div>
           <div class="widget-content nopadding">


  <telerik:RadGrid ID="RadGrid2"  EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="True" CellSpacing="0" GridLines="none"    
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server" >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" DataKeyNames = "TRANSID,ADVID,PLTFRMID"  NoMasterRecordsText="No records to display" >
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
                       

                                     <telerik:GridTemplateColumn UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                             <asp:ImageButton ID="btnedit"   runat="server"  Width="16" border="0" />&nbsp;&nbsp;
                               <asp:ImageButton ID="btnCand2"  Width="20"  runat="server"  border="0" />&nbsp;&nbsp;

                          <%--   <asp:ImageButton ID="btnint" style="cursor:default" Width="16"  runat="server"  border="0" Enabled="false" />&nbsp;&nbsp;
                            <asp:ImageButton ID="btnget"  runat="server" Width="16"  border="0" />&nbsp;&nbsp;                            
                             <asp:ImageButton ID="btnjob"   runat="server" Width="16" border="0" />&nbsp;&nbsp;
                           --%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn UniqueName="rpst"  HeaderText="View">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowpost" runat="server"  Text="View Post" CommandName="viewpost" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>



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
                    <HeaderStyle Font-Size="12px"  Font-Bold="true" Wrap="false" Font-Names="Arial"/>
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                        </clientsettings>
            </telerik:RadGrid>
        </div>
        </div>
                                    </div>
                                    </div>
     </telerik:RadPageView>

         <telerik:RadPageView ID="TAB2"  Height="300px"  runat="server" Style="padding-top: 3px;">
         <div class="row-fluid">
                                
                                    <div class="span12">

                                            <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>Candidate Details    </h5>
            <asp:DropDownList ID="DropDownList2" width="200px" style="margin-bottom:-3px;display:none;"  AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="0" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="Button4" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png"   CausesValidation="False" Visible ="false"   /> 
                                  &nbsp;&nbsp;

                    <div class="button" style="float:right; margin-top: 4px; margin-right: 30px;">
                     <asp:ImageButton ID="Button3" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" Visible ="true"   /> 

                       <a id="a2"  data-toggle="modal" href="#modeCanddetail" class="btn btn-mini" title="Add New Advertisement" style ="display:none;" ><img  title="Add New Candidate" style="border:0px;vertical-align:middle; float:right" alt="" src="images/add-new-record-solid.jpg"/></a>

                      </div>
          </div>
           <div class="widget-content nopadding">


                                      <telerik:RadGrid ID="RadGrid3"  EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="True" CellSpacing="0" GridLines="none"    
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server" >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" DataKeyNames="CANDID,ADVID,AGRNO,CANDRESULT"  NoMasterRecordsText="No records to display" >
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
                                     <telerik:GridTemplateColumn UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                             <asp:ImageButton ID="btnedit"   runat="server"  Width="16" border="0" />&nbsp;&nbsp;
                             <asp:ImageButton ID="btnint"  Width="20"  runat="server"  border="0" />&nbsp;&nbsp;
                              <asp:ImageButton ID="btnjob"   runat="server" Width="20" border="0" />&nbsp;&nbsp;
                          <%--   
                            <asp:ImageButton ID="btnget"  runat="server" Width="16"  border="0" />&nbsp;&nbsp;                            
                            
                           --%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="EMAIL" UniqueName="eid"  SortExpression="EMAIL">
                                                    <ItemTemplate>  
                                                                                                          
                                                        <asp:LinkButton ID="lnkeml" CommandName="sndeml" CommandArgument='<%#Eval("EMAIL")%>' runat="server"  Text='<%#Eval("Email")%>' style="text-transform:lowercase"  onmouseover="show(this)" onmouseout="MouseOut(this)"  ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                         <telerik:GridTemplateColumn UniqueName="res"  HeaderText="RESUME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowResume" runat="server"  Text="View Resume" CommandName="viewaddresume" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>



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
                    <HeaderStyle Font-Size="12px"  Font-Bold="true" Wrap="false" Font-Names="Arial"/>
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                        </clientsettings>
            </telerik:RadGrid>
        </div>
        </div>
                                    </div>
                                    </div>
     </telerik:RadPageView>

     
        </telerik:RadMultiPage>
 
    

                 <div class="modal hide" id="addnewrecord">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">Add New Record</h3>
                </div>
                <div class="modal-body">
                    <iframe runat="server" id="addnewfrm"  frameborder="0" width="100%" height="420px" src="#"></iframe>
                </div>
              </div>

                   <div class="modal hide" id="addnewvac">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">Add New Record</h3>
                </div>
                <div class="modal-body">
                    <iframe runat="server" id="Iframe2"  frameborder="0" width="100%" height="420px" src="#"></iframe>
                </div>
              </div>

           <%--        <div class="modal hide" id="addnewCand">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">Add New Record</h3>
                </div>
                <div class="modal-body">
                    <iframe runat="server" id="Iframe2"  frameborder="0" width="100%" height="350px" src="bdc_inv_Candpop.aspx"></iframe>
                </div>
              </div>--%>
          

              <!-- div resume -->
              <div class="modal hide" id="modePostdetail">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">View/Add Resume</h3>
                </div>
                <div class="modal-body">
                    <iframe runat="server" id="modePostdetail2"  frameborder="0" width="100%" height="420px" src="#"></iframe>
                </div>
              </div>

              <div class="modal hide" id="modeCanddetail">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">View/Add Resume</h3>
                </div>
                <div class="modal-body">
                    <iframe runat="server" id="Iframe1"  frameborder="0" width="100%" height="420px" src="#"></iframe>
                </div>
              </div>

              <button type="button" style="display: none;" id="Button22" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#modelUploadResume">
                Launch demo modal
            </button>    
              <!-- div resume -->

              <div class="modal hide"  id="addnewfollowup"  >
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                </div>
                <div class="modal-body">       
                     <%-- <div id="loading">
                        <div id="loadingimage" style="color: #ccc; font-size: 17px; line-height: 55px; font-family: verdana;
                            text-align: center;">
                            Loading...
                        </div>
                    </div>--%>
                <iframe runat="server" id="Ifrmfollowup"  frameborder="0" width="100%" height="420px"  > </iframe>
                </div>
              
              </div>
              <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewfollowup">
                Launch demo modal
            </button>  

     </div>
    </div>

    </div>
</asp:Content>

