using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Customer
{
    public partial class AddCustomerPage : Page
    {
        private UInt32 CustomerId
        {
            get
            {
                UInt32 customerId = 0;
                if (ViewState["customerid"] != null)
                    customerId = Convert.ToUInt32(ViewState["customerid"]);
                return customerId;
            }
            set
            {
                ViewState["customerid"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) { }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            CustomerId = SaveCustomer();
            var registeredCustomer = GetCustomerGetById(CustomerId);
            var confirmation = (CustomerId > 0);
            DisplayResult(confirmation, registeredCustomer);
        }

        private UInt32 SaveCustomer()
        {
            ABCPOS abcHardware = new ABCPOS();

            var customerId = abcHardware.AddCustomer(
                customerName: CustomerNameTextBox.Text,
                address: AddressTextBox.Text,
                city: CityTextBox.Text,
                province: ProvinceTextBox.Text,
                postal: PostalTextBox.Text);
            return customerId;
        }

        private global::Customer GetCustomerGetById(UInt32 customerId)
        {
            ABCPOS abcHardware = new ABCPOS();
            var registeredCustomer = abcHardware.FindCustomer(customerId);
            return registeredCustomer;
        }

        private void DisplayResult(Boolean confirmation, global::Customer registeredCustomer)
        {
            if (confirmation)
            {
                ClearTextFields(Page);
                MessageResult.Text = String.Format("Customer [{0} - {1}] successfully added.",
                    CustomerId,
                    registeredCustomer.Name);
            }
            else
                MessageResult.Text = "Error in update";
        }

        private void ClearTextFields(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                var box = control as TextBox;
                if (box != null)
                    box.Text = String.Empty;
                else if (control.HasControls())
                    ClearTextFields(control);
            }
        }
    
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearTextFields(Page);
            MessageResult.Visible = false;
        }
    }
}