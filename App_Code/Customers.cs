using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

public class Customers : DataManager
{
    public UInt32 AddCustomer(Customer customer)
    {
        UInt32 customerId = 0;
        using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "AddCustomer",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Input parameters
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@name",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Name
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@address",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Address,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@city",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.City,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@province",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Province
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@postalCode",
                SqlDbType = SqlDbType.Char,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Postal
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
            customerId = Convert.ToUInt32(cmd.ExecuteScalar());
        }

        return customerId;
    }

    public Boolean UpdateCustomer(Customer customer)
    {
        Boolean success = false;
        using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "UpdateCustomer",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Input parameters
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@customerId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.CustomerId
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@name",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Name
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@address",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Address,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@city",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.City,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@province",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Province
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@postalCode",
                SqlDbType = SqlDbType.Char,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.Postal
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@isDeleted",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customer.IsDeleted
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

    public Customer GetCustomer(UInt32 customerId)
    {
        Customer customer = null;
        using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "GetCustomer",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Input parameters
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@customerId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                IsNullable = false,
                Value = customerId
            });

            // Call procedure
            SqlDataReader reader = null;
            connection.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                customer = new Customer
                    (
                    name: reader["Name"].ToString(),
                    address: reader["Address"].ToString(),
                    city: reader["City"].ToString(),
                    province: reader["Province"].ToString(),
                    postal: reader["PostalCode"].ToString(),
                    customerId: Convert.ToUInt32(reader["CustomerID"]),
                    isDeleted: Convert.ToBoolean(reader["IsDeleted"])
                    );
                reader.Close();
            }
        }

        return customer;
    }

    public IList<Customer> GetCustomers()
    {
        IList<Customer> customers = new List<Customer>();
        using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        {
            // Prepare command
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "GetCustomers",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            // Call procedure
            SqlDataReader reader = null;
            connection.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                    customers.Add(new Customer
                        (
                        name: reader["Name"].ToString(),
                        address: reader["Address"].ToString(),
                        city: reader["City"].ToString(),
                        province: reader["Province"].ToString(),
                        postal: reader["PostalCode"].ToString(),
                        customerId: Convert.ToUInt32(reader["CustomerID"]),
                        isDeleted: Convert.ToBoolean(reader["IsDeleted"])
                        ));
                reader.Close();
            }
        }

        return customers;
    }
}