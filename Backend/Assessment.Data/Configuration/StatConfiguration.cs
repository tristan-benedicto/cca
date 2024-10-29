using Assessment.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Data.Configuration;

public class StatConfiguration : BaseConfiguration<Stat>
{
    public override void Configure(EntityTypeBuilder<Stat> builder)
    {
        builder
            .Property(x => x.TopUser)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(x => x.CallCount)
            .IsRequired();

        builder
            .Property(x => x.Hour)
            .IsRequired();

        builder
            .HasMany<Call>(x => x.Calls);

        builder
            .HasData(new List<Stat>
            {
                new()
                {
                    Id = Guid.Parse("cc9a2cee-5b63-49a5-8f78-1867c4d34ad7"),
                    CallCount = 0,
                    Hour = DateTime.Now,
                    TopUser = "Test User",
                },
            }); ;
        
        base.Configure(builder);
    }
}