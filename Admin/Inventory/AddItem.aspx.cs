using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Inventory
{
    public partial class AddItemPage : Page
    {
        private String ItemCode
        {
            get
            {
                String itemCode = String.Empty;
                if (ViewState["customerid"] != null)
                    itemCode = ViewState["customerid"].ToString();
                return itemCode;
            }
            set
            {
                ViewState["customerid"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) { }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ItemCode = ItemCodeTextBox.Text;
            var confirmation = SaveItem();
            var inventoryItem = GetItemByCode(ItemCode);
            DisplayResult(confirmation, inventoryItem);
        }

        private Boolean SaveItem()
        {
            ABCPOS abcHardware = new ABCPOS();

            var confirmation = abcHardware.AddItem(
                itemCode: ItemCodeTextBox.Text,
                description: DescriptionTextBox.Text,
                unitPrice: Decimal.Parse(UnitPriceTextBox.Text),
                stock: Int32.Parse(StockTextBox.Text));
            return confirmation;
        }

        private Item GetItemByCode(String itemCode)
        {
            ABCPOS abcHardware = new ABCPOS();
            var inventoryItem = abcHardware.FindItem(itemCode);
            return inventoryItem;
        }

        private void DisplayResult(Boolean confirmation, Item inventoryItem)
        {
            if (confirmation)
            {
                ClearTextFields(Page);
                MessageResult.Text = String.Format("Item [{0} - {1}] successfully added.",
                                        ItemCode,
                                        inventoryItem.Description);
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
            MessageResult.Visible = false;
            ClearTextFields(Page);
        }

    }
}