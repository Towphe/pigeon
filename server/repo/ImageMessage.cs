using System;
using System.Collections.Generic;

namespace repo;

public partial class ImageMessage
{
    public Guid ImageMessageId { get; set; }

    public Guid MessageId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public virtual Message1 Message { get; set; } = null!;
}
