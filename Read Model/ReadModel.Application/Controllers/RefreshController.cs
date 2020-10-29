using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Indexes;

namespace ReadModel.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RefreshController : ControllerBase
    {
        private readonly IIndexRebuilder _indexRebuilder;
        private readonly IIndexRefresher _indexRefresher;
        private readonly IAliasSwapper _aliasSwapper;

        public RefreshController(IIndexRebuilder indexRebuilder, IIndexRefresher indexRefresher, IAliasSwapper aliasSwapper)
        {
            _indexRebuilder = indexRebuilder;
            _indexRefresher = indexRefresher;
            _aliasSwapper = aliasSwapper;
        }

        [HttpGet]
        [Route("indexes")]
        [ApiExplorerSettings(IgnoreApi = true)]
        // Sans un refresh au lancement de l'application, le endponit indexes/{indexType}/{id:guid} ne fonctionne pas. 
        public async Task<IActionResult> RefreshAllIndexesAtFirstLaunch() => await RefreshAllIndexes();

        [HttpPost]
        [Route("indexes")]
        public async Task<IActionResult> RefreshAllIndexes()
        {
            await _indexRebuilder.RebuildAllIndexesAsync();
            await _indexRefresher.RefreshAllIndexesAsync();
            await _aliasSwapper.SwapAllIndexesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("indexes/{indexType}")]
        public async Task<IActionResult> RefreshIndex([FromRoute] IndexType indexType)
        {
            await _indexRebuilder.RebuildIndexAsync(indexType);
            await _indexRefresher.RefreshIndexAsync(indexType);
            await _aliasSwapper.SwapIndexAsync(indexType);

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
