namespace MeraStore.Shared.Common.Logging.Masking.Strategies;

public class PhoneNumberMaskingStrategy : IMaskingStrategy
{
  public string Mask(string input)
  {
    if (string.IsNullOrEmpty(input) || input.Length < 4) return input;

    return new string('*', input.Length - 4) + input[^4..];
  }
}
