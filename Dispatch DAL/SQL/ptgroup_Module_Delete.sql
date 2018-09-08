use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Module_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Module_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure delete a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroup_Module_Delete]
(
	@ID int
)

as

set nocount on

delete from [Module]
where [ID] = @ID
go
