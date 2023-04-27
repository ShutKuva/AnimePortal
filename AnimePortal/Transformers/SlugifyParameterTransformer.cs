using static System.Text.RegularExpressions.Regex;

namespace AnimePortalAuthServer.Transformers
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            return value != null ? Replace(value.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower() : null;
        }
    }
}
