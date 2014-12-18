<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpdateCustomer.aspx.cs" Inherits="Admin.Customer.UpdateCustomerPage" %>
<%@ Register Src="~/FindCustomerUserControl.ascx" TagName="FindCustomerUserControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= FindCustomerUserControl1.Find.ClientID %>").click(function () {
                $("#<%= MessageResult.ClientID %>").hide();
            });
        });
    </script>
    <!-- heading -->
    <h2>
        Update Customer</h2>
    <hr />
    <!-- User Control -->
    <uc1:FindCustomerUserControl ID="FindCustomerUserControl1" runat="server" />
    <hr />
    <div runat="server" id="CustomerFieldsDiv" visible="false">
        <table>
            <!-- Customer id -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Customer ID:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CustomerIdTextBox" Width="100px" ReadOnly="true" />&nbsp;
                </td>
            </tr>
            <!-- Name -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Name:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CustomerNameTextBox" Width="230px" MaxLength="30" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="CustomerNameTextBox" ErrorMessage="Customer name is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- Address -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Address:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AddressTextBox" Width="230px" MaxLength="40" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="AddressTextBox" ErrorMessage="Address is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- City -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="City:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CityTextBox" Width="230px" MaxLength="20" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="CityTextBox" ErrorMessage="City is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- Province -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Province:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ProvinceTextBox" Width="230px" MaxLength="20" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="ProvinceTextBox" ErrorMessage="Province is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- Postal Code -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Postal Code:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="PostalTextBox" Width="230px" MaxLength="7" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="PostalTextBox" ErrorMessage="Post code is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- Deleted -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Deleted:" />&nbsp;
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="IsDeletedCheckbox"/>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button runat="server" ID="SaveButton" Text="Save" Width="80px" OnClick="SaveButton_Click" />&nbsp;
        <asp:Button runat="server" ID="ClearButton" Text="Cancel" Width="80px" 
            onclick="ClearButton_Click" />&nbsp;
        <hr />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
        ShowSummary="true" ForeColor="Red" />
    <asp:Label runat="server" ID="MessageResult" ForeColor="Blue" />
</asp:Content>
