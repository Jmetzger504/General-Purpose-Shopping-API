# General Purpose Shopping API

## Project Description
General purpose RESTful WebAPI for consumers and owners.
Allows customers to view products, order products, view their order history, register an account, and view their invoice.<br>
Allows administrators to CRUD manage products and customer accounts.

## Technologies Used
.NET Framework<br>
ADO.NET<br>
SqlDataClient
## Features
### Customer Model Features
Add a Customer<br>
Get list of Customers<br>
Search Customer by ID<br>
Get Customer Invoice by ID<br>
Deposit Customer Funds<br>
Delete Customer<br>
### Order Model Features
Place an Order for a Customer<br>
Get all Ordered Items for an Order by ID
### Order Item Model Features
Add an Item to Stock<br>
Restock an Item<br>
Update Item price
### Future Features
* Customer Data<br>
1. Customer registration date<br>
2. Customer Login Date Table<br>
3. Customer orders by category<br>
4. Total invoice for Customer per Product category.<br>

* Customer Features<br>
1. Canceling Orders with refund.<br>
2. Canceling specific items within an Order with refund.<br>
3. Delivery checks for ordered Products and their Order.<br>
4. Unique email verification for new Customers.<br>
5. Update Customer info like address and email.<br><br>
* Product Data
5. Item subcategories.
## Getting Started
* Fork this repository.
* Git clone https://github.com/Jmetzger504/General-Purpose-Shopping-API
*  Open the solution file in Visual Studio
* Download Serilog 2.10.0
* Download System.Data.SqlClient 4.8.3
* Download Swashbuckle.AspNetCore 6.3.0
* Setup your database using the provided .bak file.
* Fill in the connection strings with your database (or give it the same name that is already present)
* Run it!
## Usage
Within the Swagger UI, you can easily access any of the current features.<br>
No fields have to be filled in using JSON format, and all input parameters are self-explanatory.
## Contributors
Julian Metzger
## License
MIT License
