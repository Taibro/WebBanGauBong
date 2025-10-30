--CREATE DATABASE QL_THU_BONG
--USE QL_THU_BONG
--drop database QL_THU_BONG
--DROP TABLE Category
--DROP TABLE Product
--DROP TABLE ProductSize
--DROP TABLE ProductImages
CREATE TABLE Category
(
	CategoryID char(10),
	CategoryName nvarchar(50),
	Primary key(CategoryID)
)

CREATE TABLE Product
(
	ProductID char(10),
	CategoryID char(10),
	ProductName nvarchar(100),
	Primary key(ProductID),
	Foreign key(CategoryID) references Category(CategoryID)
)

CREATE TABLE ProductSize
(
	ProductSizeID char(10),
	ProductID char(10),
	SizeName int,
	Price decimal,
	StockQuantity int,
	Primary key(ProductSizeID),
	Foreign key(ProductID) references Product(ProductID)
)


CREATE TABLE ProductImages
(
	ProductImageID char(10),
	ProductID char(10),
	ImageURL varchar(500),
	primary key(ProductImageID),
	foreign key(ProductID) references Product(ProductID)
)

CREATE TABLE Discount
(
	DiscountID char(10),
	ProductID char(10),
	DiscountName nvarchar(50),
	StartDate date, 
	EndDate date,
	DiscountRate float,
	primary key(DiscountID),
	foreign key(ProductID) references Product(ProductID)
)

CREATE TABLE Roles
(
	RoleID char(10),
	RoleName nvarchar(50),
	primary key(RoleID),
)

CREATE TABLE Users
(
	UserID char(10),
	RoleID char(10),
	Name nvarchar(50),
	Email nvarchar(50),
	Password varchar(255),
	primary key(UserID),
	foreign key(RoleID) references Roles(RoleID)
)

CREATE TABLE Rating
(
	RatingID char(10),
	ProductID char(10),
	UserID char(10),
	Star float,
	Comment nvarchar(500),
	primary key(RatingID),
	foreign key(ProductID) references Product(ProductID),
	foreign key(UserID) references Users(UserID)
)

CREATE TABLE Orders
(
	OrderID char(10),
	UserID char(10),
	OrderDate datetime,
	Address nvarchar(255),
	Status nvarchar(50),
	UserPaymentMethod nvarchar(50),
	primary key(OrderID),
	foreign key(UserID) references Users(UserID)
)

CREATE TABLE OrderDetail
(
	OrderID char(10),
	ProductSizeID char(10),
	Quantity int,
	UnitPrice decimal,
	primary key(OrderID, ProductSizeID),
	foreign key(ProductSizeID) references ProductSize(ProductSizeID),
	foreign key(OrderID) references Orders(OrderID)
)

CREATE TABLE ShoppingCart
(
	ShoppingCartID char(10),
	UserID char(10),
	primary key(ShoppingCartID),
	foreign key(UserID) references Users(UserID)
)

CREATE TABLE ShoppingCartItem
(
	ShoppingCartID char(10),
	ProductSizeID char(10),
	Quantity int,
	primary key(ShoppingCartID, ProductSizeID),
	foreign key(ProductSizeID) references ProductSize(ProductSizeID),
	foreign key(ShoppingCartID) references ShoppingCart(ShoppingCartID)
)


INSERT INTO Category
values
('L001', N'Thú Bông'),
('L002', N'Gấu Bông Hoạt Hình'),
('L003', N'Gối ôm'),
('L004', N'Gấu Bông'),
('L005', N'Gấu Bông Có Mền 2IN1'),
('L006', N'Gấu Bông Size Nhỏ'),
('L007', N'Gấu Bông Đẹp'),
('L008', N'Gấu Bông Giá Rẻ'),
('L009', N'Gấu Bông Teddy'),
('L010', N'Gấu Bông Dài'),
('L011', N'Heo Bông'),
('L012', N'Thỏ Bông'),
('L013', N'Hoa gấu bông')

INSERT INTO Product
values
('SP0001', 'L002', N'Gấu Bông Stitch Hồng lông Smooth'),
('SP0002', 'L002', N'Gấu Bông Stitch Xanh cosplay Monster'),
('SP0003', 'L002', N'Gấu Bông Stitch nằm lông mịn Smooth'),
('SP0004', 'L002', N'Gấu Bông Stitch Xanh Mặc Yếm Jeans'),

('SP0005', 'L013', N'Gấu Bông Hoa Hồng Khổng Lồ có ngăn Giấu Quà'),
('SP0006', 'L013', N'Hoa Gấu Bông Lotso'),
('SP0007', 'L013', N'Hoa Gấu Bông Lena'),
('SP0008', 'L013', N'Gấu Bông Hoa Hồng Khổng Lồ'),

('SP0009', 'L001', N'Vịt Bông cosplay Súp Lơ xanh lông mịn'),
('SP0010', 'L001', N'Gấu Bông Hươu Cao Cổ lông Smooth siêu mịn'),
('SP0011', 'L001', N'Gấu Bông Capybara mũm mĩm ôm Vịt'),
('SP0012', 'L001', N'Gấu Bông Bánh Mì Capybara baby'),

('SP0013', 'L002', N'Gấu Bông SHIN nằm mặc đồ ngủ'),
('SP0014', 'L002', N'Gấu Bông Baby Three màu Galaxy'),

('SP0015', 'L005', N'Mèo Bông Mặt Quạo có mền 2in1'),
('SP0016', 'L005', N'Gấu Bông Capybara đội nón Sinh Nhật có mền 2in1'),
('SP0017', 'L005', N'Vịt Bông Vàng có mền 2in1'),

('SP0018', 'L009', N'Gấu Teddy mặc áo len Love'),
('SP0020', 'L009', N'Gấu Teddy heads and tales'),
('SP0019', 'L009', N'Gấu bông Teddy áo len Choco'),
('SP0021', 'L009', N'Gấu Bông Tốt Nghiệp – Gấu Teddy tốt nghiệp lông xù màu Nâu'),
('SP0022', 'L009', N'Gấu Bông Tốt Nghiệp – Gấu Teddy tốt nghiệp lông xù màu Vàng'),

('SP0023', 'L010', N'Mèo Hoàng Thượng Đội Lốt Thú Gối Ôm Dài'),
('SP0024', 'L010', N'Chó Bông Shiba lông Smooth Gối Ôm Dài'),
('SP0025', 'L010', N'Gấu Bông Lotso Nằm lông Smooth')

INSERT INTO ProductSize
VALUES
('KT0001', 'SP0001', 45, 215000, 10),
('KT0002', 'SP0002', 95, 895000, 10),
('KT0003', 'SP0003', 30, 150000, 10),
('KT0004', 'SP0003', 50, 290000, 10),
('KT0005', 'SP0003', 60, 420000, 10),
('KT0006', 'SP0003', 80, 580000, 10),
('KT0007', 'SP0004', 35, 240000, 10),
('KT0008', 'SP0004', 45, 340000, 10),
('KT0009', 'SP0004', 55, 450000, 10),
('KT0010', 'SP0004', 70, 670000, 10),

('KT0011', 'SP0005', 40, 290000, 10),
('KT0012', 'SP0006', 40, 185000, 10),
('KT0013', 'SP0007', 30, 145000, 10),
('KT0014', 'SP0008', 40, 205000, 10),
('KT0015', 'SP0008', 70, 395000, 10),

('KT0016', 'SP0009', 60, 220000, 10),
('KT0017', 'SP0009', 85, 360000, 10),
('KT0018', 'SP0009', 150, 450000, 10),
('KT0019', 'SP0010', 45, 205000, 10),
('KT0020', 'SP0010', 65, 315000, 10),
('KT0021', 'SP0010', 80, 455000, 10),
('KT0022', 'SP0010', 120, 725000, 10),
('KT0023', 'SP0011', 35, 185000, 10),
('KT0024', 'SP0011', 45, 245000, 10),
('KT0025', 'SP0011', 55, 365000, 10),
('KT0026', 'SP0012', 20, 95000, 10),
('KT0027', 'SP0012', 70, 325000, 10),

('KT0028', 'SP0013', 55, 280000, 10),
('KT0029', 'SP0013', 75, 420000, 10),
('KT0030', 'SP0014', 30, 175000, 10),
('KT0031', 'SP0014', 60, 364000, 10),

('KT0032', 'SP0015', 45, 350000, 10),
('KT0033', 'SP0016', 45, 375000, 10),
('KT0034', 'SP0017', 40, 330000, 10),

('KT0035', 'SP0018', 55, 245000, 10),
('KT0036', 'SP0019', 80, 244000, 10),
('KT0037', 'SP0019', 160, 1251000, 10),
('KT0038', 'SP0019', 120, 1712000, 10),
('KT0039', 'SP0020', 40, 145000, 10),
('KT0040', 'SP0020', 50, 240000, 10),
('KT0041', 'SP0020', 75, 440000, 10),
('KT0042', 'SP0021', 40, 220000, 10),
('KT0054', 'SP0021', 50, 315000, 10),
('KT0043', 'SP0022', 40, 220000, 10),
('KT0044', 'SP0022', 50, 315000, 10),

('KT0045', 'SP0023', 80, 350000, 10),
('KT0046', 'SP0023', 100, 480000, 10),
('KT0047', 'SP0023', 125, 670000, 10),
('KT0048', 'SP0024', 60, 250000, 10),
('KT0049', 'SP0024', 80, 370000, 10),
('KT0050', 'SP0024', 110, 580000, 10),
('KT0051', 'SP0025', 110, 715000, 10),
('KT0052', 'SP0025', 60, 285000, 10),
('KT0053', 'SP0025', 80, 405000, 10)

INSERT INTO ProductImages
values
('H0001', 'SP0001', 'gau-bong-stitch-hong-1-768x768.jpg'),
('H0002', 'SP0002', 'gau-bong-stitch-cosplay-monster-5.jpg'),
('H0003', 'SP0002', 'gau-bong-stitch-cosplay-monster-4.jpg'),
('H0004', 'SP0002', 'gau-bong-stitch-cosplay-monster-3.jpg'),
('H0005', 'SP0002', 'gau-bong-stitch-cosplay-monster-2.jpg'),
('H0006', 'SP0002', 'gau-bong-stitch-cosplay-monster-1.jpg'),
('H0007', 'SP0003', 'gau-bong-stitch-nam-long-min-smooth-tim-1.jpg'),
('H0008', 'SP0003', 'gau-bong-stitch-nam-long-min-smooth-tim-2.jpg'),
('H0009', 'SP0003', 'gau-bong-stitch-nam-long-min-smooth-tim-3.jpg'),
('H0010', 'SP0003', 'gau-bong-stitch-nam-long-min-smooth-tim-4-768x768.jpg'),
('H0011', 'SP0004', 'gau-bong-stitch-mac-yem-xanh-1.jpg'),

('H0012', 'SP0005', 'gau-bong-hoa-hong-ngan-dung-qua-2.jpg'),
('H0013', 'SP0005', 'gau-bong-hoa-hong-ngan-dung-qua-1.jpg'),
('H0014', 'SP0006', 'hoa-gau-bong-lotso.jpg'),
('H0015', 'SP0006', 'hoa-gau-bong-lotso-dau-2.jpg'),
('H0016', 'SP0006', 'hoa-gau-bong-lotso-dau-1.jpg'),
('H0017', 'SP0007', 'hoa-gau-bong-lena.jpg'),
('H0018', 'SP0007', 'hoa-bong-gau-lena-mau-dam-1.jpg'),
('H0019', 'SP0008', 'gau-bong-hoa-hong-khong-lo-8.jpg'),
('H0020', 'SP0008', 'gau-bong-hoa-hong-khong-lo-7.jpg'),
('H0021', 'SP0008', 'gau-bong-hoa-hong-khong-lo-3-768x768.jpg'),

('H0022', 'SP0009', 'vit-bong-cosplay-bap-cai-xanh-1.jpg'),
('H0023', 'SP0009', 'vit-bong-cosplay-bap-cai-xanh-2.jpg'),
('H0024', 'SP0010', 'gau-bong-huou-cao-co-2.jpg'),
('H0025', 'SP0010', 'gau-bong-huou-cao-co-1.jpg'),
('H0026', 'SP0010', 'gau-bong-huou-cao-co-3.jpg'),
('H0027', 'SP0011', 'gau-bong-capybara-om-vit-vang-1.jpg'),
('H0028', 'SP0011', 'gau-bong-capybara-om-vit-vang-4.jpg'),
('H0029', 'SP0011', 'gau-bong-capybara-om-vit-vang-3.jpg'),
('H0030', 'SP0012', 'gau-bong-capybara-banh-mi-goi-om-dai-2.jpg'),
('H0031', 'SP0012', 'gau-bong-capybara-banh-mi-goi-om-dai-1.jpg'),
('H0032', 'SP0012', 'gau-bong-banh-mi-capybara-mini-1.jpg'),

('H0033', 'SP0013', 'gau-bong-shin-mac-do-ngu-form-dai-3.jpg'),
('H0034', 'SP0013', 'gau-bong-shin-mac-do-ngu-form-dai-2.jpg'),
('H0035', 'SP0013', 'gau-bong-shin-mac-do-ngu-form-dai-1.jpg'),
('H0036', 'SP0014', 'gaubong-baby-three-galaxy-3-1-768x768.jpg'),
('H0037', 'SP0014', 'gaubong-baby-three-galaxy-1-1-768x768.jpg'),
('H0038', 'SP0014', 'gaubong-baby-three-galaxy-2-768x768.jpg'),

('H0039', 'SP0015', 'goi-men-meo-bong-mat-quao-1.jpg'),
('H0040', 'SP0016', 'gau-bong-capybara-happy-birthday-co-men-2in1-1.jpg'),
('H0041', 'SP0017', 'goi-men-vit-bong-vang-1.jpg'),

('H0042', 'SP0018', 'gau-teddy-mac-ao-len-love-1.jpg'),
('H0043', 'SP0019', 'gaubong-teddy-ao-len-choco-1m2.jpg'),
('H0044', 'SP0019', 'gau-teddy-ao-len-choco-2022.jpg'),
('H0045', 'SP0019', 'gau-teddy-choco-2m.jpg'),
('H0046', 'SP0020', 'gau-teddy-heads-and-tales.jpg'),
('H0047', 'SP0020', 'gau-bong-teddy-head-and-tales.jpg'),
('H0048', 'SP0020', 'gau-teddy-head-and-tales-nau.jpg'),
('H0049', 'SP0021', 'gaubong-gau-teddy-tot-nghiep-nau-1.jpg'),
('H0050', 'SP0022', 'gaubong-teddy-tot-nghiep-2.jpg'),

('H0051', 'SP0023', 'meo-bong-hoang-thuong-cosplay-tho-goi-om-dai-3.jpg'),
('H0052', 'SP0023', 'meo-bong-hoang-thuong-cosplay-tho-goi-om-dai-2.jpg'),
('H0053', 'SP0023', 'meo-bong-hoang-thuong-cosplay-panda-goi-om-dai-768x768.jpg'),
('H0054', 'SP0024', 'cho-bong-shiba-smooth-goi-om-dai-3.jpg'),
('H0055', 'SP0024', 'cho-bong-shiba-smooth-goi-om-dai-2.jpg'),
('H0056', 'SP0024', 'cho-bong-shiba-smooth-goi-om-dai-1.jpg'),
('H0057', 'SP0025', 'gaubong-lotso-goi-om-dai-2.jpg'),
('H0058', 'SP0025', 'gaubong-lotso-goi-om-dai-1.jpg'),
('H0059', 'SP0025', 'gau-bong-lotso-mau-do-nam-long-smooth-8.jpg')

SELECT * FROM Product