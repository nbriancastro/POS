<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddCustomer.aspx.cs" Inherits="Admin.Customer.AddCustomerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= SaveButton.ClientID %>").click(function () {
                $("#<%= MessageResult.ClientID %>").hide();
            });
        });
    </script>
    <h2>Add Customer</h2>
    <hr />
    <table>
        <!-- Name -->
        <tr>
            <td class="Labelfield"><asp:Label runat="server" Font-Bold="true" Text="Name:" />&nbsp;</td>
            <td><asp:TextBox runat="server" ID="CustomerNameTextBox" Width="230px" MaxLength="30" />&nbsp;
                <asp:RequiredFieldValidator runat="server" Text="*" ForeColor="Red"
                    ControlToValidate="CustomerNameTextBox"
                    ErrorMessage="Customer name is required."
                    Display="Dynamic" /></td>
        </tr>
        <!-- Address -->
        <tr>
            <td class="Labelfield"><asp:Label runat="server" Font-Bold="true" Text="Address:" />&nbsp;</td>
            <td><asp:TextBox runat="server" ID="AddressTextBox" Width="230px" MaxLength="40" />&nbsp;
                <asp:RequiredFieldValidator runat="server" Text="*" ForeColor="Red"
                    ControlToValidate="AddressTextBox"
                    ErrorMessage="Address is required."
                    Display="Dynamic" /></td>
        </tr>
        <!-- City -->
        <tr>
            <td class="Labelfield"><asp:Label runat="server" Font-Bold="true" Text="City:" />&nbsp;</td>
            <td><asp:TextBox runat="server" ID="CityTextBox" Width="230px" MaxLength="20" />&nbsp;
                <asp:RequiredFieldValidator runat="server" Text="*" ForeColor="Red"
                    ControlToValidate="CityTextBox"
                    ErrorMessage="City is required."
                    Display="Dynamic" /></td>
        </tr>
        <!-- Province -->
        <tr>
            <td class="Labelfield"><asp:Label runat="server" Font-Bold="true" Text="Province:" />&nbsp;</td>
            <td><asp:TextBox runat="server" ID="ProvinceTextBox" Width="230px" MaxLength="20" />&nbsp;
                <asp:RequiredFieldValidator runat="server" Text="*" ForeColor="Red"
                    ControlToValidate="ProvinceTextBox"
                    ErrorMessage="Province is required."
                    Display="Dynamic" /></td>
        </tr>
        <!-- Postal Code -->
        <tr>
            <td class="Labelfield"><asp:Label runat="server" Font-Bold="true" Text="Postal Code:" />&nbsp;</td>
            <td><asp:TextBox runat="server" ID="PostalTextBox" Width="230px" MaxLength="7" />&nbsp;
                <asp:RequiredFieldValidator runat="server" Text="*" ForeColor="Red"
                    ControlToValidate="PostalTextBox"
                    ErrorMessage="Post code is required."
                    Display="Dynamic" /></td>
        </tr>
    </table>
    <br />
    <asp:Button runat="server" ID="SaveButton" Text="Save" Width="80px" onclick="SaveButton_Click" />&nbsp;
    <asp:Button runat="server" ID="ClearButton" Text="Cancel" Width="80px" CausesValidation="False" onclick="ClearButton_Click" />&nbsp;
    <hr />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowSummary="true" ForeColor="Red" />
    <asp:Label runat="server" ID="MessageResult" ForeColor="Blue" />
</asp:Content>