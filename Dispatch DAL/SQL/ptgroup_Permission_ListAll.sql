use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Permission_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Permission_ListAll]
go

create procedure [dbo].[ptgroup_Permission_ListAll]

as

set nocount on

select  [Id],
	[UserId],
	[GroupId]
from [Permission] WITH (NOLOCK)
go
