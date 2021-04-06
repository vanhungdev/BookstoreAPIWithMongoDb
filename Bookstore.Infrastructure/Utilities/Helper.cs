using Bookstore.Infrastructure.Configuration;
using Bookstore.Infrastructure.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Bookstore.Infrastructure.Utilities
{
    public static partial class Helper
    {
        public static AppSettings Settings => AppSettingServices.Get;
        public static T CallAPI<T>(string url, APIMethod method, object objReq, CallAPIOption option = null) where T : class
        {
            var log = new StringBuilder();
            log.AppendLine(string.Concat($"Before call api url: {url}", Environment.NewLine, $"input: {JsonConvert.SerializeObject(objReq)}"));
            T result = null;
            try
            {
                if (APIMethod.GET.Equals(method) && objReq != null)
                {
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(objReq));
                    string queryString = string.Join("&", dictionary.Select(x => string.Format("{0}={1}", x.Key, HttpUtility.UrlEncode(x.Value))));
                    url += string.Format("?{0}", queryString);
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method.ToString();
                request.KeepAlive = false;
                request.ContentType = "application/json; charset=UTF-8";

                if (option != null)
                {
                    if (option.Timeout > 0)
                    {
                        request.Timeout = option.Timeout;
                    }

                    if (option.Headers.Keys.Any())
                    {
                        foreach (var item in option.Headers)
                        {
                            request.Headers.Add(item.Key, item.Value);
                        }
                    }

                    if (option.IsInternal)
                    {
                        request.Headers.Add("HEADER_AUTHORIZATION", "your token");    
                    }
                }

                if (APIMethod.POST.Equals(method))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objReq));
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                WebResponse response = request.GetResponse();
                HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;

                if (statusCode.Equals(HttpStatusCode.OK))
                {
                    string responseString = string.Empty;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = streamReader.ReadToEnd();
                    }
                    result = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    log.AppendLine($"ResponseCode: {statusCode}");
                    string responseString = string.Empty;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = streamReader.ReadToEnd();
                    }
                    log.AppendLine($"Response: {JsonConvert.DeserializeObject<dynamic>(responseString)}");
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex.ToString()}");
            }
            finally
            {
                // TODO: write log
                //LoggingHelper.SetLogStep(log.ToString());
            }
            return result;
        }

        public class CallAPIOption
        {
            public CallAPIOption()
            {
                Headers = new Dictionary<string, string>();
                Timeout = 0;
            }
            public Dictionary<string, string> Headers { get; set; }
            public int Timeout { get; set; }
            public bool IsInternal { get; set; }
        }
    }
}
