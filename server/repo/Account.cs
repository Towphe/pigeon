using System;
using System.Collections.Generic;

namespace repo;

public partial class Account
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Auth0Id { get; set; } = null!;

    public string? UserType { get; set; }

    public DateTime DateCreated { get; set; }
}
