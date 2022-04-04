using System;
using System.Data.SqlClient;

namespace Project1.Models
{
    public class Order
    {
        public int orderID { get; set; }
        public int customerID  { get; set; }
        public double orderTotal { get; set; }

        SqlConnection con = new SqlConnection("server=DESKTOP-VE9QC92\\TRAINERINSTANCE;database=shoppingApp; integrated security=true");

        #region Add Order to DB
        public int addOrder()
        {
            SqlCommand addOrder = new SqlCommand("insert into Orders values (@customerID,@orderTotal)", con);
            addOrder.Parameters.AddWithValue("@customerID",customerID);
            addOrder.Parameters.AddWithValue("@orderTotal",orderTotal);
            SqlCommand getAssignedID = new SqlCommand("select max(orderID) from Orders", con);
            SqlDataReader reader;
            int ID = 0;
            try
            {
                con.Open();
                addOrder.ExecuteNonQuery();
                reader = getAssignedID.ExecuteReader();
                if (reader.Read())
                    ID = Convert.ToInt32(reader[0]);
                return ID;
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }    
        }
        #endregion
    }
}
