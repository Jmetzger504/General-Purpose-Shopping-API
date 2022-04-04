using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Project1.Models.OrderModels
{
    public class OrderDetails
    {
        //Delete if unused!!
        public string Name { get; set; }
        public Order order {get; set;}
        public List<OrderedItem> items {get; set;}

        SqlConnection con = new SqlConnection("server=DESKTOP-VE9QC92\\TRAINERINSTANCE;database=shoppingApp; integrated security=true");

        #region Get Order by ID
        public Order getOrder(int orderID)
        {
            Order order = new Order();
            SqlCommand getOrder = new SqlCommand("select * from Orders where orderID = @orderID", con);
            getOrder.Parameters.AddWithValue("@orderID", orderID);
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = getOrder.ExecuteReader();
                while(reader.Read())
                {
                    order.orderID = Convert.ToInt32(reader["orderID"]);
                    order.customerID = Convert.ToInt32(reader["customerID"]);
                    order.orderTotal = Convert.ToInt32(reader["orderTotal"]);
                }
                return order;
            }
            catch (SqlException ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        #endregion

        #region List All Orders without Items
        public List<Order> getOrders()
        {
            SqlCommand listOrders = new SqlCommand("select * from Orders", con);
            List<Order> orderList = new List<Order>();
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = listOrders.ExecuteReader();
                while(reader.Read())
                {
                    orderList.Add(new Order()
                    {
                        orderID = Convert.ToInt32(reader["orderId"]),
                        customerID = Convert.ToInt32(reader["customerID"]),
                        orderTotal = Convert.ToDouble(reader["orderTotal"])
                    });
                }
                return orderList;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally { con.Close(); }

        }
        #endregion

        #region Get Ordered Items by order ID
        public List<OrderedItem> getOrderedItems(int orderId)
        {
            List<OrderedItem> orderList = new List<OrderedItem>();
            SqlCommand getItems = new SqlCommand("select * from OrderedItems where orderID = @Id",con);
            getItems.Parameters.AddWithValue("@Id", orderId);
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = getItems.ExecuteReader();
                while(reader.Read())
                {
                    orderList.Add(new OrderedItem()
                    {
                        OrderID = Convert.ToInt32(reader["orderID"]),
                        ItemID = Convert.ToInt32(reader["itemID"]),
                        Amount = Convert.ToInt32(reader["Amount"])

                    });
                    
                }
                this.items = orderList;
                return this.items;
            }
            catch(SqlException ex) { throw new Exception(ex.Message);}
            finally { con.Close(); }
        }
        #endregion

        #region Get OrderDetails via orderID
        public OrderDetails getOrderAndItems(int orderID)
        {
            OrderDetails orderDetails = new OrderDetails();
            Customer customer = new Customer();
           
            orderDetails.order = orderDetails.getOrder(orderID);
            orderDetails.items = orderDetails.getOrderedItems(orderID);
            return orderDetails;
        }
        #endregion

        #region Place Order
        //FIX INPUT FORMAT List of (itemId,amount) which become OrderedItems.
        public string placeOrder(int customerID, List<OrderedItem> pendingOrder)
        {
            //Get customer balance
            Customer customer = new Customer();
            customer = customer.searchCustomer(customerID);

            //Get stock and total price of ordered items. Compare to balance.

            //Get total price for all ordered items. Return if it costs too much.
            double totalPrice = 0;
            foreach(OrderedItem item in pendingOrder)
            {
                totalPrice += item.getTotalPrice();
            }
            if (totalPrice > customer.Balance)
                return "Not enough funds in your account to complete this order... :(";
            else if (totalPrice == 0)
                return "Something seems to have gone wrong with this order. :(";
            //Check stock
            int stock = 0;
            foreach(OrderedItem item in pendingOrder)
            {
                stock = item.getStock();
                if (stock < item.Amount)
                    return "Sorry, Item with ID: " + item.ItemID + " cannot supply quantity: " + stock +" :(";
            }

            //If we have gotten this far, we can confirm the order!
            //Add Order to DB given customerID and totalPrice
            Order newOrder = new Order();
            newOrder.customerID = customerID;
            newOrder.orderTotal = totalPrice;
            int newOrderID = newOrder.addOrder();
            
            
            foreach(OrderedItem item in pendingOrder)
            {
                item.OrderID = newOrderID; //Assign orderID to OrderedItems
                item.addToDB(); //Add OrderedItems to DB
                //Update Stock DB
                stock = item.getStock();  
                stock -= item.Amount;
                item.updateStock(stock);
            }
            
            //Update customer balance and invoiceTotal.
            customer.Balance -= totalPrice;
            customer.invoiceTotal += totalPrice;
            customer.updateCustomerInvoice();


            return "Order succesfully placed!";
        }
        #endregion

    }
}
