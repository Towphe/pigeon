using System;
using System.Collections.Generic;

namespace repo;

public partial class Migration
{
    public string Version { get; set; } = null!;

    public DateTime InsertedAt { get; set; }
}
