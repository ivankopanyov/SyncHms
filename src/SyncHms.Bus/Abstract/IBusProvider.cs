﻿namespace SyncHms.Bus;

public interface IBusProvider
{
    Task PublishAsync<TMessage>(TMessage message);

    void Subscribe<TMessage, TConsumer>(Func<TMessage, IMessageContext, Task> handleMessage);
}

public interface IBusProvider<TOptions> : IBusProvider where TOptions : class, new()
{
}
