use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Permission_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Permission_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure delete a new record for Permission table.
-- =============================================
create procedure [dbo].[ptgroup_Permission_Delete]
(
	@Id int
)

as

set nocount on

delete from [Permission]
where [Id] = @Id
go
