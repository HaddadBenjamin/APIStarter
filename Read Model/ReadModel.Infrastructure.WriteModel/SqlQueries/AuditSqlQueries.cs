namespace ReadModel.Infrastructure.WriteModel.SqlQueries
{
	public static class AuditSqlQueries
	{
		public static string SearchHttpRequests = @"
            --DECLARE @Id UNIQUEIDENTIFIER

            SELECT 
	            Id
	            ,QueryName
	            ,Query
	            ,QueryResultName
	            ,QueryResult
	            ,CorrelationId
	            ,Date
	            ,UserId
	            ,ImpersonatedUserId
            FROM AuditQueries
            WHERE @Id IS NULL OR @Id = Id";
	}
}