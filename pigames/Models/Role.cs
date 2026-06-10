using System;
using System.Collections.Generic;

namespace pigames.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? RoleDescription { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
