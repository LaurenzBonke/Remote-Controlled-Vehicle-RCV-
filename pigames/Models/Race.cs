using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class Race
{
    public int RaceId { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan? Starttime { get; set; }

    public TimeSpan? Endtime { get; set; }

    public int DisciplineId { get; set; }

    public virtual Discipline Discipline { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
