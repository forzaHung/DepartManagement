use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_File_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_File_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure view detail a new record for Files table.
-- =============================================
create procedure [dbo].[ptgroup_File_ViewDetail]
(
	@Id bigint
)

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
from [Files]
where [Id] = @Id
go
