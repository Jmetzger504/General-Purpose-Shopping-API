using System;
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
                return Created("Customer successfully added!", detailsModel.AddCustomer(newCustomer));
            }
            catch (Exception ex) {
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

        #region  Get Customer Invoice
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

        #region Deposit Customer Funds
        [HttpPost]
        [Route("depositCustomerFunds")]
        public IActionResult depositFunds(int Id,double deposit)
        {
            //Validate input
            if (deposit < 0)
                return BadRequest("Only positive funds, friend.");
            else if (Id <= 0)
                return BadRequest("Please input a valid ID number");
            //Get current balance
            try
            {
                Customer customer = customerModel.searchCustomer(Id);
                double balance = customer.Balance;
                customer.Balance += deposit;
                customer.updateBalance();
                return Ok("Funds succesfully added for " + customer.Name + "\n" +
                    "Customer ID: " + customer.Id + "\n" +
                    "Deposit Amount: $" + Math.Round(deposit, 2) + "\n" +
                    "New Balance: $" + Math.Round(customer.Balance, 2));  ;
            }
            catch(System.Exception ex) { return BadRequest(ex.Message); }



        }
        #endregion

        #region Delete Customer
        [HttpDelete]
        [Route("deleteCustomer")]
        public IActionResult deleteCustomer(int Id)
        {
            if (Id <= 0)
                return BadRequest("Only valid ID numbers please.");

            Customer customer;
            
            try 
            {
                customer = customerModel.searchCustomer(Id);
            }
            catch(System.Exception ex) { return NotFound(ex.Message); }

            try
            {
                customer.deleteCustomer();
                return Ok("Customer account successfully deleted!");
            }
            catch (System.Exception ex) { return NotFound(ex.Message); }
        }
        #endregion

    }
}
