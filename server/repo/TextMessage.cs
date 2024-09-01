using System;
using System.Collections.Generic;

namespace repo;

public partial class TextMessage
{
    public Guid TextMessageId { get; set; }

    public Guid MessageId { get; set; }

    public string Content { get; set; } = null!;

    public virtual Message1 Message { get; set; } = null!;
}
