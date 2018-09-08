use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Module_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Module_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure view detail a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroup_Module_ViewDetail]
(
	@ID int
)

as

set nocount on

select  [ID],
	[Name]
from [Module]
where [ID] = @ID
go
