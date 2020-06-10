create database CptXXSession2
go
use CptXXSession2
go
--Create table Countries
create table Countries
(
	CountryID varchar(20) not null primary key,
	Name varchar(20) not null
)
--create table Offices
create table Offices
(
	OfficeID varchar(20) not null primary key,
	CountryID varchar(20) not null,
	Title varchar(20) not null,
	Phone int not null,
	Contact varchar(20) not null,
	constraint FK1 foreign key(CountryID) references Countries(CountryID) 
)
--create table Roles
create table Roles
(
	RoleID varchar(20) not null primary key,
	Title varchar(20) not null
)
--create table Users
create table Users
(
	UserID varchar(20) not null primary key,
	RoleID varchar(20) not null,
	OfficeID varchar(20) not null,
	Email varchar(20) not null,
	Passwords varchar(20) not null,
	FirstName varchar(20) not null,
	LastName varchar(20) not null,
	Birthdate datetime not null,
	Active varchar(20) not null,
	Constraint FK2 foreign key (RoleID) references Roles(RoleID),
	Constraint FK3 foreign key (OfficeID) references Offices(OfficeID)
)

--create table Aircafts
create table Aircrafts
(
	AircraftID varchar(20)not null primary key,
	Name varchar(20) not null,
	MakeModel varchar(20) not null,
	Totalseats int not null,
	EconomySeats int not null,
	BusinessSeats int not null
)
--create table Airports
create table Airports
(
	AirportID varchar(20) not null  primary key,
	CountryID varchar(20) not null,
	IATACode varchar(20) not null,
	Name varchar(20) not null,
	Constraint FK4 foreign key(CountryID) references Countries(CountryID)
)
--create table Router
create table Routers
(
	RouteID varchar(20) not null primary key,
	DepartureAirportID varchar(20) not null,
	ArrivalAirportID varchar(20) not null,
	Distance int not null,
	FlightTime varchar(20) not null ,
	constraint FK5 foreign key (DepartureAirportID) references Airports(AirportID),
	constraint FK6 foreign key(ArrivalAirportID) references Airports(AirportID) 
)
--create table Schedules
create table Schedules
(
	ScheduleID int identity(1,1) not null  primary key,
	Date date not null,
	Times time not null,
	AircraftID varchar(20)not null,
	RouteID  varchar(20) not null,
	FlightNumber int not null ,
	EconomyPrice int not null,
	Confirmed varchar(20) not null,
	constraint FK7 foreign key (AircraftID) references Aircrafts(AircraftID),
	constraint FK8 foreign key (RouteID ) references Routers(RouteID)
)
drop table Schedules
--Insert table Countries
insert into Countries values('C01','Viet Nam')
insert into Countries values('C02','Korean')
insert into Countries values('C03','Thailand')
insert into Countries values('C04','Japan')
insert into Countries values('C05','China')

--Truy vấn tới bảng Countries
select * from Countries

--Insert table Offices
insert into Offices values('O01','C01','Title1',0788433339,'Contact to VietNam')
insert into Offices values('O02','C02','Title2',0962584254,'Contact to Korean')
insert into Offices values('O03','C05','Title3',0329364963,'Contact to China')
insert into Offices values('O04','C02','Title4',0976582379,'Contact to Korean')
insert into Offices values('O05','C03','Title5',0367258780,'Contact to Thailand')

--Truy vấn tới bảng Offices
select * from Offices

--Insert table Roles
insert into Roles values('R01','RTitle1')
insert into Roles values('R02','RTitle2')
insert into Roles values('R03','RTitle3')
insert into Roles values('R04','RTitle4')
insert into Roles values('R05','RTitle5')

--Câu lệnh truy vấn tới bảng Offices
select * from Roles

--Insert table Users
insert into Users values('U01','R01','O04','Thien@gmail.com','12345','Thien','Hoang','10/7/1999','Active')
insert into Users values('U02','R02','O02','Maria@gmail.com','23345','Maria','John','01/06/1998','Inactive')
insert into Users values('U03','R05','O02','Santo@gmail.com','1233','Santo','Kim','12/10/1990','Active')
insert into Users values('U04','R04','O05','Taylor@gmail.com','25127','Taylor','Swift','11/12/1989','Inactive')
insert into Users values('U05','R04','O01','Duong@gmail.com','4318','Duong','Duong','5/10/1990','Active')

--Câu lệnh truy vấn tới bảng Users
select * from Users

--Insert table Aircafts
insert into Aircrafts values('AC01','Viet Nam Airlines','M01',500,450,50)
insert into Aircrafts values('AC02','America Boing','M02',450,350,100)
insert into Aircrafts values('AC03','Vietjet Air','M03',300,250,50)
insert into Aircrafts values('AC04','Japan Airlines','M04',700,600,100)
insert into Aircrafts values('AC05','China Airlines','M05',850,700,150)

--Câu lệnh truy vấn tới bảng Aircafts
select * from Aircrafts

--Insert table Airports
insert into Airports values('AP01','C03','IT01','Viet Nam Airport')
insert into Airports values('AP02','C02','IT02','Korean Airport')
insert into Airports values('AP03','C03','IT03','Indonesia Airport')
insert into Airports values('AP04','C02','IT04','China Airport')
insert into Airports values('AP05','C05','IT05','Japan Airport')

--Câu lệnh truy vấn tới bảng Airports
select * from Airports

--Insert table Routers
insert into Routers values('RT1','AP01','AP02',2000,'8:00')
insert into Routers values('RT2','AP03','AP04',1000,'9:00')
insert into Routers values('RT3','AP03','AP01',3000,'10:00')
insert into Routers values('RT4','AP02','AP01',500,'11:00')
insert into Routers values('RT5','AP05','AP03',5000,'12:00')

--Câu lệnh truy vấn tới bảng Routers
select * from Routers

--Insert table Schedules
insert into Schedules values('2019/02/02','8:30','AC01','RT2',1,100,'Confirm Flight')
insert into Schedules values('2019/05/10','9:30','AC02','RT1',2,150,'Confirm Flight')
insert into Schedules values('2019/06/02','10:30','AC03','RT3',3,900,'Cancel Flight')
insert into Schedules values('2019/02/02','11:30','AC04','RT5',4,400,'Cancel Flight')
insert into Schedules values('2019/02/02','12:30','AC05','RT4',5,100,'Confirm Flight')

--Câu lệnh truy vấn tới bảng Schedules
select * from Schedules
create view show as
select Date,Times,DepartureAirportID,ArrivalAirportID,FlightNumber,AircraftID,EconomyPrice,EconomyPrice*1.3 as 'Business Price',Confirmed
From Routers,Schedules
Where Routers.RouteID=Schedules.RouteID;

Select * From show;
drop table Schedules