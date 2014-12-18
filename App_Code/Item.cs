using System;
using System.Collections.Generic;
using System.Web;

public class Item
{
    public Item(String itemCode, String description, Decimal unitPrice, Int32 stock, Boolean isDeleted = false)
    {
        this._itemCode = itemCode;
        this._description = description;
        this._unitPrice = unitPrice;
        this._stock = stock;
        this._isDeleted = isDeleted;
    }

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
    private Boolean _isDeleted;
    public Boolean IsDeleted
    {
        get { return _isDeleted; }
        set { _isDeleted = value; }
    }
}