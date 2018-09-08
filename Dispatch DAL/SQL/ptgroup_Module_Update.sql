use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Module_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Module_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroup_Module_Update]
(
	@ID int,
	@Name nvarchar(250)
)

as

set nocount on

update [Module]
set [Name] = @Name
where [ID] = @ID
go
