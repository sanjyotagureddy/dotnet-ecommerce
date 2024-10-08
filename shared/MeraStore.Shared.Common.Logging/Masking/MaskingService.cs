﻿using MeraStore.Shared.Common.Logging.Masking.Strategies;
using Newtonsoft.Json.Linq;

namespace MeraStore.Shared.Common.Logging.Masking;

public class MaskingService(Dictionary<string, List<string>> fieldStrategyMap)
{
  private readonly Dictionary<string, IMaskingStrategy> _strategies = new()
  {
    { "CreditCard", new CreditCardMaskingStrategy() },
    { "Email", new EmailMaskingStrategy() },
    { "PhoneNumber", new PhoneNumberMaskingStrategy() },
    { "Default", new DefaultMaskingStrategy() }
  };

  public string MaskSensitiveData(string jsonContent)
  {
    if (string.IsNullOrEmpty(jsonContent)) return jsonContent;

    var jsonObject = JObject.Parse(jsonContent);

    foreach (var strategyEntry in fieldStrategyMap)
    {
      var strategyName = strategyEntry.Key;
      var fields = strategyEntry.Value;

      if (!_strategies.TryGetValue(strategyName, out var strategy)) continue;

      foreach (var fieldPath in fields)
      {
        var tokens = jsonObject.SelectTokens(fieldPath, errorWhenNoMatch: false).ToList();
        foreach (var token in tokens)
        {
          if (token.Type == JTokenType.String)
          {
            var originalValue = token.ToString();
            var maskedValue = strategy.Mask(originalValue);
            token.Replace(maskedValue);
          }
        }
      }
    }

    return jsonObject.ToString();
  }
}
