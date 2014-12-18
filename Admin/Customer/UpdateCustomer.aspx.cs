using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Customer
{
    public partial class UpdateCustomerPage : Page
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
                MessageResult.Visible = false;
            }
            else
            {
                MessageResult.Visible = true;
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
            IsDeletedCheckbox.Checked = registeredCustomer.IsDeleted;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Boolean confirmation = false;

            var registeredCustomer = GetCustomerGetById(CustomerId);
            if (registeredCustomer != null)
                confirmation = SaveCustomer(registeredCustomer);
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

        private Boolean SaveCustomer(global::Customer registeredCustomer)
        {
            ABCPOS abcHardware = new ABCPOS();

            registeredCustomer.Name = CustomerNameTextBox.Text;
            registeredCustomer.Address = AddressTextBox.Text;
            registeredCustomer.City = CityTextBox.Text;
            registeredCustomer.Province = ProvinceTextBox.Text;
            registeredCustomer.Postal = PostalTextBox.Text;
            registeredCustomer.IsDeleted = IsDeletedCheckbox.Checked;
            var confirmation = abcHardware.UpdateCustomer(registeredCustomer);
            return confirmation;
        }

        private void DisplayResult(Boolean confirmation, global::Customer registeredCustomer)
        {
            if (confirmation)
            {
                ClearTextFields(Page);
                MessageResult.Text = String.Format("Customer [{0} - {1}] successfully updated.",
                    CustomerId,
                    registeredCustomer.Name);
            }
            else
                MessageResult.Text = "Error in update";
            MessageResult.Visible = true;
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
            CustomerFieldsDiv.Visible = false;
        }
    }
}