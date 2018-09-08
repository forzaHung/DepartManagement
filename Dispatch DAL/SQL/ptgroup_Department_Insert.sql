use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Department_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Department_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Department table.
-- =============================================
alter procedure [dbo].[ptgroup_Department_Insert]
(
	@Name nvarchar(2000),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@Location nvarchar(250),
	@EmployeeManagerId int,
	@EmployeeDebutyManagerId int,
	@EmployeeChiefOfTheOfficeId int,
	@Id int OUTPUT
)

as

set nocount on

insert into [Department]
(
	[Name],
	[CreatedDate],
	[ModifiedDate],
	[Location]
)
values
(
	@Name,
	@CreatedDate,
	@ModifiedDate,
	@Location,
	@EmployeeManagerId,
	@EmployeeDebutyManagerId,
	@EmployeeChiefOfTheOfficeId
)

set @Id = scope_identity()
go
