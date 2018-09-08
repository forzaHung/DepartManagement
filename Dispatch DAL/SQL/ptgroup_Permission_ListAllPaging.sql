use [Dispatch]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroup_Permission_ListAllPaging]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroup_Permission_ListAllPaging]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 2/16/2017
-- Description:	The procedure select all record with paging for Permission table.
-- =============================================
create procedure [dbo].[ptgroup_Permission_ListAllPaging]

(
@pageIndex int,
@pageSize int,
@totalRow int output
)

as

set nocount on

DECLARE @UpperBand int, @LowerBand int

SELECT @totalRow = COUNT(*) FROM Permission

SET @LowerBand  = (@pageIndex - 1) * @PageSize
SET @UpperBand  = (@pageIndex * @PageSize)

SELECT  [Id],
	[UserId],
	[GroupId]
 FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowNumber FROM Permission WITH (NOLOCK)) AS temp
WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
go
