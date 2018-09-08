use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Employee_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Employee_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroup_Employee_Insert]
(
	@DepartmentId int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Address nvarchar(350),
	@Phone varchar(20),
	@PictureId bigint,
	@IsActive bit,
	@IsDel bit,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@Id int OUTPUT
)

as

set nocount on

insert into [Employee]
(
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
)
values
(
	@DepartmentId,
	@FirstName,
	@LastName,
	@Address,
	@Phone,
	@PictureId,
	@IsActive,
	@IsDel,
	@CreatedDate,
	@ModifiedDate
)

set @Id = scope_identity()
go
