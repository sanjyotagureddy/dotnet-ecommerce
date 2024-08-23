using System.Net;
using System.Net.Sockets;
using Serilog.Core;
using Serilog.Events;

namespace MeraStore.Shared.Common.Logging.Enrichers;

public class IpAddressEnricher() : ILogEventEnricher
{
  private string _ipAddress;
  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (string.IsNullOrEmpty(_ipAddress))
    {
      _ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "";
    }
    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("IpAddress", _ipAddress));
  }
}