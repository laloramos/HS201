
    create table Attendee (
        Id INT not null,
       FirstName NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       Email NVARCHAR(255) null,
       ConferenceId INT null,
       primary key (Id)
    )

    create table Speaker (
        Id INT not null,
       FirstName NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       Bio NVARCHAR(255) null,
       primary key (Id)
    )

    create table [Session] (
        Id INT not null,
       Title NVARCHAR(255) null,
       Abstract NVARCHAR(255) null,
       ConferenceId INT null,
       primary key (Id)
    )

    alter table Attendee 
        add constraint FKCE6004EE11D69968 
        foreign key (ConferenceId) 
        references Conference

    alter table [Session] 
        add constraint FKBF1D3E3711D69968 
        foreign key (ConferenceId) 
        references Conference
