<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="_employer_detail_list.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <script type="text/javascript">
     $(document).ready(function () {
         /*
         *  Simple image gallery. Uses default settings
         */

         $('.pop1').fancybox({
             width: 285,
             height: 395,
             padding: 0,

             openEffect: 'elastic',
             openSpeed: 500,
             type: 'iframe',
             closeEffect: 'elastic',
             closeSpeed: 500,
             autoSize: false,
             closeClick: true

         });

         $('.pop2').fancybox({
             width: 500,
             height: 300,
             padding: 0,

             openEffect: 'elastic',
             openSpeed: 500,
             type: 'iframe',
             closeEffect: 'elastic',
             closeSpeed: 500,
             autoSize: false,
             closeClick: true

         });

         $('.pop5').fancybox({
             width: 560,
             height: 650,
             padding: 0,

             openEffect: 'elastic',
             openSpeed: 500,
             type: 'iframe',
             closeEffect: 'elastic',
             closeSpeed: 500,
             autoSize: false,
             closeClick: true,
             afterClose: function () {
                 window.location.reload(true);
             }

         });
         $('.pop3').fancybox({
             width: screen.width * 90 / 100,
             height: screen.height * 90 / 100,
             padding: 0,

             openEffect: 'elastic',
             openSpeed: 500,
             type: 'iframe',
             closeEffect: 'elastic',
             closeSpeed: 500,
             autoSize: false,
             closeClick: true,
             afterClose: function () {
                 window.location.reload(true);
             }

         });

         $('.pop4').fancybox({
             width: 1300,
             height: 699,
             padding: 0,

             openEffect: 'elastic',
             openSpeed: 500,
             type: 'iframe',
             closeEffect: 'elastic',
             closeSpeed: 500,
             autoSize: false,
             closeClick: true,
             afterClose: function () {
                 window.location.reload(true);
             }

         });




         $('.fancybox67').fancybox({
             'width': 285,
             'height': 395,
             'type': 'iframe',
             'autoScale': 'false',
             'hideOnContentClick': false,
             //	           
             'afterShow': function () {
                 $("div.fancybox-wrap").easydrag();
             }
         });

         $('.fancybox67_att').fancybox({
             'width': 282,
             'height': 420,
             'type': 'iframe',
             'autoScale': 'false',
             'hideOnContentClick': false,
             //	           
             'afterShow': function () {
                 $("div.fancybox-wrap").easydrag();
             }
         });
         $('.fancybox67_att2').fancybox({
             'width': 420,
             'height': 420,
             'type': 'iframe',
             'autoScale': 'false',
             'hideOnContentClick': false,
             //	           
             'afterShow': function () {
                 $("div.fancybox-wrap").easydrag();
             }
         });

         $('.fancybox67_att_manualpunch').fancybox({
             'width': 720,
             'height': 380,
             'type': 'iframe',
             'autoScale': 'false',
             'hideOnContentClick': false,
             //	           
             'afterShow': function () {
                 $("div.fancybox-wrap").easydrag();
             }
         });
     });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


  <div class="container-fluid">
    <hr>
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div class="row-fluid">
      <div class="span12">
       
     
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5>List Of Employers</h5>
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
          
            <telerik:RadGrid ID="RadGrid1" EnableViewState="false"  CSSClass="table table-bordered"  
            AllowPaging="True" CellSpacing="0" GridLines="None"  
             AllowSorting="True"   AllowFilteringByColumn="True"  runat="server">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top" >
              <CommandItemTemplate>
                    <div style="padding: 5px 5px;">
                      
                        <a id="addlink" runat="server" target="_blank" href="employer_det.aspx"  class="pop3" style="cursor: pointer; float: right;"><img style="border:0px;vertical-align:middle;" alt="" src="Images/add-new-record-solid.png"/></a> &nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="PerformInsert" Visible='<%# RadGrid1.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle;" alt="" src="Images/Insert.gif"/> Add this Customer</asp:LinkButton>&nbsp;&nbsp;
                       
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid"><img style="border:0px;vertical-align:middle;" alt="" src="Images/Refresh.png"/>Refresh customer list</asp:LinkButton>
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
            <%-- <CommandItemSettings AddNewRecordText="Add new record" AddNewRecordImageUrl="Images/AddRecord.png"
                    RefreshText="Refresh" RefreshImageUrl="Images/Refresh.png"></CommandItemSettings>--%>
                    </MasterTableView>
                     <PagerStyle AlwaysVisible="true"  Mode="NumericPages" PageSizeLabelText="Page Size: " PageSizes="{1, 10, 20, 50, 100, 200, 250}"></PagerStyle>
                 <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="false" />
                    </ClientSettings>
            </telerik:RadGrid>
        </div>
      </div>
    </div>
    </form>
  </div>
 <script src="js/jquery.min.js"></script> 
<script src="js/jquery.ui.custom.js"></script> 
<script src="js/bootstrap.min.js"></script> 
<script src="js/jquery.uniform.js"></script> 
<script src="js/select2.min.js"></script> 
<script src="js/jquery.dataTables.min.js"></script> 
<script src="js/matrix.js"></script> 
<script src="js/matrix.tables.js"></script>
</asp:Content>

