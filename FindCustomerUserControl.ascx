<%@ Control Language="C#" AutoEventWireup="false" CodeFile="FindCustomerUserControl.ascx.cs" Inherits="FindCustomerUserControl" %>

<asp:Label ID="Label1" runat="server" SkinId="SkinLabels" Text="Customer ID:" />&nbsp;
<asp:TextBox runat="server" ID="CustomerIdTextBox" Width="100px" MaxLength="10"/>&nbsp;
<asp:RequiredFieldValidator runat="server" ID="StudentIdRequiredValidator" Text="*" ForeColor="Red"
    ControlToValidate="CustomerIdTextBox"
    ErrorMessage="Customer ID is required."
    Display="Dynamic" />
<br />
<br />
<asp:Button runat="server" ID="FindButton" Text="Find" Width="80px" />