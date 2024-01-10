create database QuanLyCSDLTaiNguyenRung 
go 

use QuanLyCSDLTaiNguyenRung 
go 
--mục chưa cải thiện
create table Users  
( 
                 Id int identity(1,1) primary key, 

                 DisplayName nvarchar(max), 

                 UserName nvarchar(100), 

                 Password nvarchar(max), 
) 
go 



insert into Users (DisplayName, UserName, Password) values (N'Pham Thu Phuong', N'Phuong', N'8d6dac3a5bcbe726fcdb846b87761e4c') 

go 

insert into Users (DisplayName, UserName, Password) values (N'Nguyen Hoang Nam', N'Nam',N'678b7eee13e79e5946093840ce1542a0') 

go 

insert into Users (DisplayName, UserName, Password) values (N'Vu Song Tung',N'Tung',N'cc80e854898352557a4fbf24b1308ca0') 

go 

--Quan li CSDL cay giong 

 
create table SupplierTree-- cơ sở cung cấp cây giống 

( 

                 Id int identity(1,1) primary key, 

                 DisplayName nvarchar(max), 

                 Adress nvarchar(max) 
) 
go 
insert into SupplierTree(DisplayName, Adress) values (N'Thu Phương', N'Thái Bình') 

go 
insert into SupplierTree(DisplayName, Adress) values (N'Tiến Đạt', N'Nam Định') 

go 





 
create table Tree 

( 

                 Id int identity(1,1) primary key, 

                 DisplayName nvarchar(max), 

                 Count int, 

                 IdSupplierTree int null, 

                 foreign key (IdSupplierTree) references SupplierTree(Id), 
) 
go 

insert into Tree (DisplayName, Count, IdSupplierTree) values (N'Hạt lạc', N'50', 1) 

go 

insert into Tree (DisplayName, Count, IdSupplierTree) values (N'Hạt vừng', N'100',2)

go 



 
--Quan ly CSDL che bien go 

 
create table SupplierWood 

( 

                 Id int identity(1,1) primary key, 

                 DisplayName nvarchar(max), 

                 Adress nvarchar(max),
				 Typess nvarchar(max), -- loai hinh che bien go( thu cong/cong nghiep) 

				Form nvarchar (max),--hinh thuc che bien go (nho le/ doanh nghiep) 
) 

go 


create table Wood 

( 

                Id int identity(1,1) primary key, 

                DisplayName nvarchar(max), 

                Count int, 

                Unit nvarchar(50), 

				

				IdSupplierWood int null, 
				DateInput DateTime,

                foreign key (IdSupplierWood) references SupplierWood(Id), 
) 

go 


 
--quan ly CSDL tai nguyen sinh vat rung 

 
create table StorageAnimal -- co so luu tru dong vat 

( 

				Id int identity(1,1) primary key, 

				DisplayName nvarchar(max), 

				Adress nvarchar(max) 

) 

go 

 

create table Animal 

( 

				Id int identity(1,1) primary key, 

				DisplayName nvarchar(max), 

				Count int, 

				IdStorageAnimal int null, 
				StatusAnimal nvarchar(max), 
				DateInput DateTime,
				

                foreign key (IdStorageAnimal) references StorageAnimal(Id)

) 
go





--Mục muốn cải thiện
create table UserRole 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

) 

go 

insert into UserRole(DisplayName) values (N'CBNV1') 

insert into UserRole(DisplayName) values (N'CBNV2') 

go 

create table Userss 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

UserName nvarchar(100), 

Passwords nvarchar(max), 

IdUserRole int null, 

 

foreign key (IdUserRole) references UserRole(Id), 

) 

go 

 

insert into Userss (DisplayName, UserName, Passwords,IdUserRole) values (N'Pham Thu Phuong', N'Phuong', N'8d6dac3a5bcbe726fcdb846b87761e4c',1) 

go 

insert into Userss (DisplayName, UserName, Passwords,IdUserRole) values (N'Nguyen Hoang Nam', N'Nam',N'678b7eee13e79e5946093840ce1542a0',1) 

go 

insert into Userss (DisplayName, UserName, Passwords,IdUserRole) values (N'Nguyen Tien Dat',N'Dat',N'348f35ee00ef6ee1a7e79fb5eff51ea6',1) 

go 

select * from Userss 

select * from Userss where UserName='Nam' 

delete from Userss where UserName='Dat' 

 

create table Actions 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

DateInput DateTime, 

IdUser int null, 

 

foreign key (IdUser) references Userss(Id), 

) 

go 

insert into Actions(DisplayName,DateInput,IdUser) values (N'Đổi mật khẩu',CURRENT_TIMESTAMP,4) 

insert into Actions(DisplayName,DateInput,IdUser) values (N'Thêm người dùng',CURRENT_TIMESTAMP,5) 

select Userss.UserName, Userss.DisplayName, Actions.DisplayName, Actions.DateInput from Userss inner join Actions on Userss.UserName=Actions.IdUser where Userss.UserName='Nam' 

select * from Actions order by DateInput desc 

 

create table Groups  

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

) 

go 

insert into Groups(DisplayName) values (N'CBNV huyện') 

insert into Groups(DisplayName) values (N'CBNV xã') 

 

 

create table MemberGroup  

( 

Id int identity(1,1) primary key, 

IdUser int null, 

IdGroup int null, 

DateInput	DateTime, 

foreign key (IdUser) references Users(Id), 

foreign key (IdGroup) references Groups(Id), 

) 

go 

insert into MemberGroup(IdUser,IdGroup,DateInput) values (1,1,CURRENT_TIMESTAMP) 

insert into MemberGroup(IdUser,IdGroup,DateInput) values (2,1,CURRENT_TIMESTAMP) 

insert into MemberGroup(IdUser,IdGroup,DateInput) values (3,1,CURRENT_TIMESTAMP) 

delete from MemberGroup where IdUser=1 

select IdUser,IdGroup from MemberGroup where IdGroup=1 

--Quan li CSDL cay giong 

 

create table SupplierTrees-- cơ sở cung cấp cây giống 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

Adress nvarchar(max) 

) 

go 

insert into SupplierTrees(DisplayName,Adress) values (N'Nhà vườn An Khánh',N'Gia Lâm') 

insert into SupplierTrees(DisplayName,Adress) values (N'Trang trại cây giống Xuân Phượng',N'Long Biên') 

insert into SupplierTrees(DisplayName,Adress) values (N'Công ty giống cây trồng Hà Nội',N'Hà Đông') 

 

delete from SupplierTree where DisplayName='Nhà vườn An Khánh' 

update SupplierTree set DisplayName='Update' where DisplayName='Nhà vườn An Khánh' 

select DisplayName,Adress from SupplierTrees where DisplayName ='Nhà vườn An Khánh' 

create table Trees 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

Count int, 

IdSupplierTree int null, 

 

foreign key (IdSupplierTree) references SupplierTrees(Id), 

) 

go 

insert into Trees(DisplayName,Count,IdSupplierTree) values (N'Bưởi',210,1) 

insert into Trees(DisplayName,Count,IdSupplierTree) values (N'Cam',190,3) 

insert into Trees(DisplayName,Count,IdSupplierTree) values (N'Táo',400,2) 

delete from Trees where DisplayName='Bưởi' 

update Trees set IdSupplierTree=2 where DisplayName='Bưởi' 

select * from Trees where DisplayName='Bưởi' 

select * from Trees where IdSupplierTree=1 

--Quan ly CSDL che bien go 

 

create table Types  

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

) 

go 

insert into Types(DisplayName) values (N'thủ công') 

insert into Types(DisplayName) values (N'công nghiệp') 

delete from Types when DisplayName='thủ công' 

 

create table Form  

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

) 

go 

insert into Form(DisplayName) values (N'tư nhân') 

insert into Form(DisplayName) values (N'doanh nghiệp') 

delete from Form when DisplayName='doanh nghiệp' 

 

create table SupplierWoods 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

Adress nvarchar(max), 

DateInput DateTime, 

IdType int null, 

IdForm int null, 

foreign key (IdType) references Types(Id), 

foreign key (IdForm) references Form(Id), 

) 

go 

insert into SupplierWoods(DisplayName,Adress,DateInput,IdType,IdForm) values (N'xưởng gỗ Xline',N'Gia Lâm',CURRENT_TIMESTAMP,1,1) 

insert into SupplierWoods(DisplayName,Adress,DateInput,IdType,IdForm) values (N'xưởng mộc Kiến Hưng',N'Hai Bà Trưng',CURRENT_TIMESTAMP,1,1) 

delete from SupplierWoods when DisplayName='xưởng gỗ Xline' 

update SupplierWoods set Adress='update',IdForm=2,IdType=2 where DisplayName='xưởng gỗ Xline' 

select * from SupplierWoods where DisplayName='xưởng mộc Xline' 

select * from SupplierWoods where IdType=1
select * from SupplierWoods where IdForm=1

create table Woods 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

Count int, 

Unit nvarchar(50), 

 

IdSupplierWood int null, 

 

foreign key (IdSupplierWood) references SupplierWoods(Id), 

) 

go 
insert into Woods(DisplayName,Count,Unit,IdSupplierWood) values (N'xoan đào',20,N'tấn',1)
delete from Woods where DisplayName='xoan đào'
update Woods set IdSupplierWood=2 where DisplayName='xoan đào'
select * from Woods where DisplayName='xoan đào'
select * from Woods where IdSupplierWood=1

 

--quan ly CSDL tai nguyen sinh vat rung 

 

create table StatusAnimals  

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

) 

go 

insert into StatusAnimals(DisplayName) values(N'ổn định') 

delete from StatusAnimals where DisplayName='ổn định' 

 

create table ChangeAnimal 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

) 

go 

insert into ChangeAnimal (DisplayName) values (N'tăng') 

delete from ChangeAnimal where DisplayName='tăng' 

 

create table StorageAnimals 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

Adress nvarchar(max), 

DateInput DateTime, 

) 

go 

insert into StorageAnimals(DisplayName,Adress,DateInput) values (N'vườn quốc gia','Bắc Kan',CURRENT_TIMESTAMP) 

delete from StorageAnimals where DisplayName='vườn quốc gia' 

update StorageAnimals set Adress='update' where DisplayName='vườn quốc gia' 

select*from StatusAnimals where DisplayName='vườn quốc gia' 

create table Animals 

( 

Id int identity(1,1) primary key, 

DisplayName nvarchar(max), 

Count int, 

IdStorageAnimal int null, 

IdStatusAnimal  int null, 

IdChangeAnimal int null, 

DateInput DateTime, 

foreign key (IdStatusAnimal) references StatusAnimals(Id), 

foreign key (IdStorageAnimal) references StorageAnimals(Id), 

foreign key (IdChangeAnimal) references ChangeAnimal(Id), 

) 

go 

insert into Animals(DisplayName,Count,IdStorageAnimal,IdStatusAnimal,IdChangeAnimal,DateInput) values (N'gấu',13,1,1,1,CURRENT_TIMESTAMP) 

insert into Animals(DisplayName,Count,IdStorageAnimal,IdStatusAnimal,IdChangeAnimal,DateInput) values (N'hổ',10,1,1,1,CURRENT_TIMESTAMP) 

delete from Animals where DisplayName='gấu' 

update Animals set IdStorageAnimal=2 where DisplayName='gấu' 

select * from Animals where DisplayName='gấu' 

select * from Animals where IdStorageAnimal=1 

select * from Animals where IdChangeAnimal=1 

select * from Animals where IdStatusAnimal=1 


