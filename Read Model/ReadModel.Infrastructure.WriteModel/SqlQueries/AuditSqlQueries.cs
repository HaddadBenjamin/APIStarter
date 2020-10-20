namespace ReadModel.Infrastructure.WriteModel.SqlQueries
{
    public static class AuditSqlQueries
    {
        public static string SearchHttpRequests = @"
            --DECLARE @Id UNIQUEIDENTIFIER

            SELECT Id, HttpMethod, Uri, RequestHeaders, RequestBody, HttpStatus, ResponseBody, ClientApplication, Duration, CorrelationId, Date, UserId, ImpersonatedUserId
            FROM AuditRequests WITH (READUNCOMMITTED)
            WHERE @Id IS NULL OR @Id = Id";
    }
}