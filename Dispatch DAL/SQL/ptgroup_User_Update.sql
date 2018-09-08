use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_User_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_User_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Users table.
-- =============================================
create procedure [dbo].[ptgroup_User_Update]
(
	@Id int,
	@UserName nvarchar(50),
	@Password nvarchar(350),
	@LastPass nvarchar(350),
	@RoleId int,
	@EmployeeId int,
	@LastChangePassword datetime,
	@LastLogin datetime,
	@Token varchar(500)
)

as

set nocount on

update [Users]
set [UserName] = @UserName,
	[Password] = @Password,
	[LastPass] = @LastPass,
	[RoleId] = @RoleId,
	[EmployeeId] = @EmployeeId,
	[LastChangePassword] = @LastChangePassword,
	[LastLogin] = @LastLogin,
	[Token] = @Token
where [Id] = @Id
go
