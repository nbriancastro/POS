using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ABCPOS
{
    public Int32 ProcessSale(Sale ABCSale)
    {
        Int32 SaleNumber;
        Sales SalesManager = new Sales();
        try
        {
            if (ABCSale.SaleItems.Count < 1)
                throw new EmptySaleException("Sale has no purchased items.");

            SaleNumber = SalesManager.AddSale(ABCSale);
        }
        catch (SqlException sqlEx)
        {
            if ((sqlEx.Class == 13) && (sqlEx.State == 2))
                throw new InsufficientStockException(sqlEx);
            throw;
        }

        return SaleNumber;
    }

    public IList<Customer> GetActiveCustomers()
    {
        Customers customerManager = new Customers();
        return customerManager.GetCustomers();
    }

    public Customer FindCustomer(UInt32 customerId)
    {
        Customers customerManager = new Customers();
        var customer = customerManager.GetCustomer(customerId);
        return customer;
    }
    public UInt32 AddCustomer(String customerName, String address, String city, String province, String postal)
    {
        Customers customerManager = new Customers();
        Customer customer = new Customer(
            name: customerName,
            address: address,
            city: city,
            province: province,
            postal: postal);

        var customerId = customerManager.AddCustomer(customer);
        return customerId;
    }
    public Boolean UpdateCustomer(Customer customer)
    {
        Customers customerManager = new Customers();
        var confirmation = customerManager.UpdateCustomer(customer);
        return confirmation;
    }
    public Boolean DeleteCustomer(Customer customer)
    {
        Customers customerManager = new Customers();
        customer.IsDeleted = true;
        var confirmation = customerManager.UpdateCustomer(customer);
        return confirmation;
    }

    public IList<Item> GetActiveItems()
    {
        Items customerManager = new Items();
        return customerManager.GetItems();
    }

    public Item FindItem(String itemCode)
    {
        Items itemManager = new Items();
        var item = itemManager.GetItem(itemCode);
        return item;
    }
    public Boolean AddItem(String itemCode, String description, Decimal unitPrice, Int32 stock)
    {
        Items itemManager = new Items();
        Item item = new Item(
            itemCode: itemCode,
            description: description,
            unitPrice: unitPrice,
            stock: stock);

        var confirmation = itemManager.AddItem(item);
        return confirmation;
    }
    public Boolean UpdateItem(Item item)
    {
        Items itemManager = new Items();
        var confirmation = itemManager.UpdateItem(item);
        return confirmation;
    }
    public Boolean DeleteItem(Item item)
    {
        Items itemManager = new Items();
        item.IsDeleted = true;
        var confirmation = itemManager.UpdateItem(item);
        return confirmation;
    }
}

public class InsufficientStockException : SystemException
{
    public InsufficientStockException(SqlException sqlEx) : base(sqlEx.Message, sqlEx) {}
}

public class EmptySaleException : SystemException
{
    public EmptySaleException() : base() { }
    public EmptySaleException(String message) : base(message) { }
}