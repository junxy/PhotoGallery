using System;
using System.Collections.Generic;
using System.Net;
namespace System.Web.WebPages2
{
    public static class ResponseExtensions
    {
        public static void SetStatus(this HttpResponseBase response, HttpStatusCode httpStatusCode)
        {
            response.SetStatus((int)httpStatusCode);
        }
        public static void SetStatus(this HttpResponseBase response, int httpStatusCode)
        {
            response.StatusCode = httpStatusCode;
            response.End();
        }
        public static void WriteBinary(this HttpResponseBase response, byte[] data, string mimeType)
        {
            response.ContentType = mimeType;
            response.WriteBinary(data);
        }
        public static void WriteBinary(this HttpResponseBase response, byte[] data)
        {
            response.OutputStream.Write(data, 0, data.Length);
        }
        public static void OutputCache(this HttpResponseBase response, int numberOfSeconds, bool sliding = false, IEnumerable<string> varyByParams = null, IEnumerable<string> varyByHeaders = null, IEnumerable<string> varyByContentEncodings = null, HttpCacheability cacheability = HttpCacheability.Public)
        {
            HttpCachePolicyBase cache = response.Cache;
            HttpContext current = HttpContext.Current;
            cache.SetCacheability(cacheability);
            cache.SetExpires(current.Timestamp.AddSeconds((double)numberOfSeconds));
            cache.SetMaxAge(new TimeSpan(0, 0, numberOfSeconds));
            cache.SetValidUntilExpires(true);
            cache.SetLastModified(current.Timestamp);
            cache.SetSlidingExpiration(sliding);
            if (varyByParams != null)
            {
                foreach (string current2 in varyByParams)
                {
                    cache.VaryByParams[current2] = true;
                }
            }
            if (varyByHeaders != null)
            {
                foreach (string current3 in varyByHeaders)
                {
                    cache.VaryByHeaders[current3] = true;
                }
            }
            if (varyByContentEncodings != null)
            {
                foreach (string current4 in varyByContentEncodings)
                {
                    cache.VaryByContentEncodings[current4] = true;
                }
            }
        }
    }
}
