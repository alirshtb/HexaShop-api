using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.InfrastructureContracts
{
    public interface ICookiesManager
    {
        string AddValue(Microsoft.AspNetCore.Http.HttpContext context, string key, string value);
        string GetValue(Microsoft.AspNetCore.Http.HttpContext context, string key);
        bool Contains(Microsoft.AspNetCore.Http.HttpContext context, string key);
        void Remove(Microsoft.AspNetCore.Http.HttpContext context, string key);
        CookieOptions GetDefaultOptions(Microsoft.AspNetCore.Http.HttpContext context);
        string GetBrowserId(Microsoft.AspNetCore.Http.HttpContext context);
    }
}
