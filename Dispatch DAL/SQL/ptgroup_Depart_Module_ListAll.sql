use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Depart_Module_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Depart_Module_ListAll]
go

create procedure [dbo].[ptgroup_Depart_Module_ListAll]

as

set nocount on

select  [Id],
	[DepartId],
	[ModuleId]
from [Depart_Module] WITH (NOLOCK)
go
