use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Role_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Role_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure delete a new record for Role table.
-- =============================================
create procedure [dbo].[ptgroup_Role_Delete]
(
	@UserId int,
	@ModuleId int
)

as

set nocount on

delete from [Role]
where [UserId] = @UserId
	and [ModuleId] = @ModuleId
go
