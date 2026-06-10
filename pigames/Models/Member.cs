using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public int UserId { get; set; }

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
