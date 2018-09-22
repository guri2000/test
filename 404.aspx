<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="404.aspx.vb" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="container-fluid">
    <div class="row-fluid">
      <div class="span12">
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"> <i class="icon-info-sign"></i> </span>
            <h5>Error 404</h5>
          </div>
          <div class="widget-content">
            <div class="error_ex">
              <h1>404</h1>
              <h3>Oops, You're lost.</h3>
              <p>The requested page could not be found but may be available again in the future.</p>
              <a class="btn btn-warning btn-big"  href="index.aspx">Back to Home</a> </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>

