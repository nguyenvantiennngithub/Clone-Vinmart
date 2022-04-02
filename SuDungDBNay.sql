use master
go
drop database VinMart
go
create database VinMart;
go
use VinMart
go


/*Tạo database*/
go
use VinMart

go

/*Bản này là giống kiểu enum, là cố định các giá trị có thể có ví dụ như là chức vụ của account thì chỉ có admin và customer thôi*/
create table AccountPosition(
	id int identity primary key,
	displayName nvarchar(50) not null,
)
insert into AccountPosition (displayName) values (N'Admin')
insert into AccountPosition (displayName) values ( N'Customer')
select * from AccountPosition
/*Này là bản account*/



create table AccountDetail (/*Cái này là các thông tin khác ít sử dụng hơn của account*/
	id int identity primary key,
	birthDay date check (birthDay <= GetDate()),
	phoneNumber varchar(13),
	gender nvarchar(10),/*Giới tính*/	
	province nvarchar(20),/*Tỉnh*/
	district nvarchar(20),/*Huyện*/
	Commune nvarchar(20),/*Xã*/
	apartmentNumber nvarchar(20),/*Số nhà*/
	detail nvarchar(200),/*Cái này là 1 cái để muốn ghi chi tiết hay gì đó thì ghi*/
)
insert into AccountDetail(phoneNumber, gender, province, district, Commune, apartmentNumber) 
values ('0963154685', 'Nam', 'TPHCM', 'Quan 12', 'Nguyen Dinh Chieu', '123/456')
create table Account (
	id int identity primary key,
	username nvarchar(50) not null,
	password nvarchar(50) not null,
	displayName nvarchar(50) not null,
	position int not null foreign key references AccountPosition(id),/*Vị trí trỏ qua bên AccountPosition để độ định các giá trị có thể có trong chức vụ*/
	email nvarchar(50),
	idAccountDetail int not null foreign key references AccountDetail(id),
)
go
insert into Account(username, password, displayName, position, email, idAccountDetail) values ('vantien', '1', 'VanTiennn', 1, 'nguyenvantiennn0910@gmail.com', 1)
go

create table Address(
	id int identity primary key,
	name nvarchar(50),
	phoneNumber varchar(13),
	email nvarchar(50),
	province nvarchar(20),
	district nvarchar(20),
	commune nvarchar(20),
	apartmentNumber nvarchar(20),
)




create table BigCategory(/*Ko biết cái này thì vỡ notion ra xem nha*/
	id int identity primary key,
	displayName nvarchar(100) not null,
	mediaURL nvarchar(100) not null,/*Đường dẩn của image*/
)

create table Category(/*Không biết thì xem trên notion nhá*/
	id int identity primary key,
	idBigCategory int foreign key references BigCategory(id),
	displayName nvarchar(100) not null,
)

/*Thằng này như cái AccountPosition ở trên kéo lên trên nhá*/
create table ProductStatus(
	id int identity primary key,
	displayName nvarchar(50) not null,
)
insert into ProductStatus (displayName) values (N'Còn hàng')
insert into ProductStatus (displayName) values (N'Hết hàng')
insert into ProductStatus (displayName) values (N'Ngưng bán')
  
  
create table ProductDetail(/*Giống như cái AccountDetail*/
	id int identity primary key,
	ingredients nvarchar(200),/*Thành phần*/
	preserve nvarchar(200),/*Bảo quản*/
	origin nvarchar(200),/*Xuất xứ*/
	manuals nvarchar(200),/*Hướng dẩn sử dụng*/
)
insert into ProductDetail values ('TP', 'BQ', 'XX', 'HDSD');
create table Product(
	id int identity primary key,
	idCategory int foreign key references Category(id),
	displayName nvarchar(100) not null,
	mediaURL nvarchar(200) not null,/*Như trên*/
	price float not null,/*Giá bán*/
	salePrice float not null,/*Giá sale*/
	unit nvarchar(50) not null,/*Đơn vị tính*/
	idProductDetail int foreign key references ProductDetail(id),
	status int foreign key references ProductStatus(id),
)


create table CartDetail(/*Giỏ hàng*/
	idProduct int foreign key references Product(id),
	idUser int foreign key references Account(id),
	count int not null,
	primary key (idUser, idProduct),
)

create table AddressUser(
	id int identity primary key,
	idUser int foreign key references Account(id),
	idAddress int foreign key references Address(id),
)

create table Purchase(/*Các sản phẩm đã mua*/
	id int identity primary key,
	idAddress int not null foreign key references Address(id),
	idUser int not null foreign key references Account(id),
	createAt datetime default GetDate(),
)

create table PurchaseDetail(/*Chi tiết sản phẩm đã mua*/
	id int identity primary key,
	idPurchase int not null foreign key references Purchase(id),
	idProduct int not null foreign key references Product(id),
	price float not null,
	count int not null,
)

insert into BigCategory (displayName, mediaURL) values (N'Thịt - Trứng - Hải Sản', N'/public/images/BigCategory/ThitTrungHaiSan.png')
insert into BigCategory (displayName, mediaURL) values (N'Rau - Củ - Trái Cây', N'/public/images/BigCategory/RauCuTraiCay.png')

insert into Category (displayName, idBigCategory) values (N'Rau Củ Quả', 2)
insert into Category (displayName, idBigCategory) values (N'Trái cây', 2)

insert into Category (displayName, idBigCategory) values (N'Thịt',  1)
insert into Category (displayName, idBigCategory) values (N'Trứng', 1)
insert into Category (displayName, idBigCategory) values (N'Hải Sản', 1)

select * from Category

insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Bưởi hồng da xanh túi lưới', 2, 46000, 50000, '1.2KG', 1, 1, '/public/images/Product/BuoiHong.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Dưa lưới sạch Đế Vương King size ruột cam ', 2, 59940, 59940, N'QUẢ', 1, 1, '/public/images/Product/DuaLuoi.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Dưa hấu Hắc Mỹ Nhân (Long An)', 2, 30000, 35000, '1.8KG', 1, 1, '/public/images/Product/DuaHau.jpg')

insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Chuối Vàng', 2, 46000, 50000, '0.6KG', 1, 1, '/public/images/Product/ChuoiVang.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Bắp cải trắng', 1, 17000, 17000, '0.6KG', 1, 1, '/public/images/Product/BapCai.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Rau muống WinEco', 1, 15000, 15000, '0.6KG', 1, 1, '/public/images/Product/RauMuong.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Cải chíp', 1, 9000, 9000, '0.6KG', 1, 1, '/public/images/Product/CaiChip.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Cải xanh', 1, 8000, 8000, '0.6KG', 1, 1, '/public/images/Product/CaiXanh.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Mướp hương', 1, 18000, 18000, '0.6KG', 1, 1, '/public/images/Product/MuopHuong.jpg')


insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Nộm sứa Tĩnh Gia Nhật Minh gói 350g', 5, 17000, 170001, '1KG', 1 ,1, '/public/images/Product/NomSua.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Chả cá chiên Quy Nhơn 200g', 5, 15000, 150001, '1KG' , 1, 1, '/public/images/Product/ChaCa.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Cá basa tươi 0.8kg up', 5, 9000, 9000, '1KG' , 1, 1, '/public/images/Product/CaBasa.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Cá basa fillet', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/CaBasaf.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Nghêu sạch Lenger khay 600g', 5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/NgheuSach.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Mực khô nhỏ An Vĩnh 200g', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/MucKho.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Mực ống gim An Vĩnh khay 200g', 5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/MucOng.png')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Thịt nghêu Lenger 200g', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/ThitNgheu.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Cá cơm sấy giòn ăn liền An Vĩnh hộp 100g', 5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/CaCom.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Chả cá thát lát tươi (nguyên chất)', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/ChaCaThatLat.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N' Mực xé tẩm sấy giòn ăn liền An Vĩnh gói 100g',  5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/MucXe.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Hàu sữa Nhật Minh túi 280g', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/HauSua.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Tôm viên Hoa Doanh gói 200g', 5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/TomVien.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Mực viên Hoa Doanh gói 200g', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/MucVien.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Chả cá hấp Quy Nhơn Hoa Doanh gói 200g', 5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/ChaCaHap.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Cá viên Hoa Doanh gói 200g', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/CaVienHD.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Cá viên biển Quy Nhơn Hoa Doanh gói 300g', 5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/CaVienQN.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Trứng gà Ba Huân hộp 10 quả', 5, 8000, 8000, '1KG' , 1, 1, '/public/images/Product/TrungGa10.jpg')
insert into Product (displayName, idCategory, price, salePrice, unit, status, idProductDetail, mediaURL) values (N'Trứng gà hộp 6 quả Ba Huân', 5, 18000, 18000, '1KG' , 1, 1, '/public/images/Product/TrungGa6.jpg')

use Vinmart

select * from BigCategory
select * from Category
select * from purchaseDetail
select * from Product
select * from AccountDetail
select * from CartDetail
select * from purchase
select * from address
select * from account

select * from AccountPosition


select * from accountDetail
