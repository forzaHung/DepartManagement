use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Depart_Module_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Depart_Module_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure view detail a new record for Depart_Module table.
-- =============================================
create procedure [dbo].[ptgroup_Depart_Module_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[DepartId],
	[ModuleId]
from [Depart_Module]
where [Id] = @Id
go
