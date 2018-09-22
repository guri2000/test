<%@ Page Title="" Language="VB" MasterPageFile="~/popup.master" AutoEventWireup="false" CodeFile="ChangePwd.aspx.vb" Inherits="ChangePwd" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
       <form id="Form1"  runat="server" method="post" class="form-horizontal" >
              <asp:ScriptManager ID="ScriptManager1" runat="server">
              </asp:ScriptManager>
              <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
              </telerik:RadWindowManager>

                <div class="row-fluid">
                    <div class="span12">
                        <div class="" 
                             style="float: right;margin: 6px 0 -13px;  border-bottom-style: none; 
                             border-bottom-color: inherit; border-bottom-width: medium;">
             
                               <asp:Button ID="Button1" Visible="false" class="btn btn-success"  runat="server" Text="Save" />
                               <asp:ImageButton  ID="ImageButton1"    runat="server"  
                                                 ImageUrl="~/images/save20x20.png" Height="20" Width="20" ToolTip ="Save"  />
                               <asp:ImageButton ID="Button2"    runat="server" ToolTip="Reset" 
                                                ImageUrl="~/images/reload20x20.png"   CausesValidation="False" />
                    </div>
                </div>
            </div>

            <div class="row-fluid" style="margin-top: 0px; display:none;">
                <div class="span12">
                    <span id="error" style="color: Red; display: none">* Special Characters not allowed</span>
                </div>
            </div>

            <div class="row-fluid">
                <div class="span12">
                     <div class="widget-box">
                         <div class="widget-title"> <span class="icon"> <i class="icon-align-justify"></i> </span>
                          <h5>Change Password-info</h5>
                         </div>

                        <div class="widget-content">

                            <div class="control-group">
                                    <label class="control-label">Login Name : </label>
                                <div class="controls">
                                    <asp:TextBox ID="txt_LoginName" Enabled="False" onkeypress="return IsAlphaNumeric(event);"  Text="" class="span11"   placeholder="Login name" runat="server"></asp:TextBox>
                                    
                                     <asp:TextBox ID="agntmail" runat="server" style="display:none;"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationposition"
                                        runat="server" ErrorMessage="Login Name is required" ControlToValidate="txt_LoginName" ForeColor="Red" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="control-group">
                                  <label for="normal" class="control-label">Old Password :  </label>
                               <div class="controls">
                                    <div style="display:none;">
                                    <asp:TextBox ID="txt_OldExistingPwd"  TextMode="SingleLine"  onkeypress="return IsAlphaNumeric1(event);"  placeholder="Old Password"  class="span11 mask text" runat="server" ></asp:TextBox>
                                    </div>
                                    <asp:TextBox ID="txt_OldPwd" onkeypress="return IsAlphaNumeric1(event);"  TextMode="Password"   class="span11 mask text" placeholder="Old Password" runat="server"></asp:TextBox>
                                    <br />
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator61" CssClass="validationposition"
                                      runat="server" ErrorMessage="Old Password is required" ControlToValidate="txt_OldPwd" ForeColor="Red" Display="Dynamic" >
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator  ID="CompareValidator1" CssClass="validationposition"
                                      runat="server" ErrorMessage="Old password does not match." ControlToValidate="txt_OldPwd"  ControlToCompare="txt_OldExistingPwd" ForeColor="Red" Display="Dynamic">
                                    </asp:CompareValidator>
                                </div>
                            </div>

                             <div class="control-group">
                                  <label for="normal" class="control-label">New Password :  </label>
                               <div class="controls">
                                    <asp:TextBox ID="txt_NewPwd" onkeypress="return IsAlphaNumeric1(event);" Text="" TextMode="Password"   class="span11 mask text" placeholder="New Password" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationposition"
                                      runat="server" ErrorMessage="New Password is required" ControlToValidate="txt_NewPwd" ForeColor="Red" Display="Dynamic" >
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                           <div class="control-group">
                                  <label for="normal" class="control-label">Confirm Password:  </label>
                               <div class="controls">
                                    <asp:TextBox ID="txt_ConfmPwd" onkeypress="return IsAlphaNumeric1(event);" TextMode="Password"   class="span11 mask text" placeholder="Confirm Password"  runat="server"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validationposition"
                                      runat="server" ErrorMessage="Confirm Password is required" ControlToValidate="txt_ConfmPwd" ForeColor="Red" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                 
                                    <asp:CompareValidator  ID="RequiredFieldValidator10" CssClass="validationposition"
                                      runat="server" ErrorMessage="New password and Confirm password must match." ControlToValidate="txt_ConfmPwd"  ControlToCompare="txt_NewPwd" ForeColor="Red" Display="Dynamic">
                                    </asp:CompareValidator>
                              </div>
                          </div>


                          <div class="control-group">
                                  <label for="normal" class="control-label">Enter Text:  </label>
                               <div class="controls">
                                   <table border="0" cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td colspan="3">
                                                 <asp:TextBox ID="txtCaptcha" class="span11 mask text" placeholder="Captcha Image Text"  runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationposition"
                                                   runat="server" ErrorMessage="Enter Captcha Text." ControlToValidate="txtCaptcha" ForeColor="Red" Display="Dynamic">
                                                 </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                    CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                                    FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/refresh.png" runat="server" CausesValidation="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <%-- <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Invalid. Please try again." 
                                                    OnServerValidate="ValidateCaptcha" runat="server" />--%>
                                            </td>
                                            <td align="right">
                                               <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" />--%>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>

                              </div>
                          </div>


                        </div> 
                     </div> 
                </div> 
            </div> 
    </form> 
    </div> 
</asp:Content>

