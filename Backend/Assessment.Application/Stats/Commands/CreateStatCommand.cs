using Assessment.Data;
using Assessment.Data.Entities;
using Assessment.Shared.Models;
using FluentValidation;
using MediatR;

namespace Assessment.Application.Stats.Commands;

public class CreateStatCommand : IRequest<Result<CreateStatResult>>
{
    public int CallCount{ get; set; }
    public DateTime Hour { get; set; }
    public string TopUser { get; set; }
}

public class CreateStatResult
{
    public Guid Id { get; set; }
}

public class CreateStatCommandValidator : AbstractValidator<CreateStatCommand>
{
    public CreateStatCommandValidator()
    {
        RuleFor(x => x.TopUser)
            .NotEmpty()
            .WithMessage("Top User is required");
        
        RuleFor(x => x.TopUser)
            .MaximumLength(255)
            .WithMessage("Top User must not exceed 255 characters");
    }
}

public class CreateStatCommandHandler : IRequestHandler<CreateStatCommand, Result<CreateStatResult>>
{
    private readonly ApplicationDbContext _context;

    public CreateStatCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateStatResult>> Handle(CreateStatCommand request, CancellationToken cancellationToken)
    {
        var stat = new Stat
        {
            TopUser = request.TopUser,
        };

        _context.Stats.Add(stat);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<CreateStatResult>.Success(new CreateStatResult
        {
            Id = stat.Id,
        });
    }
}
