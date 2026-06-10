using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual Vehicle? Vehicle { get; set; }
}
