use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Employee_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Employee_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure delete a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroup_Employee_Delete]
(
	@Id int
)

as

set nocount on

delete from [Employee]
where [Id] = @Id
go
