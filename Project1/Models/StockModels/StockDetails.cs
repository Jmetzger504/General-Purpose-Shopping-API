using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Project1.Models.StockModels
{
    public class StockDetails
    {
        SqlConnection con = new SqlConnection("server=DESKTOP-VE9QC92\\TRAINERINSTANCE;database=shoppingApp; integrated security=true");

        #region Add Stock to Database
        public string addStockItem(newStock stock)
        {
            SqlCommand addStockItem = new SqlCommand("insert into Stock values (@Name,@Category,@Price,@Quantity,@Brand)",con);
            addStockItem.Parameters.AddWithValue("@Name",stock.Name);
            addStockItem.Parameters.AddWithValue("@Category",stock.Category);
            addStockItem.Parameters.AddWithValue("@Price",stock.Price);
            addStockItem.Parameters.AddWithValue("@Quantity",stock.Quantity);
            addStockItem.Parameters.AddWithValue("@Brand",stock.Brand);

            try
            {
                con.Open();
                addStockItem.ExecuteNonQuery();
                return "Stock Item added successfully!";
            }
            catch(SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region List Stock
        public List<Stock> listAllStock()
        {
            SqlCommand listStock = new SqlCommand("Select * from Stock", con);
            List<Stock> stockList = new List<Stock>();
            SqlDataReader reader;

            
            try
            {
                con.Open();
                reader = listStock.ExecuteReader();

                while (reader.Read())
                {
                    stockList.Add(new Stock()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToDouble(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Brand = reader["Brand"].ToString()
                    });
                }
                return stockList;
            }
            catch(SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        

       

        

    }
}
