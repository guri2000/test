<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="bdc_inv_Schedpop.aspx.vb" Inherits="_Default" %>

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

<%--<script type="text/javascript">



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
        grd3.style.height = hght2 + "px";
        var frm = document.getElementById('<%=Ifrmfollowup.ClientID %>');
        //    var hght2 = hght - (hght * 26 / 100);
        //    frm.height = hght2 + "px";
        //    $(".modal").css('height', hght2);
        //    $(".modal-body").css('max-height', hght2);
    }
    </script>--%>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css">
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}

input[disabled], select[disabled], textarea[disabled], input[readonly], select[readonly], textarea[readonly] {
    height: 28px;
}

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
overflow-y: hidden !important;
width:100%;
}
.cls-res {color: #bd1920 !important; font-size: 11px; text-decoration: underline;}
.cls-res:hover {color: #a10d13 !important;}

</style>

<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  
  <script type="text/javascript">
      function ShowPopup() {
         // alert("fired");
          $(document).ready(function () {
              $("#btnShowPopup1").click();
              Setgrid();
          });
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
        $('#addnewpop1').modal('show');
      
    }


    function Setgrid() {

        var hght = screen.height;
        var hght1 = screen.height - 600;
        var grd = document.getElementById('<%=RadGrid1.ClientID %>');
        grd.style.height = hght1 + "px";
        var frm = document.getElementById('<%=Ifrmfollowup1.ClientID %>');
        var hght2 = hght - (hght * 26 / 100);
        frm.height = hght2 + "px";      
        //    $(".modal").css('height', hght2);
        //    $(".modal-body").css('max-height', hght2);
    }
</script>

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
        <div class="widget-title" style="margin-top: 0px;"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Interview Schedule</h5>   
           &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label runat="server" ID="lblVac">   </asp:Label>

             <div class="" style="display:none;  float: right;margin: 4px 10px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">            
               <asp:Button ID="Button1" Enabled="false" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"  Enabled="true"   runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" style="margin-right: 5px;" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />

            </div>      
    </div>
     <div class="row-fluid widget-content">
          <div class="control-group">
               <telerik:RadGrid ID="RadGrid1"  EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered" AutoGenerateColumns="true"  
            AllowPaging="false" CellSpacing="0" GridLines="none"    
             AllowSorting="false" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server"  Width="95%">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView CommandItemDisplay="Top"   DataKeyNames="advid,CANDID,logid" NoMasterRecordsText="No records to display" >
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
                             <telerik:GridTemplateColumn UniqueName="" HeaderText=""  Visible ="true"
                            ItemStyle-Wrap="false" AllowFiltering="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <HeaderTemplate>
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                             <asp:ImageButton ID="btnPrvRnd"   runat="server"  Width="16" border="0" Visible="true" im />&nbsp;&nbsp;
                               <asp:ImageButton ID="btnNxtRnd"  Width="20"  runat="server"  border="0" Visible="true"/>&nbsp;&nbsp;

                          <%--   <asp:ImageButton ID="btnint" style="cursor:default" Width="16"  runat="server"  border="0" Enabled="false" />&nbsp;&nbsp;
                            <asp:ImageButton ID="btnget"  runat="server" Width="16"  border="0" />&nbsp;&nbsp;                            
                             <asp:ImageButton ID="btnjob"   runat="server" Width="16" border="0" />&nbsp;&nbsp;
                           --%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                            <%--    <telerik:GridTemplateColumn UniqueName="prvRound"  HeaderText="Previous Round">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowRound" runat="server"  Text="View Previous Round" CommandName="viewround" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn UniqueName="nxtRound"  HeaderText="Feedback">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowRound" runat="server"  Text="Enter Feedback" CommandName="viewrNxtound" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>




                       <%--         <telerik:GridTemplateColumn UniqueName="rpst"  HeaderText="View">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkshowpost" runat="server"  Text="View Post" CommandName="viewpost" CssClass="cls-res" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>



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
                <iframe runat="server" id="Ifrmfollowup1"  frameborder="0" width="100%" > </iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>




              <div class="modal hide"  id="addnewfollowup"  >
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:14px;font-style:normal;font-weight:600;"><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h3>
                </div>
                <div class="modal-body">       
                     <%-- <div id="loading">
                        <div id="loadingimage" style="color: #ccc; font-size: 17px; line-height: 55px; font-family: verdana;
                            text-align: center;">
                            Loading...
                        </div>
                    </div>--%>
                <iframe runat="server" id="Ifrmfollowup2"  frameborder="0" width="100%" height="420px"  > </iframe>
                </div>
              
              </div>




              <button type="button" style="display: none;" id="btnShowPopup1" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewpop1">
                Launch demo modal
            </button> 
    </form>
</div>

</asp:Content>

