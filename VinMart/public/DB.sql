
use master 
go
drop database VinMart
go


create database VinMart;
/*Tạo database*/
go
use VinMart;

go

/*Bản này là giống kiểu enum, là cố định các giá trị có thể có ví dụ như là chức vụ của account thì chỉ có admin và customer thôi*/
create table AccountPosition(
	id int identity primary key,
	displayName nvarchar(50) not null,
)
insert into AccountPosition (displayName) values (N'Admin')
insert into AccountPosition (displayName) values ( N'Customer')

/*Này là bản account*/
create table Account (
	id int identity primary key,
	username nvarchar(50) unique not null,
	password nvarchar(50) not null,
	displayName nvarchar(50) not null,
	position int foreign key references AccountPosition(id),/*Vị trí trỏ qua bên AccountPosition để độ định các giá trị có thể có trong chức vụ*/
	email nvarchar(50),
)

create table AccountDetail (/*Cái này là các thông tin khác ít sử dụng hơn của account*/
	id int primary key foreign key references Account(id),
	birthDay date check (birthDay <= GetDate()),
	phoneNumber varchar(13),
	gender nvarchar(10),/*Giới tính*/
	province nvarchar(20),/*Tỉnh*/
	district nvarchar(20),/*Huyện*/
	Commune nvarchar(20),/*Xã*/
	apartmentNumber nvarchar(20),/*Số nhà*/
	detail nvarchar(200),/*Cái này là 1 cái để muốn ghi chi tiết hay gì đó thì ghi*/
)

create table BigCategory(/*Ko biết cái này thì vỡ notion ra xem nha*/
	id int identity primary key,
	displayName nvarchar(100) not null,
	mediaURL nvarchar(100) not null,/*Đường dẩn của image*/
	slug nvarchar(100) not null,/*Cái này là ko biết nói sao nửa lên mạng xem nha, nó dùng trong đường dẩn á*/
	/*Thí dụ như là cái tên như ở dưới thì nó sẽ chuyển sang slug là */
	/*Slug là gì? Tổng hợp mọi điều cần biết về tối ưu Slug cho SEO*/
	/*slug-la-gi-tong-hop-moi-dieu-can-biet-ve-toi-uu-slug-cho-seo*/
	/*Và nó sẽ dùng như thế này để truy cập*/
	/*wiki.matbao.net/slug-la-gi-tong-hop-moi-dieu-can-biet-ve-toi-uu-slug-cho-seo*/

)

create table Category(/*Không biết thì xem trên notion nhá*/
	id int identity primary key,
	idBigCategory int foreign key references BigCategory(id),
	displayName nvarchar(100) not null,
	slug nvarchar(100) not null,
)

/*Thằng này như cái AccountPosition ở trên kéo lên trên nhá*/
create table ProductStatus(
	id int identity primary key,
	displayName nvarchar(50) not null,
)
insert into ProductStatus (displayName) values (N'Còn hàng')
insert into ProductStatus (displayName) values (N'Hết hàng')
insert into ProductStatus (displayName) values (N'Ngưng bán')
  

create table Product(
	id int identity primary key,
	idCategory int foreign key references Category(id),
	displayName nvarchar(100) not null,
	slug nvarchar(100) not null,/*Như cái category ở trên kéo lên trên xem nhá*/
	mediaURL nvarchar(100) not null,/*Như trên*/
	price float not null,/*Giá bán*/
	salePrice float not null,/*Giá sale*/
	/*Ví dụ như sản phẩm giá gốc là 140.000d và giá bán thực sự là 80.000d 
	thì salePrice là 140.000d còn price là 80.000d
	*/
	status int foreign key references ProductStatus(id),
)

create table ProductDetail(/*Giống như cái AccountDetail*/
	id int primary key foreign key references Product(id),
	ingredients nvarchar(200),/*Thành phần*/
	preserve nvarchar(200),/*Bảo quản*/
	origin nvarchar(200),/*Xuất xứ*/
	manuals nvarchar(200),/*Hướng dẩn sử dụng*/
)


create table CartDetail(/*Giỏ hàng*/
	idProduct int foreign key references Product(id),
	idUser int foreign key references Account(id),
	count int not null,
	primary key (idUser, idProduct),
)

create table Purchase(/*Các sản phẩm đã mua*/
	id int identity primary key,
	idUser int foreign key references Account(id),
	createAt date default GetDate(),
)

create table PurchaseDetail(/*Chi tiết sản phẩm đã mua*/
	id int identity primary key,
	idPurchase int foreign key references Purchase(id),
	price float not null,
	count int not null,
)


insert into Account (username, password, displayName, position, email) values ('vantiennn', '1', 'VanTiennn', 1, 'nguyenvantiennn0910@gmail.com')