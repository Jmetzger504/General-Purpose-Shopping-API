using System;
using System.Data.SqlClient;
namespace Project1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public double Balance { get; set; }
        public double invoiceTotal { get; set; }
        SqlConnection con = new SqlConnection("server=DESKTOP-VE9QC92\\TRAINERINSTANCE;database=shoppingApp; integrated security=true");
       
        #region Search Customer by ID
        public Customer searchCustomer(int ID)
        {
            SqlCommand searchCustomer = new SqlCommand("select ID,Name,Email,Address,Balance,invoiceTotal from Customer where ID = @ID", con);
            searchCustomer.Parameters.AddWithValue("@ID", ID);
            SqlDataReader reader;
            Customer customer = new Customer();

            try
            {
                con.Open();
                reader = searchCustomer.ExecuteReader();
                while (reader.Read())
                {
                    customer.Id = Convert.ToInt32(reader["ID"]);
                    customer.Name = reader["Name"].ToString();
                    customer.Email = reader["Email"].ToString();
                    customer.Address = reader["Address"].ToString();
                    customer.Balance = Convert.ToDouble(reader["Balance"]);
                    customer.invoiceTotal = Convert.ToDouble(reader["invoiceTotal"]);
                }

                return customer;
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region Update Customer balance and invoice total
        public void updateCustomerInvoice()
        {
            SqlCommand updateCustomer = new SqlCommand("update Customer set Balance = @Balance,invoiceTotal = @invoiceTotal where ID = @ID", con);
            updateCustomer.Parameters.AddWithValue("@Balance", Balance);
            updateCustomer.Parameters.AddWithValue("@invoiceTotal", invoiceTotal);
            updateCustomer.Parameters.AddWithValue("@ID", Id);
            
            try
            {
                con.Open();
                updateCustomer.ExecuteNonQuery();
            }
            catch(SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

    }
}
