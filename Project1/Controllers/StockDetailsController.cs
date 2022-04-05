using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.Models.StockModels;
using System;
namespace Project1.Controllers
{
    public class StockDetailsController : Controller
    {
        Stock stockModel = new Stock();
        StockDetails stockDetails = new StockDetails();
        
        #region Add Stock Item
        [HttpPost]
        [Route("addStockItem")]
        public IActionResult addStockItem(newStock stock)
        {
            try { return Created("", stockDetails.addStockItem(stock)); }
            catch(Exception ex) { return BadRequest(ex.Message); }
        
        }
        #endregion

        #region Search Stock item by ID
        [HttpGet]
        [Route("searchStock")]
        public IActionResult getStockByID(int ID)
        {
            try 
            {
                Stock stock = stockModel.searchStock(ID);
                if (stock.Id == 0)
                    return NotFound("This ID has no stocked item associated with it. :(");
                else
                    return Ok(stock);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Get All Stock Items
        [HttpGet]
        [Route("stockList")]
        public IActionResult getAllStock()
        {
            try
            {
                return Ok(stockDetails.listAllStock());
            }
            catch (System.Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion

        #region Restock Item
        [HttpPost]
        [Route("Restock Item")]
        public IActionResult restockItem(int Id, int amount)
        {
            //Validate input
            if (amount < 0)
                return BadRequest("Only positive quantities please.");
            else if (Id <= 0)
                return BadRequest("Please input a valid ID number");
            try
            {
                Stock stock = stockModel.searchStock(Id);
                double quantity = stock.Quantity;
                stock.Quantity += amount;
                stock.updateQuantity();
                return Ok("Item successfully restocked!\n" +
                    "Updated item quantity in stock:" + stock.Quantity);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion

        #region Delete Item
        [HttpDelete]
        [Route("deleteStockItem")]
        public IActionResult deleteStockItem(int Id)
        {
            if (Id <= 0)
                return BadRequest("Only valid ID numbers please.");

            Stock stock;
            try
            {
                stock = stockModel.searchStock(Id);
            }
            catch(System.Exception ex) { return NotFound("Item with this ID number not found!"); }
            
            try
            {
                stock.deleteStockItem();
                return Ok("Item succesfully deleted!");
            }
            catch(System.Exception ex) { return BadRequest("Some catastrophic error has occured beyond my mortal comprehension."); }
        }
        #endregion
    }
}
