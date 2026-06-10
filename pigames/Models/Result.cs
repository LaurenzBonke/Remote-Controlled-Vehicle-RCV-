using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int? Placement { get; set; }

    public int? PenaltySeconds { get; set; }

    public decimal? TotalTime { get; set; }

    public int RaceId { get; set; }

    public int TeamId { get; set; }

    public int ResultStatusId { get; set; }

    public virtual Race Race { get; set; } = null!;

    public virtual ResultStatus ResultStatus { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
