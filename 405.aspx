<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="405.aspx.vb" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="container-fluid">
    <div class="row-fluid">
      <div class="span12">
        <div class="widget-box">
          <div class="widget-title"> <span class="icon"> <i class="icon-info-sign"></i> </span>
            <h5>Error 405</h5>
          </div>
          <div class="widget-content">
            <div class="error_ex">
              <h1>405</h1>
              <h3>Opps, You're lost.</h3>
              <p>A request was made of a page using a request method not supported by that page.</p>
              <a class="btn btn-warning btn-big"  href="index.aspx">Back to Home</a> </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>

