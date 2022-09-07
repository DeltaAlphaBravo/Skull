using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skull.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;
        private readonly ISkullHub _skullHub;

        public TableController(ITableRepository tableRepository, ISkullHub skullHub)
        {
            _tableRepository = tableRepository;
            _skullHub = skullHub;
        }

        [HttpPost]
        [Route("api/table")]
        public async Task<ActionResult<string>> StartTableAsync()
        {
            var table = new Table("Booyah");
            await _tableRepository.SaveTableAsync(table);
            return table.Name;
        }

        [HttpPost]
        [Route("api/table/{tableName}/players")]
        public async Task<ActionResult<int>> AddPlayerAsync([FromRoute]string tableName, [FromBody]string name)
        {
            if (string.IsNullOrEmpty(name)) return new BadRequestObjectResult(nameof(name));
            var foundTable = await _tableRepository.GetTableAsync(name);
            if (foundTable == null) return new NotFoundResult();
            var playerId = foundTable.JoinPlayer(name);
            await _skullHub.SendMessageAsync(tableName, $"{name} joined \"{tableName}\" as player {playerId}");
            return playerId;
        }
    }
}
