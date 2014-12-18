using System;
using System.Web.UI.WebControls;

public partial class FindItemUserControl : System.Web.UI.UserControl
{
    public Button Find
    {
        get { return this.FindButton; }
    }
    // Delegate type for hooking up find complete notification.
    public delegate void FindCompleteHandler(Object sender, Item inventoryItem);

    // Event that clients can subscribe to and be notified
    // when search is complete.
    public event FindCompleteHandler FindComplete;

    public FindItemUserControl()
    {
        // Register user control event-handler
        this.Load += new EventHandler(FindCustomerUserControl_Load);
    }

    void FindCustomerUserControl_Load(object sender, EventArgs e)
    {
        this.FindButton.Click += new EventHandler(FindButton_Click);
    }

    protected void FindButton_Click(object sender, EventArgs e)
    {
        ABCPOS ABCHardware = new ABCPOS();
        String itemCode = this.ItemCodeTextBox.Text;
        Item inventoryItem = ABCHardware.FindItem(itemCode);
        OnFindComplete(inventoryItem);
    }

    // Invoke the find complete event
    protected virtual void OnFindComplete(Item inventoryItem)
    {
        if (FindComplete != null)
            FindComplete(this, inventoryItem);
    }
}