using MeraStore.Shared.Common.Logging.Masking.Strategies;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace MeraStore.Shared.Common.Logging.Masking;

public class MaskingService(IConfiguration configuration)
{
  private readonly Dictionary<string, IMaskingStrategy> _strategies = new()
  {
    { "CreditCard", new CreditCardMaskingStrategy() },
    { "Email", new EmailMaskingStrategy() },
    { "PhoneNumber", new PhoneNumberMaskingStrategy() },
    { "Default", new DefaultMaskingStrategy() }
  };
  private readonly Dictionary<string, List<string>> _fieldStrategyMap = configuration.GetSection("MaskingConfig").Get<Dictionary<string, List<string>>>();

  public string MaskSensitiveData(string jsonContent)
  {
    if (string.IsNullOrEmpty(jsonContent)) return jsonContent;

    var jsonObject = JObject.Parse(jsonContent);

    foreach (var strategyEntry in _fieldStrategyMap)
    {
      var strategyName = strategyEntry.Key;
      var fields = strategyEntry.Value;

      if (!_strategies.ContainsKey(strategyName)) continue;

      foreach (var fieldPath in fields)
      {
        var tokens = jsonObject.SelectTokens(fieldPath, errorWhenNoMatch: false).ToList();
        foreach (var token in tokens)
        {
          if (token.Type == JTokenType.String)
          {
            var originalValue = token.ToString();
            var maskedValue = _strategies[strategyName].Mask(originalValue);
            token.Replace(maskedValue);
          }
        }
      }
    }

    return jsonObject.ToString();
  }
}
