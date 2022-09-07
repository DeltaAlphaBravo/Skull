using Microsoft.AspNetCore.Mvc;

namespace Skull.Api.Controllers;

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
    public async Task<ActionResult<string>> StartTableAsync()
    {
        var table = new Table("Booyah");
        await _tableRepository.SaveTableAsync(table);
        return table.Name;
    }

    [HttpPost]
    [Route("{tableName}/players")]
    public async Task<ActionResult<int>> AddPlayerAsync([FromRoute]string tableName, [FromBody]string name)
    {
        if (string.IsNullOrEmpty(name)) return new BadRequestObjectResult(nameof(name));
        var foundTable = await _tableRepository.GetTableAsync(name);
        if (foundTable == null) return new NotFoundResult();
        var playerId = foundTable.JoinPlayer(name);
        await _skullHub.NotifyNewPlayer(tableName, name, playerId);
        return new OkObjectResult(playerId);
    }

    [HttpGet]
    [Route("{tableName}")]
    public async Task<ActionResult<ITableView>> GetTable([FromRoute]string tableName)
    {
        if (string.IsNullOrEmpty(tableName)) return new BadRequestObjectResult(nameof(tableName));
        var foundTable = await _tableRepository.GetTableAsync(tableName);
        if (foundTable == null) return new NotFoundResult();
        return new OkObjectResult(new TableView(foundTable));
    }
}
