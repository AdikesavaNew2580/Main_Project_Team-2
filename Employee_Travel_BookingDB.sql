create database Employee_Travel_Booking_SystemDB

use Employee_Travel_Booking_SystemDB

create table Employees
(
    Empid int primary key identity (1035470,1),
    EmpName varchar(50),
	Email varchar(50),
	EmpPassword varchar(50),
    Department varchar(50),
	Position varchar(50),
	HireDate date,
    PhoneNumber varchar(20),
    Address varchar(255),
	Managerid int,
	Status bit,
	Foreign key (managerid) references managers(managerid) -- Reference to the managers table
)

INSERT INTO employees (EmpName, Email, emppassword, department, position, hiredate, phonenumber, address, managerid)
VALUES
    ('Adikesava', 'adikesava.thummala@infinite.com', 'password', 'Software', 'Software Engineer', '2024-05-30', '7036775977', 'Chennai', 1025460),
    ('SriPriya', 'sripriya.ragunath@infinite.com', 'password', 'Development', 'Software Developer', '2024-05-30', '9994344812', 'Bengaluru', 1025461),
	('Sahana', 'sahana.navali@infinite.com', 'password', 'Testing', 'Associate Software Tester', '2024-05-30', '7892605685', 'Bengaluru', 1025462),
	('Vijendra', 'vijendra.tripathi@infinite.com', 'password', 'Marketing', 'Marketing Manager', '2024-05-30', '8707794129', 'Chennai', 1025460),
	('Uhalatha', 'palepu.uhalatha@infinite.com', 'password', 'Sales', 'Sale Manager', '2024-05-30', '7093380920', 'Chennai', 1025460),
	('Nithin', 'nithin.jagadeesh@infinite.com', 'password', 'Support', 'System Engineer', '2024-05-30', '8904062277', 'Bengaluru', 1025463)



create table managers
(
    ManagerId int primary key identity (1025460,1),
    Name varchar(50),
    Department varchar(50),
    Email varchar(50),
	MgrPassword varchar(50),
	Status bit,
)

INSERT INTO managers (name, department, email, mgrpassword, status)
VALUES 
    ('Santosh Tangavel', 'HR', 'santhosh.thangavel@infinite.com', 'password', 1),
    ('Srikanth Denduluri', 'Finance', 'srikanth.denduluri@infinite.com', 'password', 0),
	('Vindhya Vundru', 'Finance', 'vindhya.vundru@infinite.com', 'password', 0),
    ('Sathish Kumar', 'IT', 'sathish.kumar4@infinite.com', 'password', 1);

create table Admins
(
    Adminid int primary key,
    Name varchar(100),
    Email varchar(100),
	AdminPassword varchar(50),
)

insert into admins values
	(1,'Admin','admin@gmail.com','password')

create table TravelAgents
(
    AgentId int primary key,
    Name varchar(100),
    Email varchar(100),
	TravelAgentPassword varchar(50),
	status bit,
)

INSERT INTO travelagents (agentid, name, email, travelagentpassword, status)
VALUES 
    (101, 'MSTravels', 'agent1@example.com', 'password', 1),
    (102, 'KSRTravels', 'agent2@example.com', 'password', 0),
    (103, 'BharathTravels', 'agent3@example.com', 'password', 1),
    (104, 'AmericanTravels', 'agent4@example.com', 'password', 0);

create table TravelRequest
(
    Requestid int primary key IDENTITY(1000,1),
    EmpName varchar(100),
    EmpMail varchar(100),
    EmpId int,
    ReasonOTtravel varchar(255),
    Flight varchar(10),
    Hotel varchar(10),
    SourceLocation varchar(100),
    DestinationLocation varchar(100),
    DepartureDate date,
    DepartureTime time,
    Descriptions text,
	BookingStatus varchar(50) DEFAULT 'Pending' check (bookingstatus IN ('Confirmed', 'Not available', 'Pending')), -- Restriction on status
	ApprovalStatus VARCHAR(50) DEFAULT 'Pending' CHECK (approvalstatus IN ('Approved', 'Rejected','Pending', 'Cancelled')),
	foreign key (empid) references employees(empid)
)

select * from Admins
select * from Employees
select * from managers
select * from TravelAgents
select * from TravelRequest


 