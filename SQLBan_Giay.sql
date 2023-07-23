create database QLBan_Giay
GO
use QLBan_Giay
GO
Create Table The_Loai
(
	Ma_TheLoai int Identity(1,1),
	Ten_TheLoai nvarchar(50) NOT NULL,
	CONSTRAINT PK_The_Loai PRIMARY KEY(Ma_TheLoai)
)
GO

CREATE TABLE Giay
(
	Ma_Giay INT IDENTITY(1,1),
	Ten_Giay NVARCHAR(100) NOT NULL,
	DonViTinh NVARCHAR(50) DEFAULT 'Đôi',
	DonGia MONEY CHECK (DonGia>=0),
	MoTa NTEXT,
	HinhMinhHoa VARCHAR(50),
	Ma_TheLoai INT,
	Ma_NSX INT,
	NgayCapNhat DATETIME,
	SoLuongBan INT CHECK(SoLuongBan>0),
	Ma_Khuyen_Mai INT,
	CONSTRAINT PK_Xe PRIMARY KEY(Ma_Giay)
)
GO

CREATE TABLE Khuyen_Mai
(
	Ma_Khuyen_Mai INT IDENTITY(1,1),
	Ten_Khuyen_Mai NVARCHAR(100) NOT NULL,
	ThoiGianApDung Datetime,
	ThongTinKhuyenMai VARCHAR(200),
	CONSTRAINT PK_Khuyen_Mai PRIMARY KEY(Ma_Khuyen_Mai)
)
GO


CREATE TABLE Khach_Hang
(
	Ma_Khach_Hang INT IDENTITY(1,1),
	Ten_Khach_Hang nVarchar(50),
	DiaChi_Khach_Hang nVarchar(50),
	DienThoai_khach_Hang Varchar(10),
	TenDN_Khach_Hang Varchar(15),
	MatKhau_Khach_Hang Varchar(15),
	NgaySinh DATETIME,
	GioiTinh nvarchar(10),
	Email Varchar(50),
	CONSTRAINT PK_Khach_Hang PRIMARY KEY(Ma_Khach_Hang)
)
GO

CREATE TABLE Don_Dat_Hang
(
	So_Don_Hang INT IDENTITY(1,1),
	Ma_Khach_Hang INT,
	Ngay_Dat_Hang DATETIME,
	TriGia Money Check (TriGia>0),
	DaGiao nvarchar(10),
	NgayGiaoHang DATETIME,
	Ten_Khach_Hang Varchar(50),
	DiaChi_Khach_Hang nvarchar(50),
	DienThoai_khach_Hang Varchar(15),
	HinhThucThanhToan nvarchar(10),
	HinhThucGiaoHang nvarchar(10),
	CONSTRAINT PK_Don_Dat_Hang PRIMARY KEY(So_Don_Hang)
)
GO

CREATE TABLE CT_DatHang
(
	So_Don_Hang INT,
	Ma_Giay INT,
	SoLuong Int Check(SoLuong>0),
	DonGia Decimal(9,2) Check(DonGia>=0),
	ThanhTien AS SoLuong*DonGia ,
	CONSTRAINT PK_CT_DatHang PRIMARY KEY(So_Don_Hang, Ma_Giay)
)
GO



CREATE TABLE Gio_Hang
(
	Ma_Gio_Hang INT IDENTITY(1,1),
	Ma_Khach_Hang int NOT NULL,
	TenSanPham Nvarchar(100),
	SoLuongSanPham Varchar(20),
	CONSTRAINT PK_Gio_Hang PRIMARY KEY(Ma_Gio_Hang,Ma_Khach_Hang)
)
GO

CREATE TABLE Quan_Tri
(
	Ma_Admin INT IDENTITY(1,1),
	Ten_Admin nVarchar(50),
	DiaChi_Admin nVarchar(100),
	DienThoai_Admin int,
	TenDN_Admin Varchar(15),
	MatKhau_Admin Varchar(15),
	NgaySinh_Admin DATETIME,
	GioiTinh_Admin nvarchar(10),
	Email_Admin Varchar(50),
	CONSTRAINT PK_Quan_Tri PRIMARY KEY(Ma_Admin)
)
GO