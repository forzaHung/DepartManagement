use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Depart_Module_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Depart_Module_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Depart_Module table.
-- =============================================
create procedure [dbo].[ptgroup_Depart_Module_Insert]
(
	@DepartId int,
	@ModuleId int,
	@Id int OUTPUT
)

as

set nocount on

insert into [Depart_Module]
(
	[DepartId],
	[ModuleId]
)
values
(
	@DepartId,
	@ModuleId
)

set @Id = scope_identity()
go
