using System;
using System.Web.UI.WebControls;

public partial class FindCustomerUserControl : System.Web.UI.UserControl
{
    public Button Find
    {
        get { return this.FindButton; }
    }
    // Delegate type for hooking up find complete notification.
    public delegate void FindCompleteHandler(Object sender, Customer registeredCustomer);

    // Event that clients can subscribe to and be notified
    // when search is complete.
    public event FindCompleteHandler FindComplete;

    public FindCustomerUserControl()
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
        String customerID = this.CustomerIdTextBox.Text;
        Customer registeredcustomer = ABCHardware.FindCustomer(uint.Parse(customerID));
        OnFindComplete(registeredcustomer);
    }

    // Invoke the find complete event
    protected virtual void OnFindComplete(Customer registeredCustomer)
    {
        if (FindComplete != null)
            FindComplete(this, registeredCustomer);
    }
}