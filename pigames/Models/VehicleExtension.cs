using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class VehicleExtension
{
    public int ExtensionId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int VehicleId { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
