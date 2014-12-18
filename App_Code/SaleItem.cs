using System;
using System.Collections.Generic;
using System.Web;

public class SaleItem
{
    public SaleItem(String itemCode, Int32 quantity)
    {
        this._itemCode = itemCode;
        this._quantity = quantity;
    }

    private String _itemCode;
    public String ItemCode
    {
        get { return _itemCode; }
        set { _itemCode = value; }
    }
    private Int32 _quantity;
    public Int32 Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
}