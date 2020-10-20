using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadModel.Domain.WriteModel.Exceptions;

namespace ReadModel.Domain.WriteModel.Readers
{
    public abstract class BaseWriteModelReader<TEntityView> : IWriteModelReader<TEntityView>
    {
        public async Task<IReadOnlyCollection<TEntityView>> GetAllAsync() => await Search(new SearchParameters());

        public async Task<TEntityView> GetByIdAsync(Guid id)
        {
            var httpRequestView = (await Search(new SearchParameters { Id = id })).FirstOrDefault();

            if (httpRequestView is null)
                throw new NotFoundException(nameof(TEntityView));

            return httpRequestView;
        }

        protected abstract Task<IReadOnlyCollection<TEntityView>> Search(SearchParameters searchParameters);
    }
}