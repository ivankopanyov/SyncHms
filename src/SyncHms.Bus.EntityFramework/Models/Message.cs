﻿namespace SyncHms.Bus.EntityFramework.Models;

public class Message
{
    public string Id { get; set; }

    public string ExchangeName { get; set; }
    
    public string QueueName { get; set; }

    public DateTime DateTime { get; set; } = DateTime.Now;
    
    public bool Fast { get; set; }

    public string Json { get; set; }

    public virtual Queue Queue { get; set; }
}
