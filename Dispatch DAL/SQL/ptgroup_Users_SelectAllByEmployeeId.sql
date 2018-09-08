use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Users_SelectAllByEmployeeId]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Users_SelectAllByEmployeeId]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure select all record in Users table.
-- =============================================
create procedure [dbo].[ptgroup_Users_SelectAllByEmployeeId]
(
	@EmployeeId int
)

as

set nocount on

select [Id],
	[UserName],
	[Password],
	[LastPass],
	[RoleId],
	[EmployeeId],
	[LastChangePassword],
	[LastLogin],
	[Token]
from [Users]
where [EmployeeId] = @EmployeeId
go
