create database QuanLyCSDLTaiNguyenRung 
go 

use QuanLyCSDLTaiNguyenRung 
go 

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

