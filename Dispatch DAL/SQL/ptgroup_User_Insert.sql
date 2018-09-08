use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_User_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_User_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Users table.
-- =============================================
create procedure [dbo].[ptgroup_User_Insert]
(
	@UserName nvarchar(50),
	@Password nvarchar(350),
	@LastPass nvarchar(350),
	@RoleId int,
	@EmployeeId int,
	@LastChangePassword datetime,
	@LastLogin datetime,
	@Token varchar(500),
	@Id int OUTPUT
)

as

set nocount on

insert into [Users]
(
	[UserName],
	[Password],
	[LastPass],
	[RoleId],
	[EmployeeId],
	[LastChangePassword],
	[LastLogin],
	[Token]
)
values
(
	@UserName,
	@Password,
	@LastPass,
	@RoleId,
	@EmployeeId,
	@LastChangePassword,
	@LastLogin,
	@Token
)

set @Id = scope_identity()
go
