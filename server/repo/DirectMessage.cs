using System;
using System.Collections.Generic;

namespace repo;

public partial class DirectMessage
{
    public Guid ChatId { get; set; }

    public Guid UserAId { get; set; }

    public Guid UserBId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<Message1> Message1s { get; set; } = new List<Message1>();

    public virtual User1 UserA { get; set; } = null!;

    public virtual User1 UserB { get; set; } = null!;
}
