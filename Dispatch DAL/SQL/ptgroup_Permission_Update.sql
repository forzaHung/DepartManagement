use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Permission_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Permission_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Permission table.
-- =============================================
create procedure [dbo].[ptgroup_Permission_Update]
(
	@Id int,
	@UserId int,
	@GroupId int
)

as

set nocount on

update [Permission]
set [UserId] = @UserId,
	[GroupId] = @GroupId
where [Id] = @Id
go
