using System;
using System.Configuration;

public class DataManager
{
    protected String ConnectionString = String.Empty;

    public DataManager()
    {
        // ReadS database connection string from configuration
        ConnectionStringSettingsCollection configConnectionStrings = ConfigurationManager.ConnectionStrings;
        if (configConnectionStrings.Count > 0)
        {
            this.ConnectionString = configConnectionStrings["defaultConnection"].ConnectionString;
        }
    }

    public DataManager(String connection)
    {
        // ReadS database connection string from configuration
        ConnectionStringSettingsCollection configConnectionStrings = ConfigurationManager.ConnectionStrings;
        if (configConnectionStrings.Count > 0)
        {
            this.ConnectionString = configConnectionStrings[connection].ConnectionString;
        }
    }
}