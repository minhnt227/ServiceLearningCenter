USE [master]
GO
/****** Object:  Database [Service_Learning]    Script Date: 3/1/2024 12:34:49 PM ******/
CREATE DATABASE [Service_Learning]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Service_Learning', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Service_Learning.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Service_Learning_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Service_Learning_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Service_Learning].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Service_Learning] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Service_Learning] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Service_Learning] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Service_Learning] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Service_Learning] SET ARITHABORT OFF 
GO
ALTER DATABASE [Service_Learning] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Service_Learning] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Service_Learning] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Service_Learning] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Service_Learning] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Service_Learning] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Service_Learning] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Service_Learning] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Service_Learning] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Service_Learning] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Service_Learning] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Service_Learning] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Service_Learning] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Service_Learning] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Service_Learning] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Service_Learning] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Service_Learning] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Service_Learning] SET RECOVERY FULL 
GO
ALTER DATABASE [Service_Learning] SET  MULTI_USER 
GO
ALTER DATABASE [Service_Learning] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Service_Learning] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Service_Learning] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Service_Learning] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Service_Learning] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Service_Learning', N'ON'
GO
ALTER DATABASE [Service_Learning] SET QUERY_STORE = ON
GO
ALTER DATABASE [Service_Learning] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Service_Learning]
GO
/****** Object:  Table [dbo].[DANH_GIA]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DANH_GIA](
	[MaDanhGia] [nchar](10) NOT NULL,
	[MaHD] [int] NULL,
	[MSSV] [nchar](10) NULL,
	[NoiDung] [ntext] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_DANH_GIA_1] PRIMARY KEY CLUSTERED 
(
	[MaDanhGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DANHGIA_DETAILS]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DANHGIA_DETAILS](
	[MaDanhGia] [nchar](10) NOT NULL,
	[HangMuc] [int] NOT NULL,
	[MucDo] [int] NULL,
	[GhiChu] [ntext] NULL,
 CONSTRAINT [PK_DANHGIA_DETAILS] PRIMARY KEY CLUSTERED 
(
	[MaDanhGia] ASC,
	[HangMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DOI_TAC]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOI_TAC](
	[ID_DoiTac] [int] IDENTITY(1,1) NOT NULL,
	[TenDoiTac] [nvarchar](100) NULL,
	[DaiDien] [nvarchar](50) NULL,
	[SDT] [nchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_DOI_TAC] PRIMARY KEY CLUSTERED 
(
	[ID_DoiTac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GIANG_VIEN]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GIANG_VIEN](
	[MaGV] [varchar](50) NOT NULL,
	[HoTenLot] [nvarchar](50) NULL,
	[Ten] [nvarchar](50) NULL,
	[Khoa] [varchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_GIANG_VIEN] PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HANG_MUC]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HANG_MUC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenHangMuc] [ntext] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_HANG_MUC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_DOITAC]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_DOITAC](
	[ID_DoiTac] [int] NOT NULL,
	[MaHD] [int] NOT NULL,
	[NoiDung] [ntext] NULL,
 CONSTRAINT [PK_DT_HOATDONG] PRIMARY KEY CLUSTERED 
(
	[ID_DoiTac] ASC,
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_GIANGVIEN]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_GIANGVIEN](
	[MaHD] [int] NOT NULL,
	[MaGV] [varchar](50) NOT NULL,
	[VaiTro] [nvarchar](50) NULL,
 CONSTRAINT [PK_HD_GIANGVIEN] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MaGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_SINHVIEN]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_SINHVIEN](
	[MaHD] [int] NOT NULL,
	[MSSV] [nchar](10) NOT NULL,
	[VaiTro] [nvarchar](50) NULL,
	[GhiChu] [ntext] NULL,
 CONSTRAINT [PK_SV_HOATDONG] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MSSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HD_TAITRO]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HD_TAITRO](
	[ID_TaiTro] [int] NOT NULL,
	[MaHD] [int] NOT NULL,
	[NoiDung] [ntext] NULL,
 CONSTRAINT [PK_HD_TAITRO] PRIMARY KEY CLUSTERED 
(
	[ID_TaiTro] ASC,
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOAT_DONG]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOAT_DONG](
	[MaHD] [int] IDENTITY(1,1) NOT NULL,
	[TenHoatDong] [ntext] NULL,
	[Loai] [nvarchar](50) NULL,
	[NgayBatDau] [datetime] NULL,
	[NgayKetThuc] [datetime] NULL,
	[Hide] [bit] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_HOAT_DONG] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHOA]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHOA](
	[MaKhoa] [varchar](50) NOT NULL,
	[TenKhoa] [nvarchar](50) NULL,
	[SDT] [nchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[NgayThanhLap] [date] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_KHOA] PRIMARY KEY CLUSTERED 
(
	[MaKhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHU_TRACH]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHU_TRACH](
	[MaGV] [varchar](50) NOT NULL,
	[NamHoc] [nvarchar](50) NOT NULL,
	[GhiChu] [ntext] NULL,
 CONSTRAINT [PK_PHU_TRACH_1] PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC,
	[NamHoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SINH_VIEN]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SINH_VIEN](
	[MSSV] [nchar](10) NOT NULL,
	[HoTen] [nvarchar](100) NULL,
	[Khoa] [varchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_SINH_VIEN] PRIMARY KEY CLUSTERED 
(
	[MSSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAI_CHINH]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAI_CHINH](
	[ID_TaiChinh] [int] IDENTITY(1,1) NOT NULL,
	[MaHD] [int] NULL,
	[UEF] [decimal](12, 0) NULL,
	[TaiTro] [decimal](12, 0) NULL,
	[Khac] [ntext] NULL,
	[TieuDe] [ntext] NULL,
	[CreatedDate] [datetime] NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_TAI_CHINH] PRIMARY KEY CLUSTERED 
(
	[ID_TaiChinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAI_TRO]    Script Date: 3/1/2024 12:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAI_TRO](
	[ID_TaiTro] [int] IDENTITY(1,1) NOT NULL,
	[TenTaiTro] [nvarchar](100) NULL,
	[DaiDien] [nvarchar](50) NULL,
	[SDT] [nchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_TAI_TRO] PRIMARY KEY CLUSTERED 
(
	[ID_TaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DOI_TAC] ON 

INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (7, N'Cty TNHH Nhat Minh', N'Nguyen Hai B', N'0935455254          ', N'NhatMinhCorp@gmail.com', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (8, N'Truong THPT Le Quy Don', N'Do Anh', N'0285355647          ', N'lqdHigh@uef.edu.vn', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (9, N'Đại Học Văn Lang', N'Th.S Nguyễn Trung Lang', N'028665417           ', N'VLU@gmail.com', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (10, N'Hợp tác xã Vô Tiền', N'Nguyễn Thanh Phương', N'025541265           ', N'VT@Bussiness.mail.com', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (11, N'Tập Đoàn Dầu Khí Miền Nam', N'Nguyễn Thành Long', N'028665425           ', N'DKMN@spf.com', 0)
INSERT [dbo].[DOI_TAC] ([ID_DoiTac], [TenDoiTac], [DaiDien], [SDT], [Email], [Hide]) VALUES (15, N'Công Ty THHH Nhật Minh', N'Phạm Nhật Minh', N'0944124154          ', N'Nhatminh.co@gmail.com', 0)
SET IDENTITY_INSERT [dbo].[DOI_TAC] OFF
GO
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'111111    ', N'Nguyen The', N'Minh', N'UEF001    ', 0)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'123123    ', N'Pham Nhật', N'Minh', N'UEF001    ', NULL)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'123456    ', N'Nguyen Hai', N'Quynh', N'UEF002    ', NULL)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'22333     ', N'ádfsd', N'ầ', N'UEF001    ', NULL)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'32164     ', N'Nguyen Thanh', N'Van', N'UEF003    ', NULL)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'321654    ', N'Nguyen Thanh', N'Phuong', N'UEF001    ', 0)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'3234234   ', N'sdfasdf', N'sd', N'UEF001    ', NULL)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'654321    ', N'Nguyen Van', N'Hai', N'UEF003    ', 0)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'987654    ', N'jnjnjn', N'nm', N'UEF002    ', NULL)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'dfsdfsdf  ', N'ádfsd', N'ầ', N'UEF001    ', NULL)
INSERT [dbo].[GIANG_VIEN] ([MaGV], [HoTenLot], [Ten], [Khoa], [Hide]) VALUES (N'NGUYENTHEMINH220719985', N'NGUYEN THẾ', N'MINH', N'UEF005    ', 0)
GO
SET IDENTITY_INSERT [dbo].[HANG_MUC] ON 

INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (1, N'hang muc A', 0)
INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (2, N'hang muc B', 0)
INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (3, N'hang muc C', 0)
INSERT [dbo].[HANG_MUC] ([ID], [TenHangMuc], [Hide]) VALUES (4, N'Hang Muc D', 0)
SET IDENTITY_INSERT [dbo].[HANG_MUC] OFF
GO
INSERT [dbo].[HD_DOITAC] ([ID_DoiTac], [MaHD], [NoiDung]) VALUES (15, 1, N'')
GO
INSERT [dbo].[HD_GIANGVIEN] ([MaHD], [MaGV], [VaiTro]) VALUES (1, N'111111    ', N'Tổ chức')
INSERT [dbo].[HD_GIANGVIEN] ([MaHD], [MaGV], [VaiTro]) VALUES (1, N'NGUYENTHEMINH220719985', N'Tham gia')
GO
INSERT [dbo].[HD_SINHVIEN] ([MaHD], [MSSV], [VaiTro], [GhiChu]) VALUES (1, N'111111    ', N'Tham gia', N'')
INSERT [dbo].[HD_SINHVIEN] ([MaHD], [MSSV], [VaiTro], [GhiChu]) VALUES (1, N'123345    ', N'Tổ chức', N'')
GO
SET IDENTITY_INSERT [dbo].[HOAT_DONG] ON 

INSERT [dbo].[HOAT_DONG] ([MaHD], [TenHoatDong], [Loai], [NgayBatDau], [NgayKetThuc], [Hide], [CreatedDate]) VALUES (1, N'form test add', N'Sự kiện', CAST(N'2024-01-01T01:30:50.000' AS DateTime), CAST(N'2024-01-26T01:30:50.623' AS DateTime), 1, CAST(N'2024-02-29T18:25:44.213' AS DateTime))
SET IDENTITY_INSERT [dbo].[HOAT_DONG] OFF
GO
INSERT [dbo].[KHOA] ([MaKhoa], [TenKhoa], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'          ', N'', N'                    ', N'', CAST(N'2023-12-09' AS Date), 1)
INSERT [dbo].[KHOA] ([MaKhoa], [TenKhoa], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'UEF001    ', N'Công Nghệ Thông Tin', N'0911222333          ', N'cntt@uef.edu.vn', CAST(N'2005-09-22' AS Date), 0)
INSERT [dbo].[KHOA] ([MaKhoa], [TenKhoa], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'UEF002    ', N'Khoa Kinh Tế', N'0284222666          ', N'kt@uef.edu.vn', CAST(N'2000-06-14' AS Date), 0)
INSERT [dbo].[KHOA] ([MaKhoa], [TenKhoa], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'UEF003    ', N'Khoa Quản Trị Kinh Doanh', N'0283534427          ', N'qtkt@uef.edu.vn', CAST(N'2006-01-25' AS Date), 0)
INSERT [dbo].[KHOA] ([MaKhoa], [TenKhoa], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'UEF004    ', N'Khoa Luật', N'0952648445          ', N'luatuef@uef.edu.vn', CAST(N'2016-01-05' AS Date), 0)
INSERT [dbo].[KHOA] ([MaKhoa], [TenKhoa], [SDT], [Email], [NgayThanhLap], [Hide]) VALUES (N'UEF005    ', N'Viện Quốc Tế', N'0254487544          ', N'vqt@Gmail.com', CAST(N'2023-12-09' AS Date), 0)
GO
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'111111    ', N'Nguyen Van C', N'UEF001    ', 0)
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'123345    ', N'Nguyen Nguyen Nguyen', N'UEF002    ', 0)
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'123456    ', N'Nguyen The Minh', N'UEF003    ', 0)
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'222222    ', N'Nguyen Van C', N'UEF002    ', 0)
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'321654    ', N'Vu THanh Van', N'UEF003    ', 0)
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'333333    ', N'Nguyen Thi Vam Co Dong', N'UEF003    ', 0)
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'654321    ', N'Nguyen Thi D', N'UEF001    ', 0)
INSERT [dbo].[SINH_VIEN] ([MSSV], [HoTen], [Khoa], [Hide]) VALUES (N'987654    ', N'Dang Thi Mai', N'UEF001    ', 0)
GO
SET IDENTITY_INSERT [dbo].[TAI_CHINH] ON 

INSERT [dbo].[TAI_CHINH] ([ID_TaiChinh], [MaHD], [UEF], [TaiTro], [Khac], [TieuDe], [CreatedDate], [Hide]) VALUES (1, 1, CAST(0 AS Decimal(12, 0)), CAST(0 AS Decimal(12, 0)), N'', N'', CAST(N'2024-01-26T01:32:46.680' AS DateTime), 0)
INSERT [dbo].[TAI_CHINH] ([ID_TaiChinh], [MaHD], [UEF], [TaiTro], [Khac], [TieuDe], [CreatedDate], [Hide]) VALUES (2, 1, CAST(0 AS Decimal(12, 0)), CAST(0 AS Decimal(12, 0)), N'', N'', CAST(N'2024-01-26T01:40:08.040' AS DateTime), 0)
INSERT [dbo].[TAI_CHINH] ([ID_TaiChinh], [MaHD], [UEF], [TaiTro], [Khac], [TieuDe], [CreatedDate], [Hide]) VALUES (3, 1, CAST(0 AS Decimal(12, 0)), CAST(0 AS Decimal(12, 0)), N'', N'', CAST(N'2024-02-21T07:09:35.167' AS DateTime), 0)
INSERT [dbo].[TAI_CHINH] ([ID_TaiChinh], [MaHD], [UEF], [TaiTro], [Khac], [TieuDe], [CreatedDate], [Hide]) VALUES (4, 1, CAST(0 AS Decimal(12, 0)), CAST(0 AS Decimal(12, 0)), N'', N'', CAST(N'2024-02-29T18:25:46.817' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[TAI_CHINH] OFF
GO
SET IDENTITY_INSERT [dbo].[TAI_TRO] ON 

INSERT [dbo].[TAI_TRO] ([ID_TaiTro], [TenTaiTro], [DaiDien], [SDT], [Email], [Hide]) VALUES (1, N'Tập Đoàn Dầu Khí Miền Nam', N'Nguyễn Hải Nam', N'025356254           ', N'daukhi@miennam.com', 0)
INSERT [dbo].[TAI_TRO] ([ID_TaiTro], [TenTaiTro], [DaiDien], [SDT], [Email], [Hide]) VALUES (2, N'Nhà Máy Lọc Dầu Phương Đông', N'Phạm Văn Tây', N'094154852           ', N'LocDauPD@EOC.com.vn', 0)
INSERT [dbo].[TAI_TRO] ([ID_TaiTro], [TenTaiTro], [DaiDien], [SDT], [Email], [Hide]) VALUES (3, N',,', N'mmmmm', N'mmm                 ', N'mm', 0)
SET IDENTITY_INSERT [dbo].[TAI_TRO] OFF
GO
ALTER TABLE [dbo].[DANH_GIA]  WITH CHECK ADD  CONSTRAINT [FK_DANH_GIA_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[DANH_GIA] CHECK CONSTRAINT [FK_DANH_GIA_HOAT_DONG]
GO
ALTER TABLE [dbo].[DANH_GIA]  WITH CHECK ADD  CONSTRAINT [FK_DANH_GIA_SINH_VIEN] FOREIGN KEY([MSSV])
REFERENCES [dbo].[SINH_VIEN] ([MSSV])
GO
ALTER TABLE [dbo].[DANH_GIA] CHECK CONSTRAINT [FK_DANH_GIA_SINH_VIEN]
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS]  WITH CHECK ADD  CONSTRAINT [FK_DANHGIA_DETAILS_DANH_GIA] FOREIGN KEY([MaDanhGia])
REFERENCES [dbo].[DANH_GIA] ([MaDanhGia])
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS] CHECK CONSTRAINT [FK_DANHGIA_DETAILS_DANH_GIA]
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS]  WITH CHECK ADD  CONSTRAINT [FK_DANHGIA_DETAILS_HANG_MUC] FOREIGN KEY([HangMuc])
REFERENCES [dbo].[HANG_MUC] ([ID])
GO
ALTER TABLE [dbo].[DANHGIA_DETAILS] CHECK CONSTRAINT [FK_DANHGIA_DETAILS_HANG_MUC]
GO
ALTER TABLE [dbo].[GIANG_VIEN]  WITH CHECK ADD  CONSTRAINT [FK_GIANG_VIEN_KHOA] FOREIGN KEY([Khoa])
REFERENCES [dbo].[KHOA] ([MaKhoa])
GO
ALTER TABLE [dbo].[GIANG_VIEN] CHECK CONSTRAINT [FK_GIANG_VIEN_KHOA]
GO
ALTER TABLE [dbo].[HD_DOITAC]  WITH CHECK ADD  CONSTRAINT [FK_DT_HOATDONG_DOI_TAC] FOREIGN KEY([ID_DoiTac])
REFERENCES [dbo].[DOI_TAC] ([ID_DoiTac])
GO
ALTER TABLE [dbo].[HD_DOITAC] CHECK CONSTRAINT [FK_DT_HOATDONG_DOI_TAC]
GO
ALTER TABLE [dbo].[HD_DOITAC]  WITH CHECK ADD  CONSTRAINT [FK_DT_HOATDONG_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_DOITAC] CHECK CONSTRAINT [FK_DT_HOATDONG_HOAT_DONG]
GO
ALTER TABLE [dbo].[HD_GIANGVIEN]  WITH CHECK ADD  CONSTRAINT [FK_HD_GIANGVIEN_GIANG_VIEN] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GIANG_VIEN] ([MaGV])
GO
ALTER TABLE [dbo].[HD_GIANGVIEN] CHECK CONSTRAINT [FK_HD_GIANGVIEN_GIANG_VIEN]
GO
ALTER TABLE [dbo].[HD_GIANGVIEN]  WITH CHECK ADD  CONSTRAINT [FK_HD_GIANGVIEN_HD] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_GIANGVIEN] CHECK CONSTRAINT [FK_HD_GIANGVIEN_HD]
GO
ALTER TABLE [dbo].[HD_SINHVIEN]  WITH CHECK ADD  CONSTRAINT [FK_SV_HOATDONG_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_SINHVIEN] CHECK CONSTRAINT [FK_SV_HOATDONG_HOAT_DONG]
GO
ALTER TABLE [dbo].[HD_SINHVIEN]  WITH CHECK ADD  CONSTRAINT [FK_SV_HOATDONG_SINH_VIEN] FOREIGN KEY([MSSV])
REFERENCES [dbo].[SINH_VIEN] ([MSSV])
GO
ALTER TABLE [dbo].[HD_SINHVIEN] CHECK CONSTRAINT [FK_SV_HOATDONG_SINH_VIEN]
GO
ALTER TABLE [dbo].[HD_TAITRO]  WITH CHECK ADD  CONSTRAINT [FK_HD_TAITRO_HD] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[HD_TAITRO] CHECK CONSTRAINT [FK_HD_TAITRO_HD]
GO
ALTER TABLE [dbo].[HD_TAITRO]  WITH CHECK ADD  CONSTRAINT [FK_HD_TAITRO_TAITRO] FOREIGN KEY([ID_TaiTro])
REFERENCES [dbo].[TAI_TRO] ([ID_TaiTro])
GO
ALTER TABLE [dbo].[HD_TAITRO] CHECK CONSTRAINT [FK_HD_TAITRO_TAITRO]
GO
ALTER TABLE [dbo].[PHU_TRACH]  WITH CHECK ADD  CONSTRAINT [FK_PHU_TRACH_GIANG_VIEN] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GIANG_VIEN] ([MaGV])
GO
ALTER TABLE [dbo].[PHU_TRACH] CHECK CONSTRAINT [FK_PHU_TRACH_GIANG_VIEN]
GO
ALTER TABLE [dbo].[SINH_VIEN]  WITH CHECK ADD  CONSTRAINT [FK_SINH_VIEN_KHOA] FOREIGN KEY([Khoa])
REFERENCES [dbo].[KHOA] ([MaKhoa])
GO
ALTER TABLE [dbo].[SINH_VIEN] CHECK CONSTRAINT [FK_SINH_VIEN_KHOA]
GO
ALTER TABLE [dbo].[TAI_CHINH]  WITH CHECK ADD  CONSTRAINT [FK_TAI_CHINH_HOAT_DONG] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOAT_DONG] ([MaHD])
GO
ALTER TABLE [dbo].[TAI_CHINH] CHECK CONSTRAINT [FK_TAI_CHINH_HOAT_DONG]
GO
USE [master]
GO
ALTER DATABASE [Service_Learning] SET  READ_WRITE 
GO
