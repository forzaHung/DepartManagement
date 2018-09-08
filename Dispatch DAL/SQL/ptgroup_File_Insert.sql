use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_File_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_File_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Files table.
-- =============================================
create procedure [dbo].[ptgroup_File_Insert]
(
	@UserId int,
	@Name nvarchar(500),
	@MimeType nvarchar(250),
	@FileSize nvarchar(250),
	@IsDel bit,
	@Private bit,
	@UploadDate datetime,
	@Id bigint OUTPUT
)

as

set nocount on

insert into [Files]
(
	[UserId],
	[Name],
	[MimeType],
	[FileSize],
	[IsDel],
	[Private],
	[UploadDate]
)
values
(
	@UserId,
	@Name,
	@MimeType,
	@FileSize,
	@IsDel,
	@Private,
	@UploadDate
)

set @Id = scope_identity()
go
