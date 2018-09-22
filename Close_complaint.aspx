<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="Close_complaint.aspx.vb" Inherits="Close_complaint" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">

      <form  runat="server" method="post" class="form-horizontal" >
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
      <div class="row-fluid">
    <div class="span12">
    <div class="widget-title" 
            style="float: right;margin: 10px 0 -13px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
             
               <asp:Button ID="Button1" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
               <asp:ImageButton  ID="ImageButton1"    runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />

            </div>
</div>
</div>

     <div class="row-fluid">
    <div class="span12">
<span id="error" style="color: Red; display: none">* Special Characters not allowed</span>
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Complaint Status-info</h5>
        </div>
        </div></div></div>
  <div class="row-fluid">
    <div class="span12">
      <div class="widget-box" style="margin-top:0px;">
        
        <div class="widget-content nopadding">
        
            <div class="control-group">
            <div class="row-fluid">
                <div class="span4">
                  <label class="control-label">Date :</label>
                      <div class="controls">
                            <telerik:RadDatePicker ID="dtp_date" class="datepicker span11" runat="server" CssClass="txt"
                                    Width="100%" Height="27px" MinDate="1960-01-01" DateInput-DateFormat="dd-MMM-yyyy" ShowAnimation-Type="Slide">
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                        </SpecialDays>
                                    </Calendar>
                            </telerik:RadDatePicker>
                      </div>
                </div>


            
                 <div class="span4" style="margin-left: -49px;">
                    <label for="normal" class="control-label" >Employee Name :</label>
                      <div class="controls" style="width: 230px;">
                        <asp:DropDownList ID="ddl_Emp"   AppendDataBoundItems="true" class="span12" runat="server">
                          <asp:ListItem Value="" >Choose Employee Name</asp:ListItem>
                         <%--  <asp:ListItem Value="0" >Test case</asp:ListItem>--%>
                          </asp:DropDownList>
                         <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationposition"
                          runat="server" ErrorMessage="Employee Name is required" ControlToValidate="ddl_Emp" 
                            ForeColor="Red" ></asp:RequiredFieldValidator>
                      </div>
                    </div>


                 <div class="span4" style="margin-left: 60px;">
                     <label for="normal" class="control-label">Complaint Status :</label>
                      <div class="controls" style="width: 204px;">
                         <asp:DropDownList ID="ddl_comp_status"   AppendDataBoundItems="false" class="span11" runat="server">
                          <asp:ListItem Value="" >Choose Complaint Status</asp:ListItem>
                         </asp:DropDownList>
                         <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                          runat="server" ErrorMessage="Status is required" ControlToValidate="ddl_comp_status" 
                            ForeColor="Red" ></asp:RequiredFieldValidator>
                      </div>
                    </div>


            </div>
            <div class="row-fluid">
               <div class="span12">
    
          
            <div class="control-group">
              <label class="control-label">Remarks :</label>
              <div class="controls">
              <asp:TextBox ID="txt_remarks" onkeypress="return IsAlphaNumeric(event);" 
          TextMode="MultiLine" class="span11" placeholder="Address" runat="server" Width="97%" Rows="3" style="max-width:97%;max-height:300px;height:330px;"></asp:TextBox>
               
              </div>
            </div>
          
       
    
    </div>
    </div>
         </div>
            
            <%--<div class="control-group" style="visibility:hidden;">
              <label class="control-label">a</label>
              <div class="controls">
               
              </div>
            </div>--%>
           
           
           
         
        </div>
      </div>
     
    </div>
 
 
  
  </div>

   </form>
</div>

</asp:Content>

