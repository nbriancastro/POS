<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AlertControl.ascx.cs" Inherits="AlertControl" %>

<div class="alert <%= AlertClass %>">
    <a class="close" data-dismiss="alert">×</a><h4 class="alert-heading"><%= HeadingText %></h4><span id="message"><%= Message %></span>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowSummary="true" />
</div>