using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Project1.Models.OrderModels;
using Project1.Models;

namespace Project1.Controllers
{
    public class OrderDetailsController : Controller
    {
        OrderDetails model = new OrderDetails();

        #region Get all orders
        [HttpGet]
        [Route("listOfOrders")]
        public IActionResult getAllOrders()
        {
            try
            {
                return Ok(model.getOrders());
            }
            catch (System.Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion

        #region Get ordered items by order id
        [HttpGet]
        [Route("orderDetailsByID")]
        public IActionResult getOrderedItems(int orderID)
        {
            try
            {
                return Ok(model.getOrderedItems(orderID));
            }
            catch(System.Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion

        #region Get order and its items by order id
        [HttpGet]
        [Route("getOrderandItemsByID")] 
        public IActionResult getOrderandItems(int orderID)
        {
            try
            {
                return Ok(model.getOrderAndItems(orderID));
            }
            catch (System.Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion

        #region Place Order
        [HttpPost]
        [Route("PlaceOrder")]
        //FIX THIS INPUT FORMAT
        public IActionResult placeOrder(int customerID, List<int> itemID,List<int> itemAmount)
        {
            List<OrderedItem> pendingOrder = new List<OrderedItem>();
            for (int i = 0; i < itemID.Count; i++)
            {
                pendingOrder.Add(new OrderedItem {
                    Amount = itemAmount[i],
                    ItemID = itemID[i]
                });
            }
            try
            {
                return Ok(model.placeOrder(customerID,pendingOrder));
            }
            catch (System.Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion
    }
}
