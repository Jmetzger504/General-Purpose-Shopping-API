using System;
using System.Data.SqlClient;
namespace Project1.Models
{
    public class Stock
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        #endregion

        SqlConnection con = new SqlConnection("server=DESKTOP-VE9QC92\\TRAINERINSTANCE;database=shoppingApp; integrated security=true");

        #region Search Stock By ID
        public Stock searchStock(int ID)
        {
            SqlCommand searchStock = new SqlCommand("select * from Stock where ID = @ID",con);
            searchStock.Parameters.AddWithValue("@ID", ID);
            SqlDataReader reader;
            Stock stock = new Stock();

            try
            {
                con.Open();
                reader = searchStock.ExecuteReader();
                while(reader.Read())
                {
                    stock.Id = Convert.ToInt32(reader["ID"]);
                    stock.Name = reader["Name"].ToString();
                    stock.Category = reader["Category"].ToString();
                    stock.Price = Convert.ToDouble(reader["Price"]);
                    stock.Quantity = Convert.ToInt32(reader["Quantity"]);
                    stock.Brand = reader["Brand"].ToString();
                }

                return stock;
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region Update Stock Quantity
        public void updateQuantity()
        {
            SqlCommand updateQuantity = new SqlCommand("update Stock set Quantity = @Quantity where ID = @Id", con);
            updateQuantity.Parameters.AddWithValue("@Quantity", Quantity);
            updateQuantity.Parameters.AddWithValue("@Id", Id);

            try
            {
                con.Open();
                updateQuantity.ExecuteNonQuery();
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region Update Stock Price
        public void updatePrice(double Price)
        {
            SqlCommand updatePrice = new SqlCommand("update Stock set Price = @Price where ID = @ID", con);
            updatePrice.Parameters.AddWithValue("@Price", Price);
            updatePrice.Parameters.AddWithValue("@ID", Id);

            try
            {
                con.Open();
                updatePrice.ExecuteNonQuery();
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region Delete Stock Item
        public void deleteStockItem()
        {
            SqlCommand deleteStockItem = new SqlCommand("delete from Stock where ID = @Id", con);
            deleteStockItem.Parameters.AddWithValue("@Id", Id);

            try
            {
                con.Open();
                deleteStockItem.ExecuteNonQuery();
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion


    }
}
