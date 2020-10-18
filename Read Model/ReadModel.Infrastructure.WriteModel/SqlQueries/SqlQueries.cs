﻿namespace ReadModel.Infrastructure.WriteModel
{
    public static class WriteModelSqlQueries
    {
        public static string SearchItems = @"
            --DECLARE @Id UNIQUEIDENTIFIER

            DROP TABLE IF EXISTS  #TempItems

            SELECT Id, Name
            INTO #TempItems
            FROM Items
            WHERE @Id IS NULL OR @Id = Id

            ALTER TABLE #TempItems ADD PRIMARY KEY (ID);

            SELECT * FROM #TempItems
            SELECT il.Id, il.ItemId, il.Name 
            FROM ItemLocation il
            INNER JOIN #TempItems ti ON il.ItemId = ti.Id";
    }
}