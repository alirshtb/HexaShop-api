using HexaShop.Application.Constracts.InfrastructureContracts;
using HexaShop.Common;
using Microsoft.AspNetCore.Http;

namespace HexaShop.Infrastructure.Repositories
{
    public class CookiesManager : ICookiesManager
    {
        public string AddValue(HttpContext context, string key, string value)
        {
            context.Response.Cookies.Append(key, value);
            return $"{key} - {value}";
        }

        public bool Contains(HttpContext context, string key)
        {
            return context.Request.Cookies.ContainsKey(key);
        }

        /// <summary>
        /// get browser id if exists if not exists create and return id.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetBrowserId(HttpContext context)
        {
            var browserId = GetValue(context, ConstantValues.BrowserId);
            if(browserId == null)
            {
                var value = Guid.NewGuid().ToString();
                AddValue(context, ConstantValues.BrowserId, value);
                browserId = value;
            }
            return browserId;
        }

        public CookieOptions GetDefaultOptions(HttpContext context)
        {
            return new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(30),
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps
            };
        }

        public string GetValue(HttpContext context, string key)
        {
            string? cookieValue;
            var result = context.Request.Cookies.TryGetValue(key, out cookieValue) == true ? cookieValue : null;
            return cookieValue;
        }

        public void Remove(HttpContext context, string key)
        {
            if(context.Request.Cookies.ContainsKey(key))
            {
                context.Response.Cookies.Delete(key);
            }
        }
    }
}
