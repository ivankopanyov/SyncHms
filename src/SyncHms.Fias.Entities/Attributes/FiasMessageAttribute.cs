﻿namespace SyncHms.Fias.Entities.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal class FiasMessageAttribute(string indicator, FiasMessageDirections direction) : Attribute
{
    public string Indicator { get; private set; } = indicator;

    public FiasMessageDirections Direction { get; private set; } = direction;
}
