<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="exis_inv_list.aspx.vb" Inherits="Default2" %>
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
.modal2 {
    position: fixed;
    top: 1%;
    left: 40%;
    right:2%;
    z-index: 1050;
    width: 70%;
    height:90%;
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.modal-body2{max-height: 100% !important; padding: 0px !important;
overflow-y: hidden !important;}
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
    margin: 2px 3px 3px 3px;
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


</style>

<%--<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
<link rel="stylesheet" type="text/css" href="fancy/jquery.fancybox.css" media="screen" />--%>
        


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

 function ShowPopup3() {

     $(document).ready(function () {
         $("#btnShowPopup3").click();         
         var frm = document.getElementById('<%=Ifrmfollowup3.ClientID %>');
         frm.height="220px";
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
    var hght1 = screen.height - 265;
    var hght2 = screen.height - 400;
    var hght3 = screen.height - 500;
    var grd = document.getElementById('<%=RadGrid1.ClientID %>');
    grd.style.height = hght1 + "px";
    var grd1 = document.getElementById('<%=RadGrid2.ClientID %>');
    grd1.style.height = hght2 + "px";
    var grd3 = document.getElementById('<%=RadGrid3.ClientID %>');
    grd3.style.height = hght3 + "px";
    var frm = document.getElementById('<%=Ifrmfollowup.ClientID %>');
    var hght2 = hght - (hght * 26 / 100);
    frm.height = hght2 + "px";
    $(".modal").css('height', hght2);
    $(".modal-body").css('max-height', hght2);
}
    </script>
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
       
         <telerik:RadTabStrip ID="RadTabStrip1"  RenderMode="Lightweight"   runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1"
                                CssClass="tabgrid" ResolvedRenderMode="Classic" Skin="Office2007" style="margin-top: 10px;">
                                <Tabs>
                                   
                                    <telerik:RadTab PageViewID="DB1" Text="Prioriy Database" Style="font: 11px/26px Arial,sans-serif !important;">
                                    </telerik:RadTab>
                                   <telerik:RadTab PageViewID="DB2" Text="Generic database" 
                                        Style="font: 11px/26px Arial,sans-serif !important;">
                                    </telerik:RadTab>
									 
                                </Tabs>
                            </telerik:RadTabStrip>


                             <telerik:RadMultiPage ID="RadMultiPage1"  runat="server" SelectedIndex="0">
                                <telerik:RadPageView ID="DB1"  Height="550px"  runat="server" Style="padding-top: 3px;">
                                
    
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List of Database1 Records:    </h5><asp:DropDownList ID="ddl_Employer" width="200px" style="margin-bottom:-3px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="0" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="Button2" OnClientClick="ShowProgress();"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png"   CausesValidation="False" /> 
                                  &nbsp;&nbsp;
               
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
         
            <telerik:RadGrid ID="RadGrid1"  EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="True" CellSpacing="0" GridLines="none"    
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server" >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" DataKeyNames = "fileno,fsn,ID"  NoMasterRecordsText="No records to display" >
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
                   <asp:ListItem Value="0" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;       
                  
<asp:LinkButton ID="LinkButton1" Visible="False" runat="server" CommandName="EmployerFilter"><img src="images/refresh16x16.png" style="display:none;" border="0" alt="" /></asp:LinkButton>
              
                         <div class="button" style="float:right;">
                       <a id="a1"  data-toggle="modal" href="#addnewrecord" class=""><img style="border:0px;vertical-align:middle; float:right" alt="" src="images/add-new-record-solid.jpg"/></a>
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

                            <%--         <telerik:GridTemplateColumn  UniqueName="" HeaderText="" 
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                             
                             <asp:ImageButton ID="btnget"  runat="server" Width="20"  border="0" Visible ="false" />&nbsp;&nbsp;   
                              <asp:ImageButton ID="btnCand"  Width="20"  runat="server"  border="0"  Visible ="false"/>&nbsp;&nbsp;

                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>


                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="res"  HeaderText="Resume">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowResume" runat="server"  Text="View Resume"  CommandName="viewaddresume" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="docs1"  HeaderText="Documents">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowdocs" runat="server" ToolTip="View/Upload Documents"   Text="Document Lib." CommandName="adddocs" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

<telerik:GridTemplateColumn AllowFiltering="false" UniqueName="icon2" HeaderStyle-Width="50" ItemStyle-Width="50" HeaderText="">
                                <ItemTemplate >             
                                 <asp:ImageButton ID="btnedit"   runat="server"  Width="16" border="0" />&nbsp;&nbsp;             
                            <img src ='<%# Eval("icon")%>' Width="25" />
                           

                            </ItemTemplate>
                            </telerik:GridTemplateColumn>


                             <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="PICs"  HeaderText="PIC">
                                <ItemTemplate >
                            <%--ImageUrl='<%# "images/" + Eval("CountryAbbreviation") +".png"%>'--%>
                            <asp:Image ID="Image1"  runat="server"  ImageUrl ='<%# Eval("Image")%>'  class="thumbnail2" Width="40"  />
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
               
        </div>
      </div>
   
                                </telerik:RadPageView>
                                  <telerik:RadPageView ID="DB2"  Height="550px"  runat="server" Style="padding-top: 3px;">

                                    <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i>        </span>
            <h5>List of Database2 Records:  </h5> &nbsp;&nbsp;
                <iframe id="jobfrm" Visible="False" runat="server" width="200px" height="40px" style="vertical-align: text-top; margin-top: -14px;" frameborder="0" scrolling="no"></iframe>
                 &nbsp;&nbsp;
                   <asp:linkbutton id="Linkbutton2" OnClientClick="ShowProgress();" style="float:right;"  class="btn-cus" runat="Server">
                      <i class="icon-plus-sign-alt"></i> Syn Job Bank File           
                 </asp:linkbutton>

                   <asp:linkbutton id="Linkbutton3" OnClientClick="ShowProgress();"  style="float:right;"   class="btn-cus"  runat="Server">
                      <i class="icon-plus-sign-alt"></i> Exsisting Records           
                 </asp:linkbutton>
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
      
      <%--  <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
              <ContentTemplate>--%>

            <telerik:RadGrid ID="RadGrid2"   EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="True" CellSpacing="0" GridLines="none"    
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server" >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No records to display" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    
                     
                    
                </CommandItemTemplate>
             
                        <Columns>
                                    
                                              <telerik:GridTemplateColumn HeaderText="EMAIL" UniqueName="eid"  SortExpression="EMAILID">
                                                    <ItemTemplate>  
                                                                                                          
                                                        <asp:LinkButton ID="lnkeml" CommandName="sndeml" CommandArgument='<%#Eval("EMAILID")%>' runat="server"  Text='<%#Eval("EMAIL")%>' style="text-transform:lowercase"   onmouseover="show(this)" onmouseout="MouseOut(this)" alter='<%#Eval("EMAIL")%>'  ori='<%#Eval("EMAILID")%>' ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn HeaderText="PHONE" UniqueName="phn"  SortExpression="MOBILENO">
                                                    <ItemTemplate>  
                                                                                                          
                                                        <asp:Label ID="lnkphn"  runat="server"  Text='<%#Eval("PHONE")%>' style="text-transform:lowercase"   onmouseover="show(this)" onmouseout="MouseOut(this)" alter='<%#Eval("PHONE")%>'  ori='<%#Eval("MOBILENO")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                
                  <telerik:GridTemplateColumn UniqueName="res"  HeaderText="Resume">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowResume" runat="server"  Text="View Resume" CommandName="viewaddresume" CssClass="cls-res" ></asp:LinkButton>
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
              
            <%--  </ContentTemplate>
              </asp:UpdatePanel>--%>
                
        </div>
      </div>
    
    
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i>        </span>
            <h5>List of Syncronise Log Records:  </h5> &nbsp;&nbsp;
              
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
      
      <%--  <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
              <ContentTemplate>--%>

            <telerik:RadGrid ID="RadGrid3" EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="True" CellSpacing="0" GridLines="none"    
             AllowSorting="True" ShowHeader="true" PageSize="5" AllowFilteringByColumn="true"  runat="server" >
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No records to display" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    
                     
                    
                </CommandItemTemplate>
             
                        <Columns>
                                    
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
      

                                </telerik:RadPageView>
                                </telerik:RadMultiPage>


     
    <asp:HiddenField ID="TabName1" runat="server" />
    

                 <div class="modal hide" id="addnewrecord">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">Add New Record</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm"  frameborder="0" width="100%" height="567px" src="exis_invc_det.aspx"></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>
              <!-- div resume -->
              <div class="modal hide" id="modelUploadResume">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;">View/Add Resume</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="frmUploadResume"  frameborder="0" width="100%" height="567px" ></iframe>
                  <%--<p>Enter event name:</p>
                  <p>ShowPopupUpload()
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>
              <button type="button" style="display: none;" id="Button22" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#modelUploadResume">
                Launch demo modal
            </button>    
              <!-- div resume -->

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

              <div class="modal2 hide"  id="addnewfollowup2">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;"><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h3>
                </div>
                <div class="modal-body2">
                <iframe runat="server" id="Ifrmfollowup2"  frameborder="0" width="100%"  ></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>

                 <div class="modal hide"  id="addnewfollowup3" style="height:210px !important;">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;"><asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h3>
                </div>
                <div class="modal-body" style="height:200px;">
                <iframe runat="server" id="Ifrmfollowup3"  frameborder="0" width="100%" height="200px" ></iframe>
              
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>

              <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewfollowup">
                Launch demo modal
            </button>  

             <button type="button" style="display: none;" id="btnShowPopup3" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewfollowup3">
                Launch demo modal
            </button>  

     </div>
    </div>

</asp:Content>

