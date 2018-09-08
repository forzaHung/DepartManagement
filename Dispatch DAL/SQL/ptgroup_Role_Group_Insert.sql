use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Role_Group_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Role_Group_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Role_Group table.
-- =============================================
create procedure [dbo].[ptgroup_Role_Group_Insert]
(
	@Name nvarchar(50),
	@ID int OUTPUT
)

as

set nocount on

insert into [Role_Group]
(
	[Name]
)
values
(
	@Name
)

set @ID = scope_identity()
go
