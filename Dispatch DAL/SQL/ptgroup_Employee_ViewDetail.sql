use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Employee_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Employee_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure view detail a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroup_Employee_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[DepartmentId],
	[FirstName],
	[LastName],
	[Address],
	[Phone],
	[PictureId],
	[IsActive],
	[IsDel],
	[CreatedDate],
	[ModifiedDate]
from [Employee]
where [Id] = @Id
go
