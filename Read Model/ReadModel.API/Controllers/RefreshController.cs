using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadModel.ElasticSearch;

namespace ReadModel.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RefreshController : ControllerBase
    {
        private readonly IIndexRebuilder _indexRebuilder;

        public RefreshController(IIndexRebuilder indexRebuilder) => _indexRebuilder = indexRebuilder;

        [HttpPost]
        [Route("indexes")]
        public async Task<IActionResult> RefreshAllIndexes()
        {
            await _indexRebuilder.RebuildAllAsync();
            // remettre toutes les données de tout les indexes.

            return Ok();
        }

        [HttpPost]
        [Route("indexes/{indexType}")]
        public async Task<IActionResult> RefreshIndex([FromRoute] IndexType indexType)
        {
            await _indexRebuilder.RebuildAsync(indexType);
            // remettre toutes les données de l'index correspondant.

            return Ok();
        }

        [HttpPost]
        [Route("indexes/{indexType}/{id:guid}")]
        public async Task<IActionResult> RefreshDocument([FromRoute] IndexType indexType, [FromRoute] Guid id)
        {
            // await delete(type/id), await refresh(type/id)
            return Ok();
        }
    }
}
