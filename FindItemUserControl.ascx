<%@ Control Language="C#" AutoEventWireup="false" CodeFile="FindItemUserControl.ascx.cs" Inherits="FindItemUserControl" %>

<asp:Label ID="Label1" runat="server" SkinId="SkinLabels" Text="Item Code:" />&nbsp;
<asp:TextBox runat="server" ID="ItemCodeTextBox" Width="100px" MaxLength="10"/>&nbsp;
<asp:RequiredFieldValidator runat="server" Text="*" ForeColor="Red"
    ControlToValidate="ItemCodeTextBox"
    ErrorMessage="Item Code is required."
    Display="Dynamic" />
<br />
<br />
<asp:Button runat="server" ID="FindButton" Text="Find" Width="80px" />