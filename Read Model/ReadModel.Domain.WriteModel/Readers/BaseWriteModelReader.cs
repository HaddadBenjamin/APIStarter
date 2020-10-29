using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadModel.Domain.WriteModel.Readers
{
    public abstract class BaseWriteModelReader<TEntityView> : IWriteModelReader<TEntityView>
    {
        public async Task<IReadOnlyCollection<TEntityView>> GetAllAsync() => await Search(new SearchParameters());

        public async Task<TEntityView> GetByIdAsync(Guid id) => (await Search(new SearchParameters { Id = id })).FirstOrDefault();

        protected abstract Task<IReadOnlyCollection<TEntityView>> Search(SearchParameters searchParameters);
    }
}