using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadModel.Domain.Exceptions;
using ReadModel.Domain.WriteModel.Readers;
using ReadModel.Domain.WriteModel.SqlConnections;
using ReadModel.Domain.WriteModel.Views;
using ReadModel.Infrastructure.WriteModel.SqlQueries;

namespace ReadModel.Infrastructure.WriteModel.Readers
{
    public class ItemReader : IItemReader
    {
        private readonly IWriteModelSqlConnection _sqlConnection;

        public ItemReader(IWriteModelSqlConnection sqlConnection) => _sqlConnection = sqlConnection;

        public async Task<IReadOnlyCollection<ItemView>> GetAllAsync() => await Search(new SearchParameters());

        public async Task<ItemView> GetByIdAsync(Guid id)
        {
            var itemView = (await Search(new SearchParameters { Id = id })).FirstOrDefault();

            if (itemView is null)
                throw new NotFoundException(nameof(ItemView));

            return itemView;
        }

        private async Task<IReadOnlyCollection<ItemView>> Search(SearchParameters searchParameters)
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
