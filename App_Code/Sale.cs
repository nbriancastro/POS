using System;
using System.Collections.Generic;

public class Sale
{
    public Sale(DateTime saleDate, String salesPerson, Int32 customerId, Decimal gstRate)
    {
        this._saleNumber = 0;
        this._saleDate = saleDate;
        this._salesPerson = salesPerson;
        this._customerId = customerId;
        this._gstRate = gstRate;
        this._saleItems = new List<SaleItem>();
    }

    private Int32 _saleNumber;
    public Int32 SaleNumber
    {
        get { return _saleNumber; }
    }
    private DateTime _saleDate;
    public DateTime SaleDate
    {
        get { return _saleDate; }
        set { _saleDate = value; }
    }
    private String _salesPerson;
    public String SalesPerson
    {
        get { return _salesPerson; }
        set { _salesPerson = value; }
    }
    private Int32 _customerId;
    public Int32 CustomerId
    {
        get { return _customerId; }
        set { _customerId = value; }
    }
    private Decimal _gstRate;
    public Decimal GSTRate
    {
        get { return _gstRate; }
        set { _gstRate = value; }
    }

    private IList<SaleItem> _saleItems;
    public IList<SaleItem> SaleItems
    {
        get { return _saleItems; }
    }

    public void AddItem(String itemCode, Int32 quantity)
    {
        this._saleItems.Add(new SaleItem(itemCode, quantity));
    }
}