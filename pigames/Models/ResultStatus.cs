using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class ResultStatus
{
    public int ResultStatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
