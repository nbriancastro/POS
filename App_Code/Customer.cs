using System;
using System.Collections.Generic;
using System.Web;

public class Customer
{
    internal Customer(String name, String address, String city, String province, String postal, UInt32 customerId = 0, Boolean isDeleted = false)
    {
        this._customerId = customerId;
        this._name = name;
        this._address = address;
        this._city = city;
        this._province = province;
        this._postal = postal;
        this._isDeleted = isDeleted;
    }

    private readonly UInt32 _customerId;
    public UInt32 CustomerId
    {
        get { return _customerId; }
    }

    private String _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private String _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    private String _city;
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    private String _province;
    public string Province
    {
        get { return _province; }
        set { _province = value; }
    }

    private String _postal;
    public string Postal
    {
        get { return _postal; }
        set { _postal = value; }
    }

    private Boolean _isDeleted;
    public Boolean IsDeleted
    {
        get { return _isDeleted; }
        set { _isDeleted = value; }
    }
}