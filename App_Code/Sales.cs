using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
// ReSharper disable InconsistentNaming
// ReSharper disable SuggestVarOrType_BuiltInTypes

public class Sales : DataManager
{
    public Int32 AddSale(Sale ABCSale)
    {
        Int32 SaleNumber;

        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // Open connection
                connection.Open();

                // Execute commands
                #region Process Sale
                SaleNumber = ProcessSale(connection, ABCSale);
                #endregion
                #region Process Sale Items
                ProcessSaleItem(connection, ABCSale, SaleNumber);
                #endregion
                #region Update Item Stock
                UpdateItemStock(connection, ABCSale);
                #endregion

                // Commit transaction scope
                scope.Complete();
            }
        }

        return SaleNumber;
    }

    private Int32 ProcessSale(SqlConnection connection, Sale ABCSale)
    {
        // Prepare command
        SqlCommand cmd = new SqlCommand {
            CommandText = "AddSale",
            CommandType = CommandType.StoredProcedure,
            Connection = connection
        };

        // Input parameters
        // Sale Date
        cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@saleDate",
            SqlDbType = SqlDbType.Date,
            Direction = ParameterDirection.Input,
            IsNullable = false,
            Value = ABCSale.SaleDate
        });
        // Sales Person
        cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@salesPerson",
            SqlDbType = SqlDbType.NVarChar,
            Direction = ParameterDirection.Input,
            IsNullable = false,
            Value = ABCSale.SalesPerson,
        });
        // Customer ID
        cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@customerID",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
            IsNullable = false,
            Value = ABCSale.CustomerId,
        });
        // GST
        cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@gstRate",
            SqlDbType = SqlDbType.SmallMoney,
            Direction = ParameterDirection.Input,
            IsNullable = false,
            Value = ABCSale.GSTRate
        });

        // Call procedure
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
    private void ProcessSaleItem(SqlConnection connection, Sale ABCSale, Int32 SaleNumber)
    {
        // Prepare command
        SqlCommand cmd = new SqlCommand {
            CommandText = "AddSaleItem",
            CommandType = CommandType.StoredProcedure,
            Connection = connection
        };

        // Prepare input parameters
        // Sale Number
        SqlParameter saleNumberParameter = cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@saleNumber",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
            IsNullable = false,
        });
        // Item Code
        SqlParameter itemCodeParameter = cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@itemCode",
            SqlDbType = SqlDbType.Char,
            Direction = ParameterDirection.Input,
            IsNullable = false
        });
        // Quantity
        SqlParameter quantityParameter = cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@quantity",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
            IsNullable = false
        });
            
        // Set parameter value then call procedure
        saleNumberParameter.Value = SaleNumber;
        foreach (SaleItem saleItem in ABCSale.SaleItems)
        {
            itemCodeParameter.Value = saleItem.ItemCode;
            quantityParameter.Value = saleItem.Quantity;
            cmd.ExecuteNonQuery();
        }
    }
    private void UpdateItemStock(SqlConnection connection, Sale ABCSale)
    {
        // Prepare command
        SqlCommand cmd = new SqlCommand {
            CommandText = "UpdateItemStock",
            CommandType = CommandType.StoredProcedure,
            Connection = connection
        };

        // Prepare input parameters
        SqlParameter itemCodeParameter = cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@itemCode",
            SqlDbType = SqlDbType.Char,
            Direction = ParameterDirection.Input,
            IsNullable = false
        });
        SqlParameter quantityParameter = cmd.Parameters.Add(new SqlParameter {
            ParameterName = "@quantity",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
            IsNullable = false
        });

        // Set parameter value then call procedure
        foreach (SaleItem saleItem in ABCSale.SaleItems)
        {
            itemCodeParameter.Value = saleItem.ItemCode;
            quantityParameter.Value = saleItem.Quantity;
            cmd.ExecuteNonQuery();
        }
    }
}