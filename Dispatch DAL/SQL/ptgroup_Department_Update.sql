use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Department_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Department_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure update a new record for Department table.
-- =============================================
create procedure [dbo].[ptgroup_Department_Update]
(
	@Id int,
	@Name nvarchar(2000),
	@CreatedDate datetime,
	@ModifiedDate datetime
)

as

set nocount on

update [Department]
set [Name] = @Name,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
where [Id] = @Id
go
