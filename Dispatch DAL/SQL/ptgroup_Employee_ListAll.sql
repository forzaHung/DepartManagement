use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Employee_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Employee_ListAll]
go

create procedure [dbo].[ptgroup_Employee_ListAll]

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
from [Employee] WITH (NOLOCK)
go
