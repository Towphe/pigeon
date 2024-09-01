using System;
using System.Collections.Generic;

namespace repo;

public partial class Message1
{
    public Guid MessageId { get; set; }

    public Guid DirectMessageId { get; set; }

    public Guid SenderId { get; set; }

    public string Type { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual DirectMessage DirectMessage { get; set; } = null!;

    public virtual ICollection<ImageMessage> ImageMessages { get; set; } = new List<ImageMessage>();

    public virtual User1 Sender { get; set; } = null!;

    public virtual ICollection<TextMessage> TextMessages { get; set; } = new List<TextMessage>();
}
