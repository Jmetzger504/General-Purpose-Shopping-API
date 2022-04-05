using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using Project1.Models.OrderModels;

namespace Project1.Models
{
    public class CustomerDetails
    {
        SqlConnection con = new SqlConnection("server=DESKTOP-VE9QC92\\TRAINERINSTANCE;database=shoppingApp; integrated security=true");



        #region Add New Customer to Database
        public string AddCustomer(NewCustomer newCustomer)
        {
            SqlCommand addCustomer = new SqlCommand("insert into Customer values (@Name,@Email,@Password,@Address,@Balance,@invoiceTotal)", con);
            addCustomer.Parameters.AddWithValue("@Name", newCustomer.Name);
            addCustomer.Parameters.AddWithValue("@Email", newCustomer.Email);
            addCustomer.Parameters.AddWithValue("@Password", newCustomer.Password);
            addCustomer.Parameters.AddWithValue("@Address", newCustomer.Address);
            addCustomer.Parameters.AddWithValue("@Balance", newCustomer.Balance);
            addCustomer.Parameters.AddWithValue("@invoiceTotal", 0);

            try
            {
                con.Open();
                addCustomer.ExecuteNonQuery();
                return "Customer added succesfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                con.Close();
                newCustomer.Password = "";
            }

        }
        #endregion

        #region Get Customer's Order ID's via Customer ID
        public List<int> getOrderIDs(int customerID)
        {
            List<int> orderIDs = new List<int>();
            SqlCommand getOrderIDs = new SqlCommand("select orderID from Orders where customerID = @customerID", con);
            getOrderIDs.Parameters.AddWithValue("@customerID", customerID);
            SqlDataReader reader;
            try
            {
                con.Open();
                reader = getOrderIDs.ExecuteReader();

                while (reader.Read())
                {
                    orderIDs.Add(Convert.ToInt32(reader["orderID"]));
                }
                return orderIDs;
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }

        }
        #endregion

        #region Get Customer Invoice Total
        public double getCustomerInvoiceTotal(int custID)
        {
            double total = 0;
            SqlCommand getCustomerInvoiceTotal = new SqlCommand("select invoiceTotal from Customer where ID = @ID", con);
            getCustomerInvoiceTotal.Parameters.AddWithValue("@ID", custID);
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = getCustomerInvoiceTotal.ExecuteReader();

                if (reader.Read())
                {
                    total = Convert.ToDouble(reader["invoiceTotal"]);
                }

                return total;

            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }

        }
        #endregion

        #region Get all of a Customer's Orders by Customer ID with Total (Invoice)
        public List<OrderDetails> getCustomerOrders(int customerID)
        {
            //Get customer info via customer ID
            Customer customer = new Customer();
            Customer loadCustomer = customer.searchCustomer(customerID);
            //Get orderID's via customer ID
            List<int> orderIDs = getOrderIDs(customerID);


            List<OrderDetails> orderInvoice = new List<OrderDetails>();
            //Get first item, include name!

            //Get OrderDetails via orderID
            foreach (int orderID in orderIDs)
            {
                OrderDetails orderDetail = new OrderDetails();
                try
                {

                    orderDetail = orderDetail.getOrderAndItems(orderID);
                    orderDetail.Name = loadCustomer.Name;   //Inefficient :(
                    orderInvoice.Add(orderDetail);
                }
                catch (SqlException ex) { throw new Exception(ex.Message); }

            }

            return orderInvoice;

        }
        #endregion

        #region List all Customers
        public List<Customer> listAllCustomers()
        {
            SqlCommand listCustomers = new SqlCommand("select ID,Name,Email,Address,Balance from Customer", con);
            List<Customer> customerList = new List<Customer>();
            SqlDataReader reader;
            try
            {
                con.Open();
                reader = listCustomers.ExecuteReader();

                while (reader.Read())
                {
                    customerList.Add(new Customer()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString(),
                        Balance = Convert.ToDouble(reader["Balance"])
                    });
                }
                return customerList;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                ;
            }
        }
        #endregion

       


    }
}
