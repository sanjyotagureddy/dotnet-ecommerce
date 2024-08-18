namespace MeraStore.Shared.Common.Logging.Masking.Strategies
{
  public class EmailMaskingStrategy : IMaskingStrategy
  {
    public string Mask(string input)
    {
      if (string.IsNullOrEmpty(input) || !input.Contains("@")) return input;
      var parts = input.Split('@');
      var username = parts[0];
      var domain = parts[1];

      var maskedUsername = username.Length > 2
          ? username[0] + new string('*', username.Length - 2) + username[^1]
          : new string('*', username.Length);

      var domainParts = domain.Split('.');
      var maskedDomain = domainParts[0][0] + new string('*', domainParts[0].Length - 1) + "." + string.Join('.', domainParts.Skip(1));

      return maskedUsername + "@" + maskedDomain;
    }
  }

}
