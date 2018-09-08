use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Module_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Module_ListAll]
go

create procedure [dbo].[ptgroup_Module_ListAll]

as

set nocount on

select  [ID],
	[Name]
from [Module] WITH (NOLOCK)
go
