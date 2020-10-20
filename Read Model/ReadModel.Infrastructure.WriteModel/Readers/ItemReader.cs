using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadModel.Domain.WriteModel.Readers;
using ReadModel.Domain.WriteModel.SqlConnections;
using ReadModel.Domain.WriteModel.Views;
using ReadModel.Infrastructure.WriteModel.SqlQueries;

namespace ReadModel.Infrastructure.WriteModel.Readers
{
    public class ItemReader : BaseWriteModelReader<ItemView>
    {
        private readonly IWriteModelSqlConnection _sqlConnection;

        public ItemReader(IWriteModelSqlConnection sqlConnection) => _sqlConnection = sqlConnection;

        protected override async Task<IReadOnlyCollection<ItemView>> Search(SearchParameters searchParameters)
        {
            await using var sqlConnection = _sqlConnection.CreateConnection();
            using var queryMultiple = await sqlConnection.QueryMultipleAsync(WriteModelSqlQueries.SearchItems, searchParameters);

            var itemsViews = (await queryMultiple.ReadAsync<ItemView>()).ToList();
            var itemLocationsViews = (await queryMultiple.ReadAsync<ItemLocationView>()).ToLookup(_ => _.ItemId);

            foreach (var itemsView in itemsViews)
                itemsView.Locations = itemLocationsViews[itemsView.Id].ToList();

            return itemsViews;
        }
    }
}
