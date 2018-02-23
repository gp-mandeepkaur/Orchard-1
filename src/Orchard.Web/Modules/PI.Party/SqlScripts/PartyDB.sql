
/*********************************************************************************/
IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Account_Master_Account')
	BEGIN
		ALTER TABLE [dbo].[Account_Master] DROP CONSTRAINT [FK_Account_Master_Account]
	END
GO


IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Account_Master_Account')
	BEGIN
ALTER TABLE [dbo].[Account_Master] ADD CONSTRAINT [FK_Account_Master_Account] FOREIGN KEY
	([ID])
	REFERENCES [dbo].[Account]([ID]) ON DELETE CASCADE
END
GO

/*********************************************************************************/
IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Address_Address')
	BEGIN
		ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Address]
	END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Address_AddressPurpose')
	BEGIN
	ALTER TABLE [dbo].[Address] ADD CONSTRAINT [FK_Address_AddressPurpose] FOREIGN KEY
		([PurposeTypeID])
		REFERENCES [dbo].[AddressPurpose]([ID])
	END
GO

/*********************************************************************************/
IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Party_Person_Party')
	BEGIN
		ALTER TABLE [dbo].[Party_Person] DROP CONSTRAINT [FK_Party_Person_Party]
	END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Party_Person_Party')
	BEGIN
ALTER TABLE [dbo].[Party_Person] ADD CONSTRAINT [FK_Party_Person_Party] FOREIGN KEY
	([PartyId])
	REFERENCES [dbo].[Party]
	([PartyId]) ON DELETE CASCADE
END
GO

/*********************************************************************************/
IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Party_Organization_Party')
	BEGIN
		ALTER TABLE [dbo].[Party_Organization] DROP CONSTRAINT [FK_Party_Organization_Party]
	END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Party_Organization_Party')
	BEGIN
ALTER TABLE [dbo].[Party_Organization] ADD CONSTRAINT [FK_Party_Organization_Party] FOREIGN KEY
	([PartyId])
	REFERENCES [dbo].[Party]([PartyId]) ON DELETE CASCADE
END
GO

/*********************************************************************************/

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='PK_PartyRole_1')
	BEGIN
		ALTER TABLE [dbo].[PartyRole] DROP CONSTRAINT [PK_PartyRole_1]
	END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='PK_PartyRole')
	BEGIN
ALTER TABLE [dbo].[PartyRole] ADD CONSTRAINT [PartyRole] PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END

/*********************************************************************************/

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='PK_Person')
	BEGIN
		ALTER TABLE [dbo].[Party_Person] DROP CONSTRAINT [PK_Person]
	END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='PK_Party_Person')
	BEGIN
ALTER TABLE [dbo].[Party_Person] ADD CONSTRAINT [PK_Party_Person] PRIMARY KEY CLUSTERED
	(
		[PartyId] ASC
	) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END

/*********************************************************************************/

--IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
--		WHERE CONSTRAINT_NAME='PK_Account_1')
--	BEGIN
--		ALTER TABLE [dbo].[Account] DROP CONSTRAINT [PK_Account_1]
--	END
--GO

--IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
--		WHERE CONSTRAINT_NAME='PK_Account')
--	BEGIN
--ALTER TABLE [dbo].[Account] ADD CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED
--	(
--		[ID] ASC
--	) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--END
--GO

/*********************************************************************************/
IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_EmailAddress_EmailPurpose')
	BEGIN
		ALTER TABLE [dbo].[EmailAddress] DROP CONSTRAINT [FK_EmailAddress_EmailPurpose]
	END
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='PK_EmailPurpose')
	BEGIN
		ALTER TABLE [dbo].[EmailPurpose] DROP CONSTRAINT [PK_EmailPurpose]
	END
GO


IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='PK_EmailPurpose')
	BEGIN
ALTER TABLE [dbo].[EmailPurpose] ADD CONSTRAINT [PK_EmailPurpose] PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_EmailAddress_EmailPurpose')
	BEGIN
		ALTER TABLE [dbo].[EmailAddress] ADD CONSTRAINT [FK_EmailAddress_EmailPurpose] FOREIGN KEY
	([PurposeTypeID])
	REFERENCES [dbo].[EmailPurpose]([ID])
END

/*********************************************************************************/
IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Address_Party')
	BEGIN
ALTER TABLE [dbo].[Address] ADD CONSTRAINT [FK_Address_Party] FOREIGN KEY
	([PartyId])
	REFERENCES [dbo].[Party] ([PartyId])
	END
GO

/*********************************************************************************/
ALTER TABLE [dbo].[Address] ALTER COLUMN [State] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE [dbo].[Address] ALTER COLUMN [City] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE [dbo].[Address] ALTER COLUMN [Zip] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE [dbo].[Address] ALTER COLUMN [Country] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

/*********************************************************************************/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Registration'))
BEGIN
    CREATE TABLE [dbo].[Registration]
	(
		[ID] [int] IDENTITY (1,1) NOT NULL,
		[Email] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		[RegistrationTokenId] [uniqueidentifier] NULL,
		[DateTimeCreated] [datetime] NULL,
		[PartyId] [uniqueidentifier] NULL,
		[LoginCreatedDate] [datetime] NULL,
		[DateTimeTokenSent] [datetime] NULL,
		[TokenCount] [int] NULL,
		[RegistrationDenied] [bit] NOT NULL CONSTRAINT [DF_Registration_RegistrationDenied] DEFAULT ((0)),
		[ReasonForDenial] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED
		(
			[ID] ASC
		) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Registration_Party')
	BEGIN
ALTER TABLE [dbo].[Registration] ADD CONSTRAINT [FK_Registration_Party] FOREIGN KEY
	(
		[PartyId]
	)
	REFERENCES [dbo].[Party]
	(
		[PartyId]
	)
	END
GO

/*********************************************************************************/
IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_Registration_Party' AND object_id = OBJECT_ID('Registration'))
		BEGIN
		CREATE NONCLUSTERED INDEX [IX_FK_Registration_Party] ON [dbo].[Registration]
		(
			[PartyId] ASC
		) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	END
GO

/*********************************************************************************/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'ModuleSchemaSettings'))
BEGIN
	CREATE TABLE [dbo].[ModuleSchemaSettings]
	(
		[ID] [int] IDENTITY (1,1) NOT NULL,
		[ModuleSchemaVersion] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[DateSchemaUpdated] [datetime] NULL,
		[UpdatedBy] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		CONSTRAINT [PK_ModuleSchemaSettings] PRIMARY KEY CLUSTERED
		(
			[ID] ASC
		) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

/*********************************************************************************/
IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_PartyRole_Party' AND object_id = OBJECT_ID('PartyRole'))
	BEGIN
			CREATE NONCLUSTERED INDEX [IX_FK_PartyRole_Party] ON [dbo].[PartyRole]
			(
				[PartyId] ASC
			) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	END
GO
/*********************************************************************************/

IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_Address_AddressPurpose' AND object_id = OBJECT_ID('Address'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_Address_AddressPurpose] ON [dbo].[Address]
(
	[PurposeTypeID] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

/*********************************************************************************/
IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_PartyRole_PartyRole' AND object_id = OBJECT_ID('PartyRole'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_PartyRole_PartyRole] ON [dbo].[PartyRole]
(
	[RoleTypeId] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

/*********************************************************************************/

IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_Phone_Party' AND object_id = OBJECT_ID('Phone'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_Phone_Party] ON [dbo].[Phone]
(
	[PartyId] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

/*********************************************************************************/

IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_EmailAddress_Party' AND object_id = OBJECT_ID('EmailAddress'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_EmailAddress_Party] ON [dbo].[EmailAddress]
(
	[PartyId] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

/*********************************************************************************/

IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_EmailAddress_EmailPurpose' AND object_id = OBJECT_ID('EmailAddress'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_EmailAddress_EmailPurpose] ON [dbo].[EmailAddress]
(
	[PurposeTypeID] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

/*********************************************************************************/
IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_Address_Address' AND object_id = OBJECT_ID('Address'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_Address_Address] ON [dbo].[Address]
(
	[PartyId] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

/*********************************************************************************/

IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_Phone_PhonePurpose' AND object_id = OBJECT_ID('Phone'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_Phone_PhonePurpose] ON [dbo].[Phone]
(
	[PurposeTypeID] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

/*********************************************************************************/

IF NOT EXISTS(
SELECT * 
	FROM sys.indexes 
	WHERE name='IX_FK_Account_Party' AND object_id = OBJECT_ID('Account'))
	BEGIN
CREATE NONCLUSTERED INDEX [IX_FK_Account_Party] ON [dbo].[Account]
(
	[PartyId] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY  = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO


/*********************************************************************************/

IF EXISTS (SELECT 1 FROM [dbo].[AddressPurpose] WHERE ID=1 AND Name != 'Permanent Address')
BEGIN
UPDATE [dbo].[AddressPurpose] SET Name = 'Permanent Address' WHERE ID=1
END

IF EXISTS (SELECT 1 FROM [dbo].[AddressPurpose] WHERE ID=2)
BEGIN
DELETE FROM [AddressPurpose] WHERE ID=2
END

IF EXISTS (SELECT 1 FROM [dbo].[EmailPurpose] WHERE ID=1 AND Name != 'Primary Address')
BEGIN
UPDATE  [dbo].[EmailPurpose] SET Name = 'Primary Address' WHERE ID=1
END

IF EXISTS (SELECT 1 FROM [dbo].[EmailPurpose] WHERE ID=2)
BEGIN
DELETE FROM [dbo].[EmailPurpose] WHERE ID=2
END

IF EXISTS (SELECT 1 FROM [dbo].[PhonePurpose] WHERE ID=1 AND Name != 'US Cell Phone')
BEGIN
UPDATE [dbo].[PhonePurpose] SET Name = 'US Cell Phone' WHERE ID=1
END

IF EXISTS (SELECT 1 FROM [dbo].[PhonePurpose] WHERE ID=2)
BEGIN
UPDATE [dbo].[PhonePurpose] SET name='Home Phone' WHERE ID=2
END
ELSE
BEGIN
	SET IDENTITY_INSERT [dbo].[PhonePurpose] ON 
	INSERT INTO [dbo].[PhonePurpose](ID, Name) VALUES(2,'Home Phone')
	SET IDENTITY_INSERT [dbo].[PhonePurpose] OFF 
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[PhonePurpose] WHERE ID = 3)
BEGIN
	SET IDENTITY_INSERT [dbo].[PhonePurpose] ON 
	INSERT INTO [dbo].[PhonePurpose](ID, Name) VALUES(3,'Work Phone')
	SET IDENTITY_INSERT [dbo].[PhonePurpose] OFF 
END

/**************Changes for Phone number format on 06-22-2017*************************/

IF NOT EXISTS(SELECT 1 FROM sys.columns 
      WHERE Name      = N'PhoneExchange'
      AND Object_ID = Object_ID(N'Phone')) 
BEGIN
	  ALTER TABLE [dbo].[Phone] ADD 
	  [PhoneExchange] nvarchar(50) NULL
END

IF EXISTS(SELECT 1 FROM sys.columns 
      WHERE Name      = N'PhoneNumber'
      AND Object_ID = Object_ID(N'Phone')) 
BEGIN
	EXECUTE	SP_RENAME 'Phone.PhoneNumber', 'PhoneLine' , 'COLUMN';
END

/*********************************************************************************/


/****** Object:  Table [dbo].[Account_SECRM]    Script Date: 12/21/2017 2:39:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Account_SECRM'))
BEGIN
			CREATE TABLE [dbo].[Account_SECRM](
				[PartyId] [uniqueidentifier] NOT NULL,
				[AccountNumber] [int] NULL,
				[CreatedDate] [datetime] NULL,
				[ModifiedDate] [datetime] NULL,
				[CreatedBy] [varchar](50) NULL,
			 CONSTRAINT [PK_Account_SECRM] PRIMARY KEY CLUSTERED 
			(
				[PartyId] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Account_SECRM_Party')
	BEGIN
		ALTER TABLE [dbo].[Account_SECRM]  WITH CHECK ADD  CONSTRAINT [FK_Account_SECRM_Party] FOREIGN KEY([PartyId])
		REFERENCES [dbo].[Party] ([PartyId])
END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
		WHERE CONSTRAINT_NAME='FK_Account_SECRM_Party')
	BEGIN
ALTER TABLE [dbo].[Account_SECRM] CHECK CONSTRAINT [FK_Account_SECRM_Party]
END
GO

/**************Changes to add ModifiedBy in column Account_SECRM 01-11-2017*************************/

IF NOT EXISTS(SELECT 1 FROM sys.columns 
      WHERE Name      = N'ModifiedBy'
      AND Object_ID = Object_ID(N'Account_SECRM')) 
BEGIN
	ALTER TABLE [dbo].[Account_SECRM] add [ModifiedBy] [varchar](50) NULL
END