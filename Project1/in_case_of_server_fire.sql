drop table OrderedItems, Orders,Stock,Customer;

create table Customer (
	ID int identity(1,1) primary key,
	Name varchar(40),
	Email varchar(40),
	Password varchar(40),
	Address varchar(100),
	Balance money,
	invoiceTotal money
);

insert into Customer (Name,Email,Password,Address,Balance,invoiceTotal) values 
('Julian Metzger','Jmetzger5@gmail.com','myPassword','123 Dox Myself Lane',42,57),
('Jeff Bezos','Jeff@gmail.com','JeffTheBest','42 Jeff Street',999999999999,101747),
('Kanye','Kanye@gmail.com','Kanye','Kanye World',555555555,98),
('Patches','Patches@gmail.com','TheLegendNeverDies','Patches Emporium',1200,508025),
('Darth Revan','Amnesia@gmail.com','Grey Jedi','Outer Rim Territories',789456,0),
('Darth Malak','IronJawIsCool@gmail.com','IHateRevan','Squinquargesimus, planet Quelii',10000,0)

create table Stock (
	ID int identity(1,1) primary key,
	Name varchar(40),
	Category varchar(20),
	Price money,
	Quantity int,
	Brand varchar(40)
)

insert into Stock (Name,Category,Price,Quantity,Brand) values
('Burger','Fast Food',5,76,'Carls Jr.'),
('Hot Dog','Fast Food',999999999999,1.5,'Costco'),
('Brawndo','Electrolytic Drink',47,456123,'BRAWNDO'),
('Brawndo','Crop Fertilizer',47,456123,'BRAWNDO'),
('iPhone 4','Cell Phone',25,22,'Apple'),
('Mask of the Father','Head Armor',8000,1,'Bandai Namco'),
('X-Wing','Starfighter',500000,1,'Incom-FreiTek Corporation'),
('Model B','Grand Piano',101700,2,'Steinway & Sons'),
('House of Julian','Real Estate',1,42,'Metzger Realty')

create table Orders (
	orderID int identity(1,1) primary key,
	customerID int foreign key references Customer(ID) on delete cascade,
	orderTotal money
)

--Julian,Jeff,Julian,Kanye,Jeff,Patches
insert into Orders values (1,52),(2,101747),(1,97),(4,500000),(3,1),(1,5),(4,8000),(4,25)



create table OrderedItems (
	orderID int foreign key references Orders(orderID) on delete cascade,	--Order the item is a part of
	itemID int foreign key references Stock(ID) on delete cascade,	--What item was ordered
	Amount int not null, --How much of it they ordered
)
insert into OrderedItems values
(1,1,1),
(1,3,1),
(2,8,1),
(2,3,1),
(3,2,2),
(3,4,2),
(4,7,1),
(5,9,1),
(6,1,1),
(7,6,1),
(8,5,1)

select * from Customer;
select * from Stock;
select * from Orders;
select * from OrderedItems;