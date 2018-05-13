-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************

DROP TABLE [reservation];
GO


DROP TABLE [site];
GO


DROP TABLE [campground];
GO


DROP TABLE [park];
GO


--************************************** [park]

CREATE TABLE [park]
(
 [park_id]        INT IDENTITY (1, 1) NOT NULL ,
 [name]           VARCHAR(50) NOT NULL ,
 [state]          VARCHAR(50) NOT NULL ,
 [establish_year] INT NOT NULL ,
 [area]           INT NOT NULL ,
 [visitors]       INT NOT NULL ,
 [description]    TEXT NOT NULL ,

 CONSTRAINT [PK_park] PRIMARY KEY CLUSTERED ([park_id] ASC)
);
GO


--************************************** [campground]

CREATE TABLE [campground]
(
 [campground_id] INT IDENTITY (1, 1) NOT NULL ,
 [park_id]       INT NOT NULL ,
 [name]          TEXT NOT NULL ,
 [open_from_mm]  INT NOT NULL ,
 [open_to_mm]    INT NOT NULL ,
 [daily_fee]     INT NOT NULL ,

 CONSTRAINT [PK_campground] PRIMARY KEY CLUSTERED ([campground_id] ASC),
 CONSTRAINT [FK_32] FOREIGN KEY ([park_id])
  REFERENCES [park]([park_id])
);
GO


--SKIP Index: [fkIdx_32]


--************************************** [site]

CREATE TABLE [site]
(
 [site_id]                INT IDENTITY (1, 1) NOT NULL ,
 [campground_id]          INT NOT NULL ,
 [campground_site_number] INT NOT NULL ,
 [max_occupancy]          INT NOT NULL ,
 [accessible]             BIT NOT NULL ,
 [max_rv_length]          INT NOT NULL ,
 [utilities]              BIT NOT NULL ,

 CONSTRAINT [PK_site] PRIMARY KEY CLUSTERED ([site_id] ASC),
 CONSTRAINT [FK_45] FOREIGN KEY ([campground_id])
  REFERENCES [campground]([campground_id])
);
GO


--SKIP Index: [fkIdx_45]


--************************************** [reservation]

CREATE TABLE [reservation]
(
 [reservation_id] INT IDENTITY (1, 1) NOT NULL ,
 [site_id]        INT NOT NULL ,
 [name]           TEXT NOT NULL ,
 [from_date]      DATE NOT NULL ,
 [to_date]        DATE NOT NULL ,
 [create_date]    DATE NOT NULL ,

 CONSTRAINT [PK_reservation] PRIMARY KEY CLUSTERED ([reservation_id] ASC),
 CONSTRAINT [FK_57] FOREIGN KEY ([site_id])
  REFERENCES [site]([site_id])
);
GO


--SKIP Index: [fkIdx_57]


