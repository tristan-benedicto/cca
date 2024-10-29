using Assessment.Data;
using Assessment.Data.Entities;
using Assessment.Shared.Extensions;
using Assessment.Shared.Models;
using MediatR;

namespace Assessment.Application.Stats.Queries;

public class GetPaginatedStatsQuery : PaginatedRequest<GetPaginatedStatsQueryRow> { }

public class GetPaginatedStatsQueryRow
{ 
    public Guid Id { get; set; }
    public DateTime Hour { get; set; }
    public int CallCount { get; set; }
    public string TopUser { get; set; }
}

public class GetPaginatedStatsQueryHandler : IRequestHandler<GetPaginatedStatsQuery, PaginatedResult<GetPaginatedStatsQueryRow>>
{
    private readonly ApplicationDbContext _context;

    public GetPaginatedStatsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<GetPaginatedStatsQueryRow>> Handle(GetPaginatedStatsQuery request, CancellationToken cancellationToken)
    {
        var isDescending = request.SortDirection == SortDirection.Descending;

        if (string.IsNullOrWhiteSpace(request.SortBy))
        {
            isDescending = true;
            request.SortBy = nameof(Stat.CallCount);
            request.SortBy2 = nameof(Stat.Hour);
        }
        
        var query = _context.Stats
            .Select(x => new GetPaginatedStatsQueryRow
            {
                Id = x.Id,
                Hour = x.Hour,
                CallCount = x.CallCount,
                TopUser = x.TopUser,
            })
            .IfNotEmptyThenWhere(request.SearchValue,
                x => x.TopUser.Contains(request.SearchValue!.Trim()))
            .OrderBy(request.SortBy, isDescending)
            .OrderBy(request.SortBy2, isDescending);

        return await query.PaginateAsync(request);
    }
}
