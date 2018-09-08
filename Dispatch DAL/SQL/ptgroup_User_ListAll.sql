use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_User_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_User_ListAll]
go

create procedure [dbo].[ptgroup_User_ListAll]

as

set nocount on

select  [Id],
	[UserName],
	[Password],
	[LastPass],
	[RoleId],
	[EmployeeId],
	[LastChangePassword],
	[LastLogin],
	[Token]
from [Users] WITH (NOLOCK)
go
