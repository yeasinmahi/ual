
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/01/2018 00:19:09
-- Generated from EDMX file: E:\Projects\GITS\Asp.net\UnitedAccessoriesLimited\UAL.BLL\Models\ualDbContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [UnitedAccessoriesDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccessToUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccessToUser] DROP CONSTRAINT [FK_AccessToUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Breakdown_ArtWorkUploads]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Breakdown] DROP CONSTRAINT [FK_Breakdown_ArtWorkUploads];
GO
IF OBJECT_ID(N'[dbo].[FK_Breakdown_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Breakdown] DROP CONSTRAINT [FK_Breakdown_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_Indent_ArtWorkUploads]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Indent] DROP CONSTRAINT [FK_Indent_ArtWorkUploads];
GO
IF OBJECT_ID(N'[dbo].[FK_Indent_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Indent] DROP CONSTRAINT [FK_Indent_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_ArtWorkUpload]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_ArtWorkUpload];
GO
IF OBJECT_ID(N'[dbo].[FK_PurchaseOrder_ArtWorkUploads]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseOrder] DROP CONSTRAINT [FK_PurchaseOrder_ArtWorkUploads];
GO
IF OBJECT_ID(N'[dbo].[FK_Sales_WOSize]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sales] DROP CONSTRAINT [FK_Sales_WOSize];
GO
IF OBJECT_ID(N'[dbo].[FK_Size_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Size] DROP CONSTRAINT [FK_Size_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_access_tbl_url]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccessToUser] DROP CONSTRAINT [FK_tbl_access_tbl_url];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Roles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AccessToUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccessToUser];
GO
IF OBJECT_ID(N'[dbo].[ArtWorkUploads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArtWorkUploads];
GO
IF OBJECT_ID(N'[dbo].[Breakdown]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Breakdown];
GO
IF OBJECT_ID(N'[dbo].[Indent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Indent];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[Payment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payment];
GO
IF OBJECT_ID(N'[dbo].[PurchaseOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseOrder];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Sales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sales];
GO
IF OBJECT_ID(N'[dbo].[Size]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Size];
GO
IF OBJECT_ID(N'[dbo].[Url]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Url];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[WOSize]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WOSize];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ArtWorkUploads'
CREATE TABLE [dbo].[ArtWorkUploads] (
    [refNumber] varchar(50)  NOT NULL,
    [label] varchar(30)  NOT NULL,
    [version] varchar(20)  NULL,
    [combo] varchar(50)  NOT NULL,
    [itemDescription] varchar(100)  NULL,
    [brand] varchar(30)  NULL,
    [dateOfSubmission] datetime  NOT NULL,
    [buyer] varchar(30)  NOT NULL,
    [comments] varchar(1000)  NULL,
    [status] varchar(50)  NULL,
    [createdBy] varchar(50)  NOT NULL,
    [Price] varchar(50)  NULL,
    [Finishing] varchar(50)  NULL,
    [isApproved] varchar(50)  NULL,
    [ApprovedBy] varchar(50)  NULL,
    [NoteAtFooter] varchar(50)  NULL,
    [DateOfApproval] datetime  NOT NULL,
    [SentForApprovalDate] datetime  NOT NULL,
    [ReceivedFrom] varchar(50)  NULL
);
GO

-- Creating table 'AccessToUsers'
CREATE TABLE [dbo].[AccessToUsers] (
    [accessID] int  NOT NULL,
    [urlID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [hasAccess] bit  NOT NULL
);
GO

-- Creating table 'Urls'
CREATE TABLE [dbo].[Urls] (
    [urlID] int  NOT NULL,
    [URL] varchar(50)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleID] bigint IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(50)  NOT NULL,
    [RoleDescription] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [CreatedBy] bigint  NOT NULL,
    [DateModified] datetime  NULL,
    [ModifiedBy] bigint  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [RoleID] bigint  NOT NULL,
    [Password] varchar(500)  NOT NULL,
    [Email] varchar(50)  NULL,
    [UserDetails] varchar(50)  NULL,
    [Address] varchar(100)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderRef] varchar(50)  NOT NULL,
    [OrderRefNo] varchar(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [CustomerName] varchar(50)  NOT NULL,
    [DeliveryPlace] varchar(50)  NOT NULL,
    [DeliveryDate] datetime  NOT NULL,
    [TOCompany] varchar(50)  NOT NULL,
    [FROMCompany] varchar(50)  NOT NULL,
    [Attn] varchar(50)  NOT NULL,
    [AccessoryType] varchar(50)  NOT NULL,
    [OrderQty] bigint  NOT NULL,
    [Remark] varchar(100)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [CreationDate] datetime  NULL,
    [ReferenceNo] varchar(50)  NOT NULL
);
GO

-- Creating table 'Sizes'
CREATE TABLE [dbo].[Sizes] (
    [OrderRef] varchar(50)  NOT NULL,
    [S] bigint  NULL,
    [M] bigint  NULL,
    [L] bigint  NULL,
    [XL] bigint  NULL,
    [XXL] bigint  NULL
);
GO

-- Creating table 'Breakdowns'
CREATE TABLE [dbo].[Breakdowns] (
    [PONumber] varchar(50)  NOT NULL,
    [Style1] varchar(50)  NULL,
    [Style2] varchar(50)  NULL,
    [Style3] varchar(50)  NULL,
    [Color] varchar(50)  NULL,
    [ReferenceNo] varchar(50)  NOT NULL,
    [OrderNo] varchar(50)  NOT NULL
);
GO

-- Creating table 'Indents'
CREATE TABLE [dbo].[Indents] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [IndentNo] bigint  NOT NULL,
    [WorkOrder] varchar(50)  NOT NULL,
    [ReferenceNo] varchar(50)  NOT NULL,
    [Status] varchar(50)  NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [PaymentID] int IDENTITY(1,1) NOT NULL,
    [PaymentMethod] varchar(50)  NULL,
    [BankAddress] varchar(150)  NOT NULL,
    [Swift] varchar(50)  NULL,
    [AccountNo] varchar(50)  NULL
);
GO

-- Creating table 'Sales'
CREATE TABLE [dbo].[Sales] (
    [Id] bigint  NOT NULL,
    [WOIssueDate] datetime  NOT NULL,
    [WO] varchar(50)  NOT NULL,
    [CustomerName] varchar(50)  NOT NULL,
    [Buyer] varchar(50)  NULL,
    [PO] varchar(100)  NULL,
    [CustomerOrderRef] varchar(50)  NULL,
    [SL] varchar(50)  NULL,
    [UALFileName] varchar(50)  NOT NULL,
    [LabelType] varchar(50)  NULL,
    [LabelName] varchar(50)  NULL,
    [Finishing] varchar(50)  NULL,
    [Version] varchar(50)  NULL,
    [BuyersRef] varchar(50)  NULL,
    [OrderQty] varchar(50)  NULL,
    [Pick] varchar(50)  NULL,
    [Cutter] varchar(50)  NULL,
    [Beam] varchar(50)  NULL,
    [RatePerDzn] varchar(50)  NULL,
    [TotalAmount] varchar(50)  NULL,
    [FinishingRate] varchar(50)  NULL,
    [FinishingTotalAmount] varchar(50)  NULL,
    [GTotalAmount] varchar(50)  NULL,
    [AmountReceived] varchar(50)  NULL,
    [OrderwiseAmountDue] varchar(50)  NULL,
    [Label] varchar(50)  NULL,
    [ProductionHours] int  NULL,
    [Priority] varchar(50)  NULL,
    [Status] varchar(50)  NULL,
    [Combo] varchar(50)  NULL,
    [IsSizewise] bit  NULL,
    [WOS] bigint  NULL
);
GO

-- Creating table 'WOSizes'
CREATE TABLE [dbo].[WOSizes] (
    [sn1] varchar(50)  NULL,
    [s1] int  NULL,
    [sn2] varchar(50)  NULL,
    [s2] int  NULL,
    [sn3] varchar(50)  NULL,
    [s3] int  NULL,
    [sn4] varchar(50)  NULL,
    [s4] int  NULL,
    [sn5] varchar(50)  NULL,
    [s5] int  NULL,
    [sn6] varchar(50)  NULL,
    [s6] int  NULL,
    [sn7] varchar(50)  NULL,
    [s7] int  NULL,
    [sn8] varchar(50)  NULL,
    [s8] int  NULL,
    [sn9] varchar(50)  NULL,
    [s9] int  NULL,
    [sn10] varchar(50)  NULL,
    [s10] int  NULL,
    [sn11] varchar(50)  NULL,
    [s11] int  NULL,
    [sn12] varchar(50)  NULL,
    [s12] int  NULL,
    [sn13] varchar(50)  NULL,
    [s13] int  NULL,
    [sn14] varchar(50)  NULL,
    [s14] int  NULL,
    [id] bigint IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'PurchaseOrders'
CREATE TABLE [dbo].[PurchaseOrders] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [PO] varchar(50)  NULL,
    [Style] varchar(50)  NULL,
    [Ref] varchar(50)  NOT NULL,
    [OrderQty] varchar(50)  NULL,
    [Remarks] varchar(50)  NULL,
    [SN1] varchar(50)  NULL,
    [S1] int  NULL,
    [SN2] varchar(50)  NULL,
    [S2] int  NULL,
    [SN3] varchar(50)  NULL,
    [S3] int  NULL,
    [SN4] varchar(50)  NULL,
    [S4] int  NULL,
    [SN5] varchar(50)  NULL,
    [S5] int  NULL,
    [SN6] varchar(50)  NULL,
    [S6] int  NULL,
    [SN7] varchar(50)  NULL,
    [S7] int  NULL,
    [SN8] varchar(50)  NULL,
    [S8] int  NULL,
    [SN9] varchar(50)  NULL,
    [S9] int  NULL,
    [SN10] varchar(50)  NULL,
    [S10] int  NULL,
    [SN11] varchar(50)  NULL,
    [S11] int  NULL,
    [SN12] varchar(50)  NULL,
    [S12] int  NULL,
    [SN13] varchar(50)  NULL,
    [S13] int  NULL,
    [SN14] varchar(50)  NULL,
    [S14] int  NULL,
    [IssueDate] datetime  NULL,
    [Status] varchar(50)  NULL,
    [ToCompany] varchar(50)  NULL,
    [FromCompany] varchar(50)  NULL,
    [Attn] varchar(50)  NULL,
    [OrderRef] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [refNumber] in table 'ArtWorkUploads'
ALTER TABLE [dbo].[ArtWorkUploads]
ADD CONSTRAINT [PK_ArtWorkUploads]
    PRIMARY KEY CLUSTERED ([refNumber] ASC);
GO

-- Creating primary key on [accessID] in table 'AccessToUsers'
ALTER TABLE [dbo].[AccessToUsers]
ADD CONSTRAINT [PK_AccessToUsers]
    PRIMARY KEY CLUSTERED ([accessID] ASC);
GO

-- Creating primary key on [urlID] in table 'Urls'
ALTER TABLE [dbo].[Urls]
ADD CONSTRAINT [PK_Urls]
    PRIMARY KEY CLUSTERED ([urlID] ASC);
GO

-- Creating primary key on [RoleID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [OrderRef] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderRef] ASC);
GO

-- Creating primary key on [OrderRef] in table 'Sizes'
ALTER TABLE [dbo].[Sizes]
ADD CONSTRAINT [PK_Sizes]
    PRIMARY KEY CLUSTERED ([OrderRef] ASC);
GO

-- Creating primary key on [PONumber] in table 'Breakdowns'
ALTER TABLE [dbo].[Breakdowns]
ADD CONSTRAINT [PK_Breakdowns]
    PRIMARY KEY CLUSTERED ([PONumber] ASC);
GO

-- Creating primary key on [ID] in table 'Indents'
ALTER TABLE [dbo].[Indents]
ADD CONSTRAINT [PK_Indents]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [PaymentID] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([PaymentID] ASC);
GO

-- Creating primary key on [WO] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [PK_Sales]
    PRIMARY KEY CLUSTERED ([WO] ASC);
GO

-- Creating primary key on [id] in table 'WOSizes'
ALTER TABLE [dbo].[WOSizes]
ADD CONSTRAINT [PK_WOSizes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [ID] in table 'PurchaseOrders'
ALTER TABLE [dbo].[PurchaseOrders]
ADD CONSTRAINT [PK_PurchaseOrders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [urlID] in table 'AccessToUsers'
ALTER TABLE [dbo].[AccessToUsers]
ADD CONSTRAINT [FK_tbl_access_tbl_url]
    FOREIGN KEY ([urlID])
    REFERENCES [dbo].[Urls]
        ([urlID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_access_tbl_url'
CREATE INDEX [IX_FK_tbl_access_tbl_url]
ON [dbo].[AccessToUsers]
    ([urlID]);
GO

-- Creating foreign key on [UserID] in table 'AccessToUsers'
ALTER TABLE [dbo].[AccessToUsers]
ADD CONSTRAINT [FK_AccessToUser_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessToUser_User'
CREATE INDEX [IX_FK_AccessToUser_User]
ON [dbo].[AccessToUsers]
    ([UserID]);
GO

-- Creating foreign key on [RoleID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Roles]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Roles]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Roles'
CREATE INDEX [IX_FK_User_Roles]
ON [dbo].[Users]
    ([RoleID]);
GO

-- Creating foreign key on [ReferenceNo] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_ArtWorkUpload]
    FOREIGN KEY ([ReferenceNo])
    REFERENCES [dbo].[ArtWorkUploads]
        ([refNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_ArtWorkUpload'
CREATE INDEX [IX_FK_Order_ArtWorkUpload]
ON [dbo].[Orders]
    ([ReferenceNo]);
GO

-- Creating foreign key on [OrderRef] in table 'Sizes'
ALTER TABLE [dbo].[Sizes]
ADD CONSTRAINT [FK_Size_Order]
    FOREIGN KEY ([OrderRef])
    REFERENCES [dbo].[Orders]
        ([OrderRef])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReferenceNo] in table 'Breakdowns'
ALTER TABLE [dbo].[Breakdowns]
ADD CONSTRAINT [FK_Breakdown_ArtWorkUploads]
    FOREIGN KEY ([ReferenceNo])
    REFERENCES [dbo].[ArtWorkUploads]
        ([refNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Breakdown_ArtWorkUploads'
CREATE INDEX [IX_FK_Breakdown_ArtWorkUploads]
ON [dbo].[Breakdowns]
    ([ReferenceNo]);
GO

-- Creating foreign key on [OrderNo] in table 'Breakdowns'
ALTER TABLE [dbo].[Breakdowns]
ADD CONSTRAINT [FK_Breakdown_Order]
    FOREIGN KEY ([OrderNo])
    REFERENCES [dbo].[Orders]
        ([OrderRef])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Breakdown_Order'
CREATE INDEX [IX_FK_Breakdown_Order]
ON [dbo].[Breakdowns]
    ([OrderNo]);
GO

-- Creating foreign key on [ReferenceNo] in table 'Indents'
ALTER TABLE [dbo].[Indents]
ADD CONSTRAINT [FK_Indent_ArtWorkUploads]
    FOREIGN KEY ([ReferenceNo])
    REFERENCES [dbo].[ArtWorkUploads]
        ([refNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Indent_ArtWorkUploads'
CREATE INDEX [IX_FK_Indent_ArtWorkUploads]
ON [dbo].[Indents]
    ([ReferenceNo]);
GO

-- Creating foreign key on [WorkOrder] in table 'Indents'
ALTER TABLE [dbo].[Indents]
ADD CONSTRAINT [FK_Indent_Order]
    FOREIGN KEY ([WorkOrder])
    REFERENCES [dbo].[Orders]
        ([OrderRef])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Indent_Order'
CREATE INDEX [IX_FK_Indent_Order]
ON [dbo].[Indents]
    ([WorkOrder]);
GO

-- Creating foreign key on [WOS] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_Sales_WOSize]
    FOREIGN KEY ([WOS])
    REFERENCES [dbo].[WOSizes]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sales_WOSize'
CREATE INDEX [IX_FK_Sales_WOSize]
ON [dbo].[Sales]
    ([WOS]);
GO

-- Creating foreign key on [Ref] in table 'PurchaseOrders'
ALTER TABLE [dbo].[PurchaseOrders]
ADD CONSTRAINT [FK_PurchaseOrder_ArtWorkUploads]
    FOREIGN KEY ([Ref])
    REFERENCES [dbo].[ArtWorkUploads]
        ([refNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseOrder_ArtWorkUploads'
CREATE INDEX [IX_FK_PurchaseOrder_ArtWorkUploads]
ON [dbo].[PurchaseOrders]
    ([Ref]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------