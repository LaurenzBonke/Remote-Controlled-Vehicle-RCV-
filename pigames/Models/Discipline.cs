using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class Discipline
{
    public int DisciplineId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
