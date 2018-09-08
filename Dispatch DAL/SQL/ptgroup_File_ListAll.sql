use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_File_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_File_ListAll]
go

create procedure [dbo].[ptgroup_File_ListAll]

as

set nocount on

select  [Id],
	[UserId],
	[Name],
	[MimeType],
	[FileSize],
	[IsDel],
	[Private],
	[UploadDate]
from [Files] WITH (NOLOCK)
go
