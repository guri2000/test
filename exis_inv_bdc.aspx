<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="exis_inv_bdc.aspx.vb" Inherits="_Default" %>

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


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style>
    .RadGrid_Default .rgCommandRow a {
    color: #fff !important;
    text-decoration: none;
}
.modal {
    position: fixed;
    top: 3%;
    left: 5%;
    right:5%;
    z-index: 1050;
    width: 50%;
    height:300px;
    margin-left: 0px;
    background-color: #FFF;
    border: 1px solid rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    outline: 0px none;
    box-shadow: 0px 3px 7px rgba(0, 0, 0, 0.3);
    background-clip: padding-box;
   
}
.modal-body{max-height: 300px !important; padding: 0px !important; overflow-y: hidden !important;}
</style>

<script type="text/javascript" src="fancy/jquery.fancybox.js"></script>
  
  <script type="text/javascript">
      function ShowPopup() {
         // alert("fired");
          $(document).ready(function () {
              $("#btnShowPopup1").click();
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
</script>

    <div class="container-fluid">

      <form  runat="server" method="post" class="form-horizontal" >
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
     <asp:ValidationSummary ID="vs" ValidationGroup="basicdetails1" ShowMessageBox="true" ShowSummary="false" runat="server" />
    <div class="row-fluid" style="padding-top:1% !important;">
      <div class="span12">
       
     
        <div class="widget-box" style="display:none;">
          <div class="widget-title" style="height:auto;"> <%--<span class="icon"><i class="icon-th"></i></span>--%>
            <%--<h5>List of Record: </h5>--%>
            <div class="row-fluid widget-content nopadding" style="background: #fff;">
            <div class="span3" style="margin-left: 10px; display:none; ">
                FSN:<span style="color:Red;">*</span>
                <asp:TextBox ID="TextBox1" onkeypress="return isNumberKey(event);" style="width: 140px;" placeholder="FSN" runat="server"
                      MaxLength="99" onblur="textBoxOnBlur(this);"></asp:TextBox> &nbsp;

                       <asp:Button ID="btnGo" OnClientClick="if(Page_ClientValidate('basicdetails1')) ShowProgress();" runat="server" class="btn_go" Text="Go" />

                  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                  runat="server" ValidationGroup="basicdetails1" ErrorMessage="FSN is required" ControlToValidate="TextBox1" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
                <div class="span3">
                 <b><Label ID="name" class="" runat="server" Text="" style="font-weight: 600; color: red;"></Label></b> 
                <b><Label ID="immclass"  class="" runat="server" Text=""></Label> </b>
                
                </div>
                <div class="span2">
                <b><Label ID="fno" class="" runat="server" Text=""></Label> </b>
               <b><Label ID="cnt" class="" runat="server" Text=""></Label> </b>
                </div>
                  <div class="span2">
                <b><Label ID="rtdate" class="" runat="server" Text=""></Label> </b>
               <b><Label ID="brcnh" class="" runat="server" Text=""></Label> </b>
                </div>
                <div class="span2" style="margin-left: 0px;">
                     <div class="" style="float: right;margin: 0px 10px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
            
               <asp:Button ID="Button1" Enabled="false" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
           

            </div>
                    <b><Label ID="status" class="" runat="server"></Label> </b>
                </div>
                </div>
          </div>
           <div class="widget-content nopadding">
                
        </div>
      </div>
    </div>
    
    </div>


  <div class="row-fluid" style="display:none;">
    <div class="span12">
<span id="error" style="color: Red; display: none"> ~ ` ! ^ & ( ) _ { } [ ] | \ : ; " < > ? Special Characters not allowed</span>
</div></div>
<div class="row-fluid">
    <div class="span12">
      <div class="widget-box">
        <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
          <h5>Assign BDC</h5>

             <div class="" style="float: right;margin: 0px 10px;  border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;">
            
        
               <asp:ImageButton  ID="ImageButton1"  OnClientClick="if(Page_ClientValidate('basicdetails')) ShowProgress();"  Enabled="true"   runat="server"  
                   ImageUrl="~/images/save20x20.png" Height="20" Width="20" ToolTip ="Save"  />
               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                 ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />

            </div>
        </div>
        <div class="row-fluid widget-content">
      
         <div class="control-group">  
           <div class="span8" style="margin-left: 0px;">
               <div class="control-group">
              <label class="control-label" style="width: 100px;">Choose BDC: <span style="color:Red;">*</span></label>
              <div class="controls" style="margin-left: 110px;">
                
                   <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="true" class="span10"  runat="server">
                   <asp:ListItem Value="" >Choose BDC</asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp; 

                 
                 
                 
                         

              
                 <%--  <asp:ImageButton  ID="imgbtn_AddDesignation" runat="server"  
                   ImageUrl="~/images/add-new24x24.png" CausesValidation="false" ToolTip ="Designation"  />--%>
                      
                    <%-- <a href="#"><img src="images/add-new24x24.png" border="0" alt="" /></a>--%>
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationposition"
                  runat="server" InitialValue="" ValidationGroup="basicdetails" ErrorMessage="BDC is required" ControlToValidate="DropDownList1" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <br />
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
                <iframe runat="server" id="Ifrm12"  frameborder="0" width="100%" height="299px" ></iframe>
                  <%--<p>Enter event name:</p>
                  <p>
                    <input id="event-name" type="text" />
                  </p>--%>
                </div>
               <%-- <div class="modal-footer"> <a href="#" class="btn" data-dismiss="modal">Cancel</a> <a href="#" id="add-event-submit" class="btn btn-primary">Add event</a> </div>--%>
              </div>
              <button type="button" style="display: none;" id="btnShowPopup1" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#addnewpop1">
                Launch demo modal
            </button> 
    </form>
</div>

</asp:Content>

