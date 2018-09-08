use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Department_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Department_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure delete a new record for Department table.
-- =============================================
create procedure [dbo].[ptgroup_Department_Delete]
(
	@Id int
)

as

set nocount on

delete from [Department]
where [Id] = @Id
go
