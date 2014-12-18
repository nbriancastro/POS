<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpdateItem.aspx.cs" Inherits="Admin.Inventory.UpdateItemPage" %>
<%@ Register Src="~/FindItemUserControl.ascx" TagName="FindItemUserControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= FindItemUserControl1.Find.ClientID %>").click(function () {
                $("#<%= MessageResult.ClientID %>").hide();
            });
        });
    </script>
    <h2>
        Add Item</h2>
    <hr />
    <!-- User Control -->
    <uc1:FinditemUserControl id="FindItemUserControl1" runat="server" />
    <hr />
    <div runat="server" id="CustomerFieldsDiv" visible="false">
        <table>
            <!-- Item Code -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Item Code:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ItemCodeTextBox" Width="230px" MaxLength="6" ReadOnly="True" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="ItemCodeTextBox" ErrorMessage="Item Code is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- Description -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Description:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DescriptionTextBox" Width="230px" MaxLength="40" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="DescriptionTextBox" ErrorMessage="Description is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- Unit Price -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Unit Price:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="UnitPriceTextBox" Width="230px" MaxLength="10" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="UnitPriceTextBox" ErrorMessage="Unit Price is required."
                        Display="Dynamic" />
                </td>
            </tr>
            <!-- Stock -->
            <tr>
                <td class="Labelfield">
                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Stock:" />&nbsp;
                </td>
                <td>
                    <asp:TextBox runat="server" ID="StockTextBox" Width="230px" MaxLength="20" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*"
                        ForeColor="Red" ControlToValidate="StockTextBox" ErrorMessage="Stock is required."
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
        <asp:Button runat="server" ID="ClearButton" Text="Cancel" Width="80px" CausesValidation="False"
            OnClick="ClearButton_Click" />&nbsp;
        <hr />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
        ShowSummary="true" ForeColor="Red" />
    <asp:Label runat="server" ID="MessageResult" ForeColor="Blue" />
</asp:Content>