using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Inventory
{
    public partial class DeleteItemPage : Page
    {
        private String ItemCode
        {
            get
            {
                String itemCode = String.Empty;
                if (ViewState["itemcode"] != null)
                    itemCode = ViewState["itemcode"].ToString();
                return itemCode;
            }
            set
            {
                ViewState["itemcode"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FindItemUserControl1.FindComplete += FindItemUserControl1_FindComplete;
        }

        protected void FindItemUserControl1_FindComplete(object sender, Item inventoryItem)
        {
            if (inventoryItem != null)
            {
                ItemCode = inventoryItem.ItemCode;
                RenderInventoryItem(inventoryItem);
                CustomerFieldsDiv.Visible = true;
                MessageResult.Visible = false;
            }
            else
            {
                MessageResult.Visible = true;
                MessageResult.Text = "Item not found.";
                CustomerFieldsDiv.Visible = false;
            }
        }

        private void RenderInventoryItem(Item inventoryItem)
        {
            ItemCodeTextBox.Text = inventoryItem.ItemCode;
            DescriptionTextBox.Text = inventoryItem.Description;
            UnitPriceTextBox.Text = inventoryItem.UnitPrice.ToString("0.00");
            StockTextBox.Text = inventoryItem.Stock.ToString();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Boolean confirmation = false;

            var inventoryItem = GetItemByCode(ItemCode);
            if (inventoryItem != null)
                confirmation = SaveItem(inventoryItem);
            else
                MessageResult.Text = "Item not found.";
            CustomerFieldsDiv.Visible = false;
            DisplayResult(confirmation, inventoryItem);
        }

        private Item GetItemByCode(String itemCode)
        {
            ABCPOS abcHardware = new ABCPOS();
            var inventoryItem = abcHardware.FindItem(itemCode);
            return inventoryItem;
        }

        private Boolean SaveItem(Item inventoryItem)
        {
            ABCPOS abcHardware = new ABCPOS();

            inventoryItem.Description = DescriptionTextBox.Text;
            inventoryItem.UnitPrice = Decimal.Parse(UnitPriceTextBox.Text);
            inventoryItem.Stock = Int32.Parse(StockTextBox.Text);
            var confirmation = abcHardware.DeleteItem(inventoryItem);
            return confirmation;
        }

        private void DisplayResult(Boolean confirmation, Item inventoryItem)
        {
            if (confirmation)
            {
                ClearTextFields(Page);
                MessageResult.Text = String.Format("Item [{0} - {1}] successfully removed.",
                                        ItemCode,
                                        inventoryItem.Description);
            }
            else
                MessageResult.Text = "Error in remove";
            MessageResult.Visible = true;
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