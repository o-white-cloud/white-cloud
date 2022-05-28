namespace white_cloud.web.Services
{
    public class UrlServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlServiceBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string BuildUrl(string path, object? queryParams = null)
        {
            var scheme = _httpContextAccessor.HttpContext?.Request.Scheme;
            var host = _httpContextAccessor.HttpContext?.Request.Host.Value;

            var query = queryParams == null ? "" : "?" + string.Join('&', queryParams.GetType().GetProperties().Select(p => $"{p.Name}={p.GetValue(queryParams)}"));
            return $"{scheme}://{host}/{path.Trim('/')}{query}";
        }
    }
}
