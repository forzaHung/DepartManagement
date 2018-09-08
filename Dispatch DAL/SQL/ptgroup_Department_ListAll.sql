use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Department_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Department_ListAll]
go

create procedure [dbo].[ptgroup_Department_ListAll]

as

set nocount on

select  [Id],
	[Name],
	[CreatedDate],
	[ModifiedDate]
from [Department] WITH (NOLOCK)
go
