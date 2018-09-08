use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Role_Group_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Role_Group_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Role_Group table.
-- =============================================
create procedure [dbo].[ptgroup_Role_Group_Update]
(
	@ID int,
	@Name nvarchar(50)
)

as

set nocount on

update [Role_Group]
set [Name] = @Name
where [ID] = @ID
go
