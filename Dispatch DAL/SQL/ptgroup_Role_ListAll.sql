use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Role_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Role_ListAll]
go

create procedure [dbo].[ptgroup_Role_ListAll]

as

set nocount on

select  [UserId],
	[ModuleId],
	[Add],
	[Edit],
	[View],
	[Delete],
	[Import],
	[Export],
	[Upload],
	[Publish],
	[Report],
	[Print]
from [Role] WITH (NOLOCK)
go
