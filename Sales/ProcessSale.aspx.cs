using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Sales
{
    public enum ActionResult
    {
        Success = 0,
        Error
    }

    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public class PurchaseItem
    {
        private String _itemCode;
        public String ItemCode
        {
            get { return _itemCode; }
            set { _itemCode = value; }
        }
        private String _description;
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private Decimal _unitPrice;
        public Decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }
        private Int32 _stock;
        public Int32 Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        private Int32 _quantity;
        public Int32 Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        private Decimal _itemTotal;
        public Decimal ItemTotal
        {
            get { return _itemTotal; }
            set { _itemTotal = value; }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class ProcessSale : Page
    {
        public Decimal SubTotal
        {
            get
            {
                Decimal itemCode = 0M;
                if (Session["subtotal"] != null)
                    itemCode = Convert.ToDecimal(Session["subtotal"]);
                return itemCode;
            }
            set
            {
                Session["subtotal"] = value;
            }
        }
        public Decimal GST
        {
            get
            {
                Decimal gst = 0M;
                if (Session["gst"] != null)
                    gst = Convert.ToDecimal(Session["gst"]);
                return gst;
            }
            set
            {
                Session["gst"] = value;
            }
        }
        public Decimal SaleTotal
        {
            get
            {
                Decimal saleTotal = 0M;
                if (Session["saletotal"] != null)
                    saleTotal = Convert.ToDecimal(Session["saletotal"]);
                return saleTotal;
            }
            set
            {
                Session["saletotal"] = value;
            }
        }

        private Decimal GetCurrentGSTPercentage()
        {
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            return Convert.ToDecimal(appSettings["GSTRate"] ?? "0");
        }
        private IList<PurchaseItem> GetItems()
        {
            IList<PurchaseItem> saleItems = HttpContext.Current.Session["purchase"] as IList<PurchaseItem>;
            return saleItems ?? new List<PurchaseItem>();
        }
        private IList<PurchaseItem> GetPurchasedItems(IList<PurchaseItem> saleItems)
        {
            return saleItems.Where(purchase => purchase.Quantity > 0).ToList();
        }
        private void ShowResult(ActionResult result, String message, String heading = null)
        {
            const string alertClassTemplate = "alert-{0}";
            const string scriptTemplate = "$(document).ready(function(){{ $('.alert-{0}').show().delay(5000).fadeOut(3000); }});";
            String alertClass = String.Format(alertClassTemplate, result.ToString().ToLower());
            String script = String.Format(scriptTemplate, result.ToString().ToLower());

            AlertControl1.HeadingText = heading ?? result.ToString();
            AlertControl1.Message = message;
            AlertControl1.AlertClass = String.Format(alertClass, result.ToString().ToLower());
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ActionResultScript", script, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "HideFlashMessages", "$(document).ready(function () { $('.alert').hide(); });", true);

            IList<PurchaseItem> purchases;
            if (!IsPostBack)
            {
                Session.Remove("purchase");
                Session.Remove("subtotal");
                Session.Remove("gst");
                Session.Remove("saletotal");

                ABCPOS abcHardware = new ABCPOS();
                CustomerDropdown.DataSource = abcHardware.GetActiveCustomers();

                SaleDateLabel.Text = DateTime.Now.ToShortDateString();
                purchases = abcHardware.GetActiveItems().Select(i => new PurchaseItem
                {
                    ItemCode = i.ItemCode,
                    Description = i.Description,
                    UnitPrice = i.UnitPrice,
                    Stock = i.Stock,
                    Quantity = 0,
                    ItemTotal = 0.00M
                }).ToList();
                Session["purchase"] = purchases;
            }
            else
            {
                purchases = HttpContext.Current.Session["purchase"] as IList<PurchaseItem>;
            }

            if (purchases != null)
            {
                Repeater1.DataSource = purchases;
                DataBind();
            }
            if (CustomerDropdown.Items.FindByValue("0") == null)
                CustomerDropdown.Items.Insert(0, new ListItem("[Select Customer]", "0"));
        }

        protected void ProcessButton_Click(object sender, EventArgs e)
        {
            // Create Sale header object
            Sale ABCSale = new Sale(
                saleDate: DateTime.Now,
                salesPerson: HttpContext.Current.User.Identity.Name,
                customerId: int.Parse(CustomerDropdown.SelectedValue),
                gstRate: GetCurrentGSTPercentage());

            // Add Sale Items objects from session to Sale header
            var items = GetItems();
            var purchasedItems = GetPurchasedItems(items);
            foreach (PurchaseItem purchase in purchasedItems)
                ABCSale.AddItem(purchase.ItemCode, purchase.Quantity);

            ABCPOS ABCHardware = new ABCPOS();
            Int32 saleNumber = 0;
            try
            {
                // Commit Sale to persistence
                saleNumber = ABCHardware.ProcessSale(ABCSale);

                // Reset session objects
                Session.Remove("subtotal");
                Session.Remove("gst");
                Session.Remove("saletotal");

                // Reset UI values
                CustomerDropdown.SelectedIndex = 0;
                AddressTextBox.Text = String.Empty;
                CityTextBox.Text = String.Empty;
                ProvinceTextBox.Text = String.Empty;
                PostalTextBox.Text = String.Empty;
                items.ToList().ForEach(item => { item.Quantity = 0; item.ItemTotal = 0; });
                Repeater1.DataSource = items;
                Page.DataBind();

                // Show success message
                var message = String.Format("Sale has been processed. <b>Sale&nbsp;Number:&nbsp;</b>{0}", saleNumber);
                ShowResult(ActionResult.Success, message);
            }
            catch (Exception ex)
            {
                ShowResult(ActionResult.Error, ex.Message);
            }
        }

        protected void CustomerDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropdown = (DropDownList)sender;
            if (uint.Parse(dropdown.SelectedValue) > 0)
            {
                ABCPOS abcHardware = new ABCPOS();
                Customer registeredCustomer = abcHardware.FindCustomer(uint.Parse(dropdown.SelectedValue));
                AddressTextBox.Text = registeredCustomer.Address;
                CityTextBox.Text = registeredCustomer.City;
                ProvinceTextBox.Text = registeredCustomer.Province;
                PostalTextBox.Text = registeredCustomer.Postal;
            }
            else
            {
                AddressTextBox.Text = String.Empty;
                CityTextBox.Text = String.Empty;
                ProvinceTextBox.Text = String.Empty;
                PostalTextBox.Text = String.Empty;
            }
        }

        # region Out-of-band Methods

        [WebMethod]
        public static dynamic OnCompute(String itemCode, Int32 quantity)
        {
            Boolean status = false;
            Decimal itemTotal = 0M;
            Decimal subTotal = 0M;
            Decimal total = 0M;
            Decimal gst = 0M;
            Decimal gstRate = 0.5M;

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            if (appSettings.Count > 0)
                gstRate = Convert.ToDecimal(appSettings["GSTRate"]);

            IList<PurchaseItem> purchases = HttpContext.Current.Session["purchase"] as IList<PurchaseItem>;
            if (purchases != null)
            {
                var item = purchases.FirstOrDefault(i => i.ItemCode.Equals(itemCode));
                if (item != null)
                {
                    item.Quantity = quantity;
                    item.ItemTotal = quantity*item.UnitPrice;
                    itemTotal = item.ItemTotal;

                    subTotal = purchases.Sum(purchase => purchase.ItemTotal);
                    gst = subTotal * gstRate;
                    total = subTotal + gst;
                }
                HttpContext.Current.Session["purchase"] = purchases;
                HttpContext.Current.Session["subtotal"] = subTotal;
                HttpContext.Current.Session["gst"] = gst;
                HttpContext.Current.Session["saletotal"] = total;
                status = true;
            }

            return new
            {
                itemTotal = itemTotal.ToString("C"),
                status,
                subTotal = subTotal.ToString("C"),
                gst = gst.ToString("C"),
                total = total.ToString("C")
            };
        }

        #endregion
    }
}