
CREATE TABLE [dbo].[Conference_tmp](
	[Id] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](255) NULL,
	[HashTag] [nvarchar](255) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Cost] [decimal](19, 5) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Attendee_tmp](
	[Id] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[ConferenceId] [int] NULL REFERENCES dbo.Conference_tmp(id)
) ON [PRIMARY]

CREATE TABLE [dbo].[Speaker_tmp](
	[Id] [int] NOT NULL IDENTITY(1,1) primary key,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Bio] [nvarchar](255) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Session_tmp](
	[Id] [int] NOT NULL  IDENTITY(1,1) PRIMARY KEY,
	[Title] [nvarchar](255) NULL,
	[Abstract] [nvarchar](255) NULL,
	[ConferenceId] [int] NULL REFERENCES conference_tmp(id),
	SpeakerId int not null references speaker_tmp(id)
) ON [PRIMARY]

set identity_insert [Conference_tmp] on
insert into [Conference_tmp] (id, name, hashtag, StartDate, EndDate, Cost)
select * from Conference
set identity_insert [Conference_tmp] off

set identity_insert [Attendee_tmp] on
insert into [dbo].[Attendee_tmp](id, firstname, LastName, Email, ConferenceId)
select * from attendee
set identity_insert [Attendee_tmp] off

set identity_insert [Speaker_tmp] on
insert into [Speaker_tmp] (id, FirstName, LastName, Bio)
select * from speaker
set identity_insert [Speaker_tmp] off

set identity_insert [Session_tmp] on
insert into [Session_tmp] (id, Title, Abstract, ConferenceId)
select * from [session]
set identity_insert [Session_tmp] off

drop table [Session]
drop table speaker
drop table attendee
drop table Conference

exec sys.sp_rename 'session_tmp', 'session'
exec sys.sp_rename 'speaker_tmp', 'speaker'
exec sys.sp_rename 'attendee_tmp', 'attendee'
exec sys.sp_rename 'Conference_tmp', 'Conference'

