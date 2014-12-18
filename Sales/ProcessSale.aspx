<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ProcessSale.aspx.cs" Inherits="Sales.ProcessSale" %>

<%@ Register src="../AlertControl.ascx" tagname="AlertControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/autoNumeric.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-alert.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var QuantityTextChange = function (text, e) {
            var param = new Object();
            param.itemCode = $(text).closest("tr").find("span[id*='ItemCodeLabel']").text();
            param.quantity = parseInt(text.value);
            var dataValue = JSON.stringify(param);
            $.ajax({
                type: "POST",
                url: "ProcessSale.aspx/OnCompute",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert("Request: " + xmlHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                success: function (response) {
                    var result = response.d;
                    if (result.status) {
                        var request = JSON.parse(this.data);
                        var spans = $("#<%= Panel1.ClientID %>").find('span');
                        var span = spans.filter(function () { return $(this).text() === request["itemCode"] ? true : false; });
                        var row = span.closest("tr");
                        row.find("span[id*='ItemTotalLabel']").text(result.itemTotal);

                        $("#<%= SubTotalLabel.ClientID %>").text(result.subTotal);
                        $("#<%= GSTLabel.ClientID %>").text(result.gst);
                        $("#<%= SaleTotalLabel.ClientID %>").text(result.total);
                    }
                }
            });
            e.preventDefault();
        };

        $(document).ready(function () {
            // Patch client validation
            var oldPageClientValidate = Page_ClientValidate;
            Page_ClientValidate = function() {
                oldPageClientValidate();
                if (!Page_IsValid) {
                    $(".alert").addClass("alert-error");
                    $(".alert span[id=message]").text("");
                    $(".alert-heading").html("Error");
                }
            };

            $("#<%= ProcessButton.ClientID %>").click(function(e) {
                if (!Page_ClientValidate()) {
                    $(".alert-error").show().delay(5000).fadeOut(3000); ;
                }
            });

            // Assign a change-event handler and numeric filter for each quantity textbox
            $("#<%= Panel1.ClientID %> :text").change(function (e) {
                QuantityTextChange(this, e);
            }).autoNumeric('init', { mDec: '0' });

        });
    </script>
    <h1>Proces Sale</h1>
    <hr />
    <table style="border: 0; width: 100%; height: 420px;">
        <tr>
            <!-- left -->
            <td style="vertical-align: top;">
                <table>
                    <!-- Sale -->
                    <tr>
                        <td class="Labelfield">
                            <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Sale Date :" />&nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="SaleDateLabel" />
                        </td>
                    </tr>
                    <!-- Customer -->
                    <tr>
                        <td class="Labelfield">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Customer :" />&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="CustomerDropdown" DataTextField="Name" DataValueField="CustomerId"
                                AutoPostBack="True" OnSelectedIndexChanged="CustomerDropdown_SelectedIndexChanged" />
                            &nbsp;
                            <asp:CompareValidator runat="server" Text="*" ForeColor="Red" Font-Bold="True" ControlToValidate="CustomerDropdown"
                                ErrorMessage="Customer is required." ValueToCompare="0" Operator="NotEqual" Display="Dynamic" />
                        </td>
                    </tr>
                    <!-- Address -->
                    <tr>
                        <td class="Labelfield">
                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Address :" />&nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="AddressTextBox" Width="230px" />
                        </td>
                    </tr>
                    <!-- City -->
                    <tr>
                        <td class="Labelfield">
                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="City :" />&nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="CityTextBox" Width="230px" />
                        </td>
                    </tr>
                    <!-- Province -->
                    <tr>
                        <td class="Labelfield">
                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Province :" />&nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="ProvinceTextBox" Width="230px" />
                        </td>
                    </tr>
                    <!-- Postal Code -->
                    <tr>
                        <td class="Labelfield">
                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Postal&nbsp;Code :" />&nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="PostalTextBox" Width="130px" />&nbsp;
                        </td>
                    </tr>
                </table>
                <hr />
                <asp:Button runat="server" ID="ProcessButton" Text="Process" Width="90px" OnClick="ProcessButton_Click" />
                <br />
                <br />
            </td>
            <!-- right -->
            <td style="vertical-align: top; text-align: right; width: 62%;">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="290px" BorderStyle="Solid"
                    BorderWidth="1">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th style="text-align: center;">
                                        Code
                                    </th>
                                    <th style="text-align: left; width: 100px;">
                                        Description
                                    </th>
                                    <th style="text-align: right; width: 100px;">
                                        Price
                                    </th>
                                    <th style="text-align: center; width: 100px;">
                                        Quantity
                                    </th>
                                    <th style="text-align: right; width: 40px;">
                                        Item&nbsp;Total
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="alternate" style="text-align: center; width: 70px;">
                                    <asp:Label runat="server" ID="ItemCodeLabel" Text='<%# Eval("ItemCode") %>' />
                                </td>
                                <td class="alternate" style="text-align: left; width: 240px;">
                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("Description") %>' />
                                </td>
                                <td class="alternate" style="text-align: right; width: 50px;">
                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("UnitPrice", "{0:c}") %>' />
                                </td>
                                <td class="alternate" style="text-align: center; width: 50px;">
                                    <asp:TextBox runat="server" ID="QuantityTextBox" Text='<%# Eval("Quantity") %>' Width="30px"
                                        Style="text-align: right" />
                                </td>
                                <td class="alternate" style="text-align: right; width: 40px;">
                                    <asp:Label runat="server" ID="ItemTotalLabel" Text='<%# Eval("ItemTotal", "{0:c}") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td style="text-align: center; width: 70px;">
                                    <asp:Label runat="server" ID="ItemCodeLabel" Text='<%# Eval("ItemCode") %>' />
                                </td>
                                <td style="text-align: left; width: 200px;">
                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("Description") %>' />
                                </td>
                                <td style="text-align: right; width: 50px;">
                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("UnitPrice", "{0:c}") %>' />
                                </td>
                                <td style="text-align: center; width: 50px;">
                                    <asp:TextBox runat="server" ID="QuantityTextBox" Text='<%# Eval("Quantity") %>' Width="30px"
                                        Style="text-align: right" />
                                </td>
                                <td style="text-align: right; width: 40px;">
                                    <asp:Label runat="server" ID="ItemTotalLabel" Text='<%# Eval("ItemTotal", "{0:c}") %>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <table class="summary" style="width: 100%">
                    <tr>
                        <td>
                            <span style="font-weight: bold;">Sub-Total:&nbsp;</span><asp:Label ID="SubTotalLabel"
                                runat="server" Text='<%# SubTotal.ToString("C") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="font-weight: bold;">GST:&nbsp;</span><asp:Label ID="GSTLabel" runat="server"
                                Text='<%# GST.ToString("C") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="font-weight: bold;">Sale Total:&nbsp;</span><asp:Label ID="SaleTotalLabel"
                                runat="server" Text='<%# SaleTotal.ToString("C") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 0; margin: 0;">
                <%--<div class="alert alert-error">
                    <a class="close" data-dismiss="alert">×</a><h4 class="alert-heading">Error</h4>
                    
                </div>
                <div class="alert alert-success">
                    <a class="close" data-dismiss="alert">×</a><h4 class="alert-heading">Success</h4>Sale has been processed.
                </div>--%>
                <uc1:AlertControl ID="AlertControl1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
