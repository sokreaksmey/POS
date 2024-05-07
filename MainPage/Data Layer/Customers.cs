using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainPage.Data_Layer
{
    public class Customers
    {
        public static DataTable GetAll()
        {
            OracleCommand command = new OracleCommand("CustomerGet", POSContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter dapter = new OracleDataAdapter(command);
            DataTable table = new DataTable();
            dapter.Fill(table);
            return table;
        }
        public static Customer Get(int customerid)
        {
            Customer customer = null;
            OracleCommand command = new OracleCommand("CustomerGet",
           POSContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("P_CustomerId", customerid);
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customer = new Customer();
                customer.CustomerId = Convert.ToInt32(reader["CustomerId"].ToString());
                customer.CustomerName = reader["CustomerName"].ToString();
                customer.CompanyName = reader["CompanyName"].ToString();
                customer.Phone = reader["Phone"].ToString();
                customer.Email = reader["Email"].ToString();
                customer.Address = reader["Address"].ToString();
            }
            return customer;

        }
        public static void Add(Customer customer)
        {

            OracleCommand command = new OracleCommand("CustomerAdd",
           POSContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("P_CustomerName", customer.CustomerName);
            command.Parameters.Add("P_CompanyName", customer.CompanyName);
            command.Parameters.Add("P_Phone", customer.Phone);
            command.Parameters.Add("P_Email", customer.Email);
            command.Parameters.Add("P_Address", customer.Address);
            command.ExecuteNonQuery();
        }

        public static void Update(Customer customer)
        {
            OracleCommand command = new OracleCommand("CustomerUpdate",
           POSContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("P_CustomerId", customer.CustomerId);
            command.Parameters.Add("P_CustomerName", customer.CustomerName);
            command.Parameters.Add("P_CompanyName", customer.CompanyName);
            command.Parameters.Add("P_Phone", customer.Phone);
            command.Parameters.Add("P_Email", customer.Email);
            command.Parameters.Add("P_Address", customer.Address);
            command.ExecuteNonQuery();
        }

        public static void Delete(int customerid)
        {
            try
            {
                OracleCommand command = new OracleCommand("CustomerDelete",POSContext.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_CustomerId", customerid);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }

        }
    }
}


