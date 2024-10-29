using Assessment.Data;
using Assessment.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Application.Stats.Queries;

public class GetStatsListQuery : IRequest<Result<GetStatsListResult>> { }

public class GetStatsListResult
{
    public List<ListItem> Items { get; set; }
}

public class ListItem
{
    public Guid Id { get; set; }
    
    public string Value { get; set; }
}

public class GetStatsListQueryHandler : IRequestHandler<GetStatsListQuery, Result<GetStatsListResult>>
{
    private readonly ApplicationDbContext _context;

    public GetStatsListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetStatsListResult>> Handle(GetStatsListQuery request, CancellationToken cancellationToken)
    {
        var result = new GetStatsListResult
        {
            Items = await _context
                .Stats
                .Select(x => new ListItem
                {
                    Id = x.Id,
                    Value = x.TopUser,
                })
                .ToListAsync(cancellationToken: cancellationToken)
        };

        return Result<GetStatsListResult>.Success(result);
    }
}