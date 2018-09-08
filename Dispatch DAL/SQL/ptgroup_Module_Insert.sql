use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Module_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Module_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure insert a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroup_Module_Insert]
(
	@Name nvarchar(250),
	@ID int OUTPUT
)

as

set nocount on

insert into [Module]
(
	[Name]
)
values
(
	@Name
)

set @ID = scope_identity()
go
