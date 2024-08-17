namespace MeraStore.Shared.Common.Logging.Attributes;

public class EventCodeAttribute(string code): Attribute
{
    public string EventCode { get; } = code;
}
