using System;
using System.Data.SqlClient;
namespace Project1.Models
{
    public class OrderedItem
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int Amount { get; set; }

        SqlConnection con = new SqlConnection("server=DESKTOP-VE9QC92\\TRAINERINSTANCE;database=shoppingApp; integrated security=true");

        #region Check Stock given requested amount
        public int getStock()
        {
            SqlCommand getStock = new SqlCommand("select Quantity from Stock where ID = @ID", con);
            getStock.Parameters.AddWithValue("@ID",ItemID);
            int stock = 0;
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = getStock.ExecuteReader();
                if(reader.Read())
                {
                    stock = reader.GetInt32(0);
                }
                return stock;
            }
            catch(SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region Add OrderedItem to DB
        public void addToDB()
        {
            SqlCommand addToDB = new SqlCommand("insert into OrderedItems values (@orderID,@itemID,@Amount)", con);
            addToDB.Parameters.AddWithValue("@orderID",OrderID);
            addToDB.Parameters.AddWithValue("@itemID",ItemID);
            addToDB.Parameters.AddWithValue("@Amount",Amount);

            try
            {
                con.Open();
                addToDB.ExecuteNonQuery();
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region Update Stock
        public void updateStock(int stock)
        {
            SqlCommand updateStock = new SqlCommand("update Stock set Quantity = @stock where ID = @ItemID", con);
            updateStock.Parameters.AddWithValue("@stock", stock);
            updateStock.Parameters.AddWithValue("@ItemID", ItemID);

            try
            {
                con.Open();
                updateStock.ExecuteNonQuery();
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region Get total price of ordered item given ID and amount.
        public double getTotalPrice()
        {
            double totalPrice = 0;
            SqlCommand getTotalPrice = new SqlCommand("select Price from Stock where ID = @ID", con);
            getTotalPrice.Parameters.AddWithValue("@ID", ItemID);
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = getTotalPrice.ExecuteReader();

                if(reader.Read())
                {
                    totalPrice = Convert.ToDouble(reader["Price"]);

                }

                totalPrice *= Amount;
                return totalPrice;
            }
            catch(SqlException ex) { throw new System.Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion
    }
}
