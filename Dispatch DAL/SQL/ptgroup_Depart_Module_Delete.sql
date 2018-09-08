use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Depart_Module_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Depart_Module_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure delete a new record for Depart_Module table.
-- =============================================
create procedure [dbo].[ptgroup_Depart_Module_Delete]
(
	@Id int
)

as

set nocount on

delete from [Depart_Module]
where [Id] = @Id
go
