use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Employee_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Employee_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroup_Employee_Update]
(
	@Id int,
	@DepartmentId int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Address nvarchar(350),
	@Phone varchar(20),
	@PictureId bigint,
	@IsActive bit,
	@IsDel bit,
	@CreatedDate datetime,
	@ModifiedDate datetime
)

as

set nocount on

update [Employee]
set [DepartmentId] = @DepartmentId,
	[FirstName] = @FirstName,
	[LastName] = @LastName,
	[Address] = @Address,
	[Phone] = @Phone,
	[PictureId] = @PictureId,
	[IsActive] = @IsActive,
	[IsDel] = @IsDel,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
where [Id] = @Id
go
