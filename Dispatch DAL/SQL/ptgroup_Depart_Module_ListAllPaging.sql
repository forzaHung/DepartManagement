use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Depart_Module_ListAllPaging]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Depart_Module_ListAllPaging]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure select all record with paging for Depart_Module table.
-- =============================================
create procedure [dbo].[ptgroup_Depart_Module_ListAllPaging]

(
@pageIndex int,
@pageSize int,
@totalRow int output
)

as

set nocount on

DECLARE @UpperBand int, @LowerBand int

SELECT @totalRow = COUNT(*) FROM Depart_Module

SET @LowerBand  = (@pageIndex - 1) * @PageSize
SET @UpperBand  = (@pageIndex * @PageSize)

SELECT  [Id],
	[DepartId],
	[ModuleId]
 FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowNumber FROM Depart_Module WITH (NOLOCK)) AS temp
WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
go
