use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Depart_Module_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Depart_Module_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Depart_Module table.
-- =============================================
create procedure [dbo].[ptgroup_Depart_Module_Update]
(
	@Id int,
	@DepartId int,
	@ModuleId int
)

as

set nocount on

update [Depart_Module]
set [DepartId] = @DepartId,
	[ModuleId] = @ModuleId
where [Id] = @Id
go
