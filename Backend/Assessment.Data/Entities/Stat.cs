using Assessment.Data.Interfaces;

namespace Assessment.Data.Entities;

public class Stat : IId, ISoftDelete
{
    public Guid Id { get; set; }
    public DateTime Hour { get; set; }
    public int CallCount { get; set; }
    public string TopUser { get; set; }

    public DateTime? DateDeleted { get; set; }
    
    public virtual ICollection<Call> Calls { get; set; }
}