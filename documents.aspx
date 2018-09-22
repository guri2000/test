<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="documents.aspx.vb" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<style>
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.RadMenu ul.rmActive, .RadMenu ul.rmRootGroup {
    display: block;
    left: -210px !important;
}
.modal {
    position: fixed;
    top: 3%;
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
.widget-title {
    background: #d4d2d2;
    border-bottom: 1px solid #bab7b7;
    height: 30px;
}
.btn_send {
    background: #bd1920;
    color: #fff;
    font-size: 12px;
    border: 1px solid rgba(0, 0, 0, 0.1);
    cursor: pointer;
    padding: 2px 10px 2px 10px;
    margin: 3px 0px 0px 0px;
    border-radius: 3px;
    font-weight: 600;
    line-height: 20px;
}
.btn_send:hover, .btn_send:focus {
    background: #a10d13;
    color: #fff;
    box-shadow: 0 2px 5px 0px #333;
    text-decoration: underline;
}
.txt_link {vertical-align: text-top; margin-left: 6%; color: #bd1920; font-size: 11px; text-decoration: underline; cursor:pointer;}
.txt_link:hover {color: #a10d13; text-decoration: none;}
.row-fluid [class*="span"] {min-height: 26px;}
select, input[type="file"] {height: 26px; line-height: 26px;}
select, textarea, input[type="text"], input[type="password"], input[type="datetime"], input[type="datetime-local"], input[type="date"], input[type="month"], input[type="time"], input[type="week"], input[type="number"], input[type="email"], input[type="url"], input[type="search"], input[type="tel"], input[type="color"], .uneditable-input {
    font-size: 12px;
}
select, textarea, input[type="text"], input[type="password"], input[type="datetime"], input[type="datetime-local"], input[type="date"], input[type="month"], input[type="time"], input[type="week"], input[type="number"], input[type="email"], input[type="url"], input[type="search"], input[type="tel"], input[type="color"], .uneditable-input, .label, .dropdown-menu, .btn, .well, .progress, .table-bordered, .btn-group > .btn:first-child, .btn-group > .btn:last-child, .btn-group > .btn:last-child, .btn-group > .dropdown-toggle, .alert {
    border-radius: 3px;
}
.font
{
    font-family:Arial;
    font-size:11px;
  
}
.modal-body{ padding: 0px !important;
overflow-y: hidden !important;}
 .RadGrid .rgFilterRow img, .RadGrid .rgFilterRow input {
    width: 90% !important;
}
#ctl00_ContentPlaceHolder1_dtp_Followup_calendar
{
    top:60px !important;
}

/*.table th, .table td {
    padding: 0px !important;}*/
.RadGrid_Metro .rgEditForm {
    border-top: 0px solid #DDD !important;
    border-bottom: 0px solid #DDD !important;    
    background-color: #efefef !important;
     margin: -8px  !important;
    }
    
    #ctl00_ContentPlaceHolder1_RadGrid1_ctl00_ctl02_ctl03_Table2 th, #ctl00_ContentPlaceHolder1_RadGrid1_ctl00_ctl02_ctl03_Table2 td {
    padding: 0px !important;
    }
    .icon-hand-right::before {
        color: #666;
        font-size: 28px;
        padding-top: 107px;
        padding-left: 22px;
    }
    .icon-hand-down::before {
        font-size: 28px;
        padding-left: 210px;
    }
    .step_cls 
    {
        float: right;
        padding: 5px 15px 5px 15px;
        background: #13d815;
        color: #fff;
        font-weight: 600;
        border-left: 1px solid #bab7b7;
    }
    .rem_top {margin-left: 6px !important; min-height: 22px !important; margin-bottom: 0px !important; margin-top: 3px;}
    .RadGrid .rgWrap {padding: 0 0px 0 2px !important;}
    .RadGrid .rgInfoPart {margin-right: 6px !important;}
    @media (min-width:900px) 
    {
        .row-fluid .span5 {
            width: 44.426%;
        }
        .icon-hand-down::before {
            padding-left: 100px;
        }
    }
    
    @media (min-width:1000px) 
    {
        .row-fluid .span5 {
            width: 44.426%;
        }
        .icon-hand-down::before {
            padding-left: 100px;
        }
    }
    
    @media (min-width:1100px) 
    {
        .row-fluid .span5 {
            width: 44.426%;
        }
        .icon-hand-down::before {
            padding-left: 110px;
        }
    }
    
    @media (min-width:1200px) 
    {
        .row-fluid .span5 {
            width: 45.171%;
        }
        .icon-hand-down::before {
            padding-left: 120px;
        }
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
<div class="container-fluid">
 <form id="Form1"  runat="server" method="post" class="form-horizontal" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
     <div class="row-fluid" style="padding-top:10px; !important;">
      <div class="span12">
       
     
        <div class="widget-box" style="margin-bottom: 0px;">
          <div class="widget-title" style="height:auto;"> <span class="icon"><i class="icon-th"></i></span>
             <h5>List of Record: </h5>
            <div class="row-fluid widget-content nopadding" style="padding-top:6px !important;">
     
                <div class="span3">
                 Name : <b><asp:Label ID="Label3"  runat="server" Text=""></asp:Label></b> <br />
            <%--    Handeled By : <b><asp:Label ID="Label4"   runat="server" Text=""></asp:Label> </b>           --%>     
                </div>
                <div class="span3">
                Category : <b><asp:Label ID="Label5"  runat="server" Text=""></asp:Label> </b><br />
              <%--  Email Id :  <b><asp:Label ID="Label6"  runat="server" Text=""></asp:Label> </b>--%>
                </div>
                  <div class="span2">
                 Fileno : <b><asp:Label ID="Label7"  runat="server" Text=""></asp:Label> </b><br />
               <%-- Status :  <b><asp:Label ID="Label8"  runat="server" Text=""></asp:Label> </b>--%>
                </div>
                  <div class="span2">
                 FSN : <b><asp:Label ID="Label4"  runat="server" Text=""></asp:Label> </b><br />
               <%-- Status :  <b><asp:Label ID="Label8"  runat="server" Text=""></asp:Label> </b>--%>
                </div>

                 </div>
                </div>
                </div>
                </div>
                </div>
   

    <div class="row-fluid" id="upldlistsection" runat="server" style="margin-top:5px;">
      <div class="span4">
        <div class="widget-box" style="margin-bottom: 5px;">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5 style="padding: 8px 10px 8px 10px;">List of Documents:    </h5>
            <%--<asp:DropDownList ID="ddl_documents" width="200px" style="margin-bottom:2px; margin-top:2px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="" >Choose</asp:ListItem>
                   <asp:ListItem Value="P">Prospect</asp:ListItem>
                   <asp:ListItem Value="A">Agent</asp:ListItem>
                   <asp:ListItem Value="Pa">Partner</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="ImageButton2"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png" Width="18px" Height="18px"  OnClientClick="ShowProgress();"  CausesValidation="False" />
--%>
            <span class="step_cls"> Step 1 </span>
            <asp:DropDownList ID="ddl_docsmast" Visible="false" width="200px" style="margin-bottom:2px; margin-top:2px;" AppendDataBoundItems="true" class="span11"  runat="server">
                    <asp:ListItem Value="0" >Choose</asp:ListItem>
                   <asp:ListItem Value="1">Prospect</asp:ListItem>
                   <asp:ListItem Value="2">Agent</asp:ListItem>
                   <asp:ListItem Value="3">Partner</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="Button2" Visible="false"  runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png" Width="18px" Height="18px"   CausesValidation="False" /> 
                 <a id="add-event" style="display:none;"  data-toggle="modal" href="#addnewrecord" class="btn-mini"><img style="border:0px;vertical-align:middle; margin: 2px 10px 2px 10px; width:90px; float:right;" alt="" src="images/add-new-record-solid.jpg"/></a>
          </div>
           <div class="widget-content nopadding">
          <asp:Panel ID="Panel1" runat="server" Visible="false">
                  <div class="alert alert-info" >
              <button class="close" data-dismiss="alert">×</button>
              <strong></strong> 
               <asp:Label ID="lbl_EmployerFilter" runat="server" ></asp:Label> </div>
               </asp:Panel>
            <telerik:RadGrid ID="RadGrid1" EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered"  
            AllowPaging="True" Height="230px" CellSpacing="0" GridLines="None"    
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="true"  runat="server">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView DataKeyNames="docid" CommandItemDisplay="Top" NoMasterRecordsText="No records to display" >
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                   
                </CommandItemTemplate>
   
                     
         
                   <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="11px"  Font-Bold="true" Wrap="false" CssClass="grid_header"  Font-Names="Arial"/>
                      <clientsettings allowkeyboardnavigation="True" reordercolumnsonclient="False" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" ClipCellContentOnResize="True"
                                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                        </clientsettings>
            </telerik:RadGrid>
                          <div class="modal hide" id="addnewrecord">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">×</button>
                  <h3 style="font-size:18px;font-style:normal;font-weight:lighter;">Add New Lead</h3>
                </div>
                <div class="modal-body">
                <iframe runat="server" id="addnewfrm"  frameborder="0" width="100%" height="567px" src="#"></iframe>
                </div>
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
        </div>
      </div>
    </div>

      <div class="span1" style="margin-left: 0%;">
         <div class="span12">
              <div style="height: 260px;">
                  <i class="icon icon-hand-right"></i>
                  <i class="icon icon-arrow-right" style="font-size: 28px; color: #666; display: none;"></i>
              </div>
         </div>
    </div>

      <div class="span4" style="margin-left: 0%;">
           <div class="widget-box" style="margin-bottom: 4px;">
                <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
                     <h5 style="padding: 8px 10px 8px 10px;">Remarks: </h5>
                     <span class="step_cls"> Step 2 </span>
                </div>
                <div class="widget-content nopadding" style="height: 200px;">
                     <div class="control-group">
                          <label class="span12 rem_top">Remarks:</label>
                          <div class="span12">
                               <asp:TextBox ID="txtRem" style="height: 170px !important; resize:none; width:95%;" onkeypress="return IsAlphaNumeric(event);" 
                                   TextMode="MultiLine" class="span12" placeholder="Document Remarks" runat="server"
                                      MaxLength="2000" onblur="textBoxOnBlur(this);"></asp:TextBox>
                          </div>
                     </div>
                </div>
           </div>
      </div>

      <div class="span1" style="margin-left: 0%;">
         <div class="span12">
              <div style="height: 260px;">
                  <i class="icon icon-hand-right"></i>
                  <i class="icon icon-arrow-right" style="font-size: 28px; color: #666; display: none;"></i>
              </div>
         </div>
    </div>

      <div class="span3" style="margin-left: 0%;">
        <div class="widget-box" style="margin-bottom: 4px;">
           <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5 style="padding: 8px 10px 8px 10px;">Upload Document:   </h5>
            <span class="step_cls"> Step 3 </span>
          </div>
           <div class="widget-content nopadding" style="height: 200px;">
          
           <asp:Label ID="Label6" runat="server"  style="color:#000;margin-left:40px;"></asp:Label>

          <asp:Panel ID="Panel2" runat="server" Visible="false">
                  <div class="alert alert-info" >
             <%-- <button class="close" data-dismiss="alert">×</button>--%>
              <strong></strong> 
             
               <asp:Label ID="Label2" runat="server" ></asp:Label> </div><br />
               <div style="margin-left: 15px;">
                 <asp:FileUpload ID="FileUpload1" runat="server" />                  
               </div>
                
                <div style="margin-left: 15px; padding-top: 50px;">
                    <asp:Button ID="Button1" runat="server" Text="Upload" />
                </div>
              
               </asp:Panel>
           
        </div>
        </div>
        <div style="">
                  <i class="icon icon-hand-down"></i>
           </div>
      </div>

  </div>

    <div class="row-fluid">
      <div class="span12">
        <div class="widget-box" style="margin-bottom:5px;">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5 style="padding: 8px 10px 8px 10px;">List of Uploaded Records: </h5>
            <asp:LinkButton id="btnAll" runat="server" class="txt_link" OnClick="btnAll_click"  >Show All Active </asp:LinkButton>
            <asp:LinkButton id="btnDeactive" runat="server" class="txt_link">Show All Deactive </asp:LinkButton>

                      

            <span class="step_cls"> Step 4 </span>
            <asp:DropDownList ID="ddl_docfiletrer" Visible="false" width="200px" style="margin-bottom:2px; margin-top:2px;" AppendDataBoundItems="true" class="span11"  runat="server">
                   <asp:ListItem Value="" >Show All</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;  <asp:ImageButton ID="ImageButton1" Visible="false" runat="server" ToolTip="Refresh" 
                 ImageUrl="~/images/refresh16x16.png" Width="18px" Height="18px"   CausesValidation="False" /> 
          </div>
           <div class="widget-content nopadding">
            <telerik:RadGrid ID="RadGrid2" EnableViewState="true"  Skin="Metro"  CSSClass="table table-bordered"  
            AllowPaging="True" Height="160px" CellSpacing="0" GridLines="None"    
             AllowSorting="True" ShowHeader="true" PageSize="12" AllowFilteringByColumn="false"  runat="server">
              <GroupingSettings CaseSensitive="false" />
              <MasterTableView DataKeyNames="id" CommandItemDisplay="Top" NoMasterRecordsText="No records to display" >
              <Columns>
              <telerik:GridTemplateColumn HeaderText="ViewDoc" UniqueName="docdwn" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtn_ViewDoc" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="ViewDoc" AlternateText='<%#Eval("DOCPATH") %>' ImageUrl="Images/magnifier.png" />
                                    <asp:ImageButton ID="imgbtn_DelDoc" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="DelDoc"  ImageUrl="Images/logout16x16.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
              </Columns>
               <NoRecordsTemplate>
      <div>
        No records to display</div>
    </NoRecordsTemplate>
              <CommandItemTemplate>
                    
                     
                    
                </CommandItemTemplate>
       
                        
    
                   <PagerStyle AlwaysVisible="true"  PageSizeControlType="RadComboBox"  Mode="NextPrevAndNumeric" PageSizeLabelText="Display Rows: " PageSizes="5,10,25,50,100,250" ></PagerStyle>
 
                    </MasterTableView>
                 <ItemStyle Wrap="false" CssClass="font"/>   
                 <AlternatingItemStyle Wrap="false" CssClass="font"/>
                    <HeaderStyle Font-Size="11px"  Font-Bold="true" Wrap="false" CssClass="grid_header" Font-Names="Arial"/>
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
   <div class="row-fluid" >
      <div class="span12">
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
            <h5 style="padding: 8px 10px 8px 10px;">Intimate User (Optional) </h5>
            <span class="step_cls"> Step 5 </span>
            </div>
            <div class="widget-content nopadding">
                 <div class="control-group">
                      <div class="span4">
                           <label class="control-label" style="width: 110px;"> To:</label>
                          <div class="controls" style="margin-left: 120px;">
                              <asp:TextBox ID="txtemail" onkeypress="return IsAlphaNumeric(event);" 
                                   class="span11" placeholder="Email" runat="server"
                                     MaxLength="100" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                      <%-- <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                                  runat="server"  InitialValue=""  ErrorMessage="Email is required" ControlToValidate="txtemail" 
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            <%--  <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>--%>
                          </div>
                      </div>
                      <div class="span4" style="margin-left:5px;">
                           <asp:Button  runat="server" class="btn_send" Text ="Send" id="btnsend"  />
                      </div>
                 </div>
                   <div class="control-group">
                   <div class="span4">
                           <label class="control-label" style="width: 110px;">CC:</label>
                          <div class="controls" style="margin-left: 120px;">
                              <asp:TextBox ID="txtCC" onkeypress="return IsAlphaNumeric(event);" 
                                   class="span11" placeholder="CC" runat="server"
                                     MaxLength="400" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                      <%-- <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                                  runat="server"  InitialValue=""  ErrorMessage="Email is required" ControlToValidate="txtemail" 
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            <%--  <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>--%>
                          </div>
                          </div>
                        </div>
                 <div class="control-group">
                      <div class="span12" style="margin-bottom: 10px;">
                           <label class="control-label" style="width: 110px;">Email Body:</label>
                          <div class="controls" style="margin-left: 120px;">
                             <asp:TextBox ID="TextBox10" style="height: 101px !important; resize:none; width:97%;" onkeypress="return IsAlphaNumeric(event);" 
                                TextMode="MultiLine" class="span12" placeholder="Email Content" runat="server"
                                 MaxLength="2000" onblur="textBoxOnBlur(this);"></asp:TextBox>
                                 <%--  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationposition"
                              runat="server"  InitialValue=""  ErrorMessage="Email Body is required" ControlToValidate="TextBox10" 
                                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                          </div>
                      </div>
                 </div>
            </div>
           </div>
        </div>
    </div>




  </form>
    </div>

</asp:Content>

