using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class Items : DataManager
{
    public Boolean AddItem(Item item)
    {
        Boolean success = false;
        using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "AddItem",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Input parameters
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@itemCode",
                SqlDbType = SqlDbType.Char,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.ItemCode
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@description",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.Description,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@unitPrice",
                SqlDbType = SqlDbType.SmallMoney,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.UnitPrice,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@stock",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.Stock
            });
            // Return parameter
            SqlParameter returnValueParameter = cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@return_status",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue,
                IsNullable = false
            });

            // Call procedure
            connection.Open();
            cmd.ExecuteScalar();
            success = (Int32)returnValueParameter.Value == 0;
        }

        return success;
    }

    public Boolean UpdateItem(Item item)
    {
        Boolean success = false;
        using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "UpdateItem",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Input parameters
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@itemCode",
                SqlDbType = SqlDbType.Char,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.ItemCode
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@description",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.Description,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@unitPrice",
                SqlDbType = SqlDbType.SmallMoney,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.UnitPrice,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@stock",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.Stock
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@isDeleted",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = item.IsDeleted
            });
            // Return parameter
            SqlParameter returnValueParameter = cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@return_status",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue,
                IsNullable = false
            });

            // Call procedure
            connection.Open();
            cmd.ExecuteNonQuery();
            success = (Int32)returnValueParameter.Value == 0;
        }

        return success;
    }

    public Item GetItem(String itemCode)
    {
        Item item = null;
        using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "GetItem",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Input parameters
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@itemCode",
                SqlDbType = SqlDbType.Char,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = itemCode
            });

            // Call procedure
            SqlDataReader reader = null;
            connection.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                item = new Item
                    (
                    itemCode: reader["ItemCode"].ToString(),
                    description: reader["Description"].ToString(),
                    unitPrice: Convert.ToDecimal(reader["UnitPrice"]),
                    stock: Convert.ToInt32(reader["Stock"]),
                    isDeleted: Convert.ToBoolean(reader["IsDeleted"])
                    );
                reader.Close();
            }
        }

        return item;
    }

    public IList<Item> GetItems()
    {
        IList<Item> items = new List<Item>();
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "GetItems",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Call procedure
            SqlDataReader reader = null;
            connection.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                    items.Add(new Item
                        (
                        itemCode: reader["ItemCode"].ToString(),
                        description: reader["Description"].ToString(),
                        unitPrice: Convert.ToDecimal(reader["UnitPrice"]),
                        stock: Convert.ToInt32(reader["Stock"]),
                        isDeleted: Convert.ToBoolean(reader["IsDeleted"])
                        ));
                reader.Close();
            }
        }

        return items;
    }
}