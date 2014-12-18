using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Customer
{
    public partial class DeleteCustomerPage : Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            FindCustomerUserControl1.FindComplete += FindCustomerUserControl1_FindComplete;
        }

        protected void FindCustomerUserControl1_FindComplete(object sender, global::Customer registeredCustomer)
        {
            if (registeredCustomer != null)
            {
                CustomerId = registeredCustomer.CustomerId;
                RenderResigteredCustomer(registeredCustomer);
                CustomerFieldsDiv.Visible = true;
            }
            else
            {
                MessageResult.Text = "Customer not found.";
                CustomerFieldsDiv.Visible = false;
            }
        }

        private void RenderResigteredCustomer(global::Customer registeredCustomer)
        {
            CustomerIdTextBox.Text = registeredCustomer.CustomerId.ToString();
            CustomerNameTextBox.Text = registeredCustomer.Name;
            AddressTextBox.Text = registeredCustomer.Address;
            CityTextBox.Text = registeredCustomer.City;
            ProvinceTextBox.Text = registeredCustomer.Province;
            PostalTextBox.Text = registeredCustomer.Postal;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Boolean confirmation = false;

            var registeredCustomer = GetCustomerGetById(CustomerId);
            if (registeredCustomer != null)
                confirmation = DeleteCustomer(registeredCustomer);
            else
                MessageResult.Text = "Customer not found.";
            CustomerFieldsDiv.Visible = false;
            DisplayResult(confirmation, registeredCustomer);
        }

        private global::Customer GetCustomerGetById(UInt32 customerId)
        {
            ABCPOS abcHardware = new ABCPOS();
            var registeredCustomer = abcHardware.FindCustomer(customerId);
            return registeredCustomer;
        }

        private Boolean DeleteCustomer(global::Customer registeredCustomer)
        {
            ABCPOS abcHardware = new ABCPOS();
            var confirmation = abcHardware.DeleteCustomer(registeredCustomer);
            return confirmation;
        }

        private void DisplayResult(Boolean confirmation, global::Customer registeredCustomer)
        {
            if (confirmation)
            {
                ClearTextFields(Page);
                MessageResult.Text = String.Format("Customer [{0} - {1}] successfully removed.",
                    CustomerId,
                    registeredCustomer.Name);
            }
            else
                MessageResult.Text = "Error in remove";
        }

        private void ClearTextFields(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                TextBox box = control as TextBox;
                if (box != null)
                    box.Text = String.Empty;
                else if (control.HasControls())
                    ClearTextFields(control);
            }
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearTextFields(Page);
            CustomerFieldsDiv.Visible = false;
        }

    }
}