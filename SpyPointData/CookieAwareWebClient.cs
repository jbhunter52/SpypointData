﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace SpyPointData
{
    public class CookieAwareWebClient : WebClient
    {
        public string Method;
        public CookieContainer CookieContainer { get; set; }
        public Uri Uri { get; set; }

        public CookieAwareWebClient()
            : this(new CookieContainer())
        {
        }

        public CookieAwareWebClient(CookieContainer cookies)
        {
            this.CookieContainer = cookies;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = this.CookieContainer;
                (request as HttpWebRequest).ServicePoint.Expect100Continue = false;
                (request as HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
                (request as HttpWebRequest).Accept = "application/json, text/plain, */*";
                (request as HttpWebRequest).Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.9");
                //(request as HttpWebRequest).Referer = "https://webapp.spypoint.com/login";
                (request as HttpWebRequest).KeepAlive = true;
                (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.Deflate |
                                                                     DecompressionMethods.GZip;

                if (Method == "POST")
                {
                    (request as HttpWebRequest).ContentType = "application/json; charset=utf-8";
                }

            }
            HttpWebRequest httpRequest = (HttpWebRequest)request;
            httpRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            return httpRequest;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            try
            {
                WebResponse response = base.GetWebResponse(request);
                String setCookieHeader = response.Headers[HttpResponseHeader.SetCookie];

                if (setCookieHeader != null)
                {
                    //do something if needed to parse out the cookie.
                    try
                    {
                        if (setCookieHeader != null)
                        {
                            Cookie cookie = new Cookie(); //create cookie
                            this.CookieContainer.Add(cookie);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                return response;
            }
            catch (WebException ex)
            {
                throw ex;
            }

        }
    }
}
