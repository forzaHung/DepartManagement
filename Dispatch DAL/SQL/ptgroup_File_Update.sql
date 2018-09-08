use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_File_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_File_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Files table.
-- =============================================
create procedure [dbo].[ptgroup_File_Update]
(
	@Id bigint,
	@UserId int,
	@Name nvarchar(500),
	@MimeType nvarchar(250),
	@FileSize nvarchar(250),
	@IsDel bit,
	@Private bit,
	@UploadDate datetime
)

as

set nocount on

update [Files]
set [UserId] = @UserId,
	[Name] = @Name,
	[MimeType] = @MimeType,
	[FileSize] = @FileSize,
	[IsDel] = @IsDel,
	[Private] = @Private,
	[UploadDate] = @UploadDate
where [Id] = @Id
go
