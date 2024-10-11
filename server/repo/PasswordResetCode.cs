using System;
using System.Collections.Generic;

namespace repo;

public partial class PasswordResetCode
{
    public Guid CodeId { get; set; }

    public string Code { get; set; } = null!;

    public DateTime DateCreated { get; set; }
}
