namespace ReadModel.Infrastructure.WriteModel.SqlQueries
{
    public static class WriteModelSqlQueries
    {
        public static string SearchItems = @"
            --DECLARE @Id UNIQUEIDENTIFIER

            SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
            
            DROP TABLE IF EXISTS  #TempItems

            SELECT Id, Name
            INTO #TempItems
            FROM Items
            WHERE (@Id IS NULL OR @Id = Id) AND IsActive = 1 

            ALTER TABLE #TempItems ADD PRIMARY KEY (ID);

            SELECT * FROM #TempItems
            SELECT il.Id, il.ItemId, il.Name 
            FROM ItemLocation il
            INNER JOIN #TempItems ti ON il.ItemId = ti.Id";
    }
}