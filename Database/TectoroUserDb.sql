If Not Exists (Select * from sys.schemas where name = '[Domain]')
      Begin
	    If Exists (Select * from sys.tables where name = 'Users')
		Begin 
			Drop Table [Domain].[Users];
			print ' Table deleted.'
		End

		drop Schema [Domain];
		print ' Schema deleted.'
	  End

Go

create Schema [Domain];

Go
print ' Schema created.'
Go

CREATE TABLE [Domain].[Users] (

[UserId] INT IDENTITY (1, 1) NOT NULL,

[UserName] NVARCHAR (200) NOT NULL,

[Email] NVARCHAR (1000) NULL,

[Alias] NVARCHAR (1000) NULL,

[FirstName] NVARCHAR (1000) NULL,

[LastName] NVARCHAR (1000) NULL,

[Position] NVARCHAR (200) NULL,

[Level] Int NULL,

[ManagerId] Int NULL,

CONSTRAINT [PK_Domain.Users] PRIMARY KEY CLUSTERED ([UserId] ASC))

print ' Table created.'

Go