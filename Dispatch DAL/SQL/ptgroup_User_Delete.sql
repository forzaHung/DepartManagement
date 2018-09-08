use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_User_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_User_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure delete a new record for Users table.
-- =============================================
create procedure [dbo].[ptgroup_User_Delete]
(
	@Id int
)

as

set nocount on

delete from [Users]
where [Id] = @Id
go
