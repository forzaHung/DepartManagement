use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Permission_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Permission_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Permission table.
-- =============================================
create procedure [dbo].[ptgroup_Permission_Insert]
(
	@UserId int,
	@GroupId int,
	@Id int OUTPUT
)

as

set nocount on

insert into [Permission]
(
	[UserId],
	[GroupId]
)
values
(
	@UserId,
	@GroupId
)

set @Id = scope_identity()
go
