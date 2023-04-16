--- Cek apakah nama Database sudah ada?
IF DB_ID('KredikuDB') IS NOT NULL   
BEGIN
   PRINT 'Database KredikuDB Sudah Tersedia'
END
ELSE
BEGIN
	CREATE DATABASE [KredikuDB]
END
GO

-- Gunakan DB Baru
USE [KredikuDB]
GO

-- cek apakah tabel2 yang akan dibuat sudah ada? kalau ada drop
IF EXISTS(SELECT TOP 1 1 FROM sys.Tables WHERE  Name = N'tr_bpkb' AND Type = N'U')
BEGIN
	PRINT 'Table tr_bpkb sudah tersedia, tabel akan di drop terlebih dahulu'
    DROP TABLE dbo.tr_bpkb
END

IF EXISTS(SELECT TOP 1 1 FROM sys.Tables WHERE  Name = N'ms_storage_location' AND Type = N'U')
BEGIN
	PRINT 'Table ms_storage_location sudah tersedia, tabel akan di drop terlebih dahulu'
    DROP TABLE dbo.ms_storage_location
END
GO

-- Proses Create Table
-- Table Location
CREATE TABLE dbo.ms_storage_location
(
	location_id		VARCHAR(10) PRIMARY KEY,
	location_name	VARCHAR(100))
GO

-- Table Transaction
CREATE TABLE dbo.tr_bpkb
(
	agreement_number		VARCHAR(100) PRIMARY KEY,
	bpkb_no					VARCHAR(100),
	branch_id				VARCHAR(10),
	bpkb_date				DATETIME,
	faktur_no				VARCHAR(100),
	faktur_date				DATETIME,
	location_id				VARCHAR(10) FOREIGN KEY (location_id) REFERENCES ms_storage_location (location_id),
	police_no				VARCHAR(20), 
	bpkb_date_in			DATETIME
)

CREATE NONCLUSTERED INDEX tr_bpkb_NN1 ON dbo.tr_bpkb (location_id)
GO


-- Insert data parameter location
INSERT INTO dbo.ms_storage_location
(
	location_id,
	location_name
)
SELECT 'BDG001', 'Bandung Utara'
UNION ALL SELECT 'BDG002', 'Bandung Selatan'
UNION ALL SELECT 'JKT001', 'Jakarta Pusat'
UNION ALL SELECT 'JKT002', 'Jakarta Selatan'
UNION ALL SELECT 'BSD001', 'BSD Aeon'
UNION ALL SELECT 'SBY001', 'Surabaya'
UNION ALL SELECT 'MLG001', 'Malang'
UNION ALL SELECT 'BGR001', 'Bogor'
UNION ALL SELECT 'BKS001', 'Bekasi Barat'
UNION ALL SELECT 'BKS002', 'Bekasi Timur'
UNION ALL SELECT 'CMH001', 'Cimahi Kota'
GO


-- Select data pada tabel2 yang telah dibuat
SELECT TOP 100 *
FROM dbo.tr_bpkb WITH(NOLOCK)
GO

SELECT TOP 100 *
FROM dbo.ms_storage_location WITH(NOLOCK)
GO