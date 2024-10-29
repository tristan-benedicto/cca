using Assessment.Application.Calls.Queries;
using Assessment.Application.Stats.Queries;
using Assessment.Application.Stats.Commands;

namespace Assessment.Api.Controllers;

public class StatController : BaseApiController
{
    private readonly IMediator _mediator;

    public StatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<PaginatedResult<GetPaginatedStatsQueryRow>> GetPaginatedStatsAsync(
        [FromQuery] GetPaginatedStatsQuery query) =>
        await _mediator.Send(query);
    
    [HttpGet("list")]
    public async Task<Result<GetStatsListResult>> GetStatsListAsync([FromQuery] GetStatsListQuery query) =>
        await _mediator.Send(query);
    
    [HttpPost]
    public async Task<Result<CreateStatResult>> CreateStatAsync(
        [FromBody] CreateStatCommand command) =>
        await _mediator.Send(command);
}