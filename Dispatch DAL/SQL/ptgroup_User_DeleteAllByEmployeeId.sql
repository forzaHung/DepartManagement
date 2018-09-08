use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_User_DeleteAllByEmployeeId]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_User_DeleteAllByEmployeeId]
go

create procedure [dbo].[ptgroup_User_DeleteAllByEmployeeId]
(
	@EmployeeId int
)

as

set nocount on

delete from [Users]
where [EmployeeId] = @EmployeeId
go
