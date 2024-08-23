create database Employee_Travel_Booking_SystemDB1

use Employee_Travel_Booking_SystemDB1

create table employees (
    employeeid int primary key identity (1035470,1),
    emp_name varchar(50),
	email varchar(50),
	emp_password varchar(50),
    department varchar(50),
	position varchar(50),
	hiredate date,
    phonenumber varchar(20),
    address varchar(255),
	managerid int,
	status bit,
	foreign key (managerid) references managers(managerid) -- Reference to the managers table
)

INSERT INTO employees (emp_name, email, emp_password, department, position, hiredate, phonenumber, address, managerid)
VALUES
    ('Adikesava', 'adikesava.thummala@infinite.com', 'password', 'Software', 'Software Engineer', '2024-05-30', '+917036775977', 'Chennai', 1025460),
    ('SriPriya', 'sripriya.ragunath@infinite.com', 'password', 'Development', 'Software Developer', '2024-05-30', '+919994344812', 'Bengaluru', 1025461),
	('Sahana', 'sahana.navali@infinite.com', 'password', 'Testing', 'Associate Software Tester', '2024-05-30', '+917892605685', 'Bengaluru', 1025462),
	('Vijendra', 'vijendra.tripathi@infinite.com', 'password', 'Marketing', 'Marketing Manager', '2024-05-30', '+918707794129', 'Chennai', 1025460),
	('Uhalatha', 'palepu.uhalatha@infinite.com', 'password', 'Sales', 'Sale Manager', '2024-05-30', '+917093380920', 'Chennai', 1025460),
	('Nithin', 'nithin.jagadeesh@infinite.com', 'password', 'Support', 'System Engineer', '2024-05-30', '+918904062277', 'Bengaluru', 1025463)




create table managers (
    managerid int primary key identity (1025460,1),
    name varchar(50),
    department varchar(50),
    email varchar(50),
	mgr_password varchar(50),
	status bit,
 
)

INSERT INTO managers (name, department, email, mgr_password, status)
VALUES 
    ('Santosh Tangavel', 'HR', 'santhosh.thangavel@infinite.com', 'password', 1),
    ('Srikanth Denduluri', 'Finance', 'srikanth.denduluri@infinite.com', 'password', 0),
	('Vindhya Vundru', 'Finance', 'vindhya.vundru@infinite.com', 'password', 0),
    ('Sathish Kumar', 'IT', 'sathish.kumar4@infinite.com', 'password', 1);

create table admins (
    adminid int primary key,
    name varchar(100),
    email varchar(100),
	admin_password varchar(50),
)

insert into admins values
	(1,'Admin','admin@gmail.com','password')

create table travelagents (
    agentid int primary key,
    name varchar(100),
    email varchar(100),
	travel_agent_password varchar(50),
	status bit,
)

INSERT INTO travelagents (agentid, name, email, travel_agent_password, status)
VALUES 
    (101, 'MSTravels', 'agent1@example.com', 'password', 1),
    (102, 'KSRTravels', 'agent2@example.com', 'password', 0),
    (103, 'BharathTravels', 'agent3@example.com', 'password', 1),
    (104, 'AmericanTravels', 'agent4@example.com', 'password', 0);

create table travelrequest (
    requestid int primary key IDENTITY(1000,1),
    employeename varchar(100),
    employeeemail varchar(100),
    employeeid int,
    reasonfortravel varchar(255),
    flightneeded varchar(10),
    hotelneeded varchar(10),
    departurecity varchar(100),
    arrivalcity varchar(100),
    departuredate date,
    departuretime time,
    additionalinformation text,
	bookingstatus varchar(50) DEFAULT 'Pending' check (bookingstatus IN ('Confirmed', 'Not available', 'Pending')), -- Restriction on status
	approvalstatus VARCHAR(50) DEFAULT 'Pending' CHECK (approvalstatus IN ('Approved', 'Rejected','Pending', 'Cancelled')),
	foreign key (employeeid) references employees(employeeid)
)

select * from Admins
select * from Employees
select * from managers
select * from TravelAgents
select * from TravelRequest







 