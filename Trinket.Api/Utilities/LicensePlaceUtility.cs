namespace Trinket.Api.Utilities
{
    public static class LicensePlateUtility
    {
        public static string NormalizeLicensePlate(string licensePlate)
        {
            return licensePlate
                        .ToLowerInvariant()
                        .Replace(" ", "")
                        .Replace("-", "")
                        .Trim();
        }
    }
}
