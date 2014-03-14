
    if exists (select * from dbo.sysobjects where id = object_id(N'Conference') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Conference

    create table Conference (
        Id INT not null,
       Name NVARCHAR(255) null,
       primary key (Id)
    )
