using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.Models.OrderModels;
namespace Project1.Controllers
{
    public class CustomerDetailsController : Controller
    {
        CustomerDetails detailsModel = new CustomerDetails();
        Customer customerModel = new Customer();
        
        #region Add Customer
        [HttpPost]
        [Route("addCustomer")]
        public IActionResult AddCustomer(NewCustomer newCustomer)
        {
            
            try {
                return Created("", detailsModel.AddCustomer(newCustomer));
            }
            catch (System.Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get All Customers
        [HttpGet]
        [Route("customerList")]
        public IActionResult getAllCustomers()
        {
            try
            {
                return Ok(detailsModel.listAllCustomers());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Search Customer by ID
        [HttpGet]
        [Route("searchCustomer")]
        public IActionResult searchCustomer(int ID)
        {
            try
            {
                Customer customer = customerModel.searchCustomer(ID);
                if (customer.Id == 0)
                    return NotFound("This ID wasn't found :(");
                else
                    return Ok(customer);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region 
        [HttpGet]
        [Route("customerInvoice")]
        public IActionResult orderInvoice(int customerID)
        {
            try
            {
                List<OrderDetails> orderInvoice = detailsModel.getCustomerOrders(customerID);
                if (orderInvoice.Count == 0)
                    return NotFound("No orders found for this customer ID :(");
                else
                    return Ok(orderInvoice);
            }
            catch(System.Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion

        
    }
}
