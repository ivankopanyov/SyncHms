﻿namespace SyncHms.Fias.Entities;

public partial class FiasLinkStart : FiasMessageToPms
{
    /// <summary>
    /// <b>Required..</b><br/>
    /// <b>Обязательно.</b>
    /// </summary>
    [Required]
    public DateTime DateTime { get; set; }
}
