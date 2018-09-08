use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Role_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Role_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Role table.
-- =============================================
create procedure [dbo].[ptgroup_Role_Insert]
(
	@UserId int,
	@ModuleId int,
	@Add bit,
	@Edit bit,
	@View bit,
	@Delete bit,
	@Import bit,
	@Export bit,
	@Upload bit,
	@Publish bit,
	@Report bit,
	@Print bit
)

as

set nocount on

insert into [Role]
(
	[UserId],
	[ModuleId],
	[Add],
	[Edit],
	[View],
	[Delete],
	[Import],
	[Export],
	[Upload],
	[Publish],
	[Report],
	[Print]
)
values
(
	@UserId,
	@ModuleId,
	@Add,
	@Edit,
	@View,
	@Delete,
	@Import,
	@Export,
	@Upload,
	@Publish,
	@Report,
	@Print
)
go
