use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Department_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Department_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure view detail a new record for Department table.
-- =============================================
create procedure [dbo].[ptgroup_Department_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[Name],
	[CreatedDate],
	[ModifiedDate]
from [Department]
where [Id] = @Id
go
