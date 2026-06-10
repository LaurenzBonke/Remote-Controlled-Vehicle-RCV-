using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string Name { get; set; } = null!;

    public string? Color { get; set; }

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<VehicleExtension> VehicleExtensions { get; set; } = new List<VehicleExtension>();
}
