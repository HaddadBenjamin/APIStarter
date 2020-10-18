using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadModel.Domain;
using ReadModel.Domain.Interfaces;

namespace ReadModel.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RefreshController : ControllerBase
    {
        private readonly IIndexRebuilder _indexRebuilder;
        private readonly IIndexRefresher _indexRefresher;

        public RefreshController(IIndexRebuilder indexRebuilder, IIndexRefresher indexRefresher)
        {
            _indexRebuilder = indexRebuilder;
            _indexRefresher = indexRefresher;
        }

        [HttpPost]
        [Route("indexes")]
        public async Task<IActionResult> RefreshAllIndexes()
        {
            await _indexRebuilder.RebuildAllIndexesAsync();
            await _indexRefresher.RefreshAllIndexesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("indexes/{indexType}")]
        public async Task<IActionResult> RefreshIndex([FromRoute] IndexType indexType)
        {
            await _indexRebuilder.RebuildIndexAsync(indexType);
            await _indexRefresher.RefreshIndexAsync(indexType);

            return Ok();
        }

        [HttpPost]
        [Route("indexes/{indexType}/{id:guid}")]
        public async Task<IActionResult> RefreshDocument([FromRoute] IndexType indexType, [FromRoute] Guid id)
        {
            await _indexRefresher.RefreshDocumentAsync(indexType, id);

            return Ok();
        }
    }
}
