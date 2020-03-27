create table LeaveType(
Id int primary key Identity(1,1),
Name varchar(50),
Count int,
CreateDate datetime default getdate(),
UpdateDate datetime,
CreatedBy varchar(50),
UpdatedBy varchar(50),
IsDeleted bit
                     
)


create table LeaveRequest(
Id int primary key Identity(1,1),
FromDate date,
ToDate date,
DayBegin varchar(15),
DayEnd varchar(15),
LeaveTypeId int foreign key references LeaveType(Id),
ShortLeaveFrom int,
ShortLeaveTo int,
Reason varchar(100),
RoleId nvarchar(450) foreign key references AspNetRoles(Id),
CreateDate datetime default getdate(),
UpdateDate datetime,
CreatedBy varchar(50),
UpdatedBy varchar(50),
IsDeleted bit
                     
)