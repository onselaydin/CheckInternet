using System;
using System.Net;


namespace CheckInternet
{
    class Program
    {
        static void Main(string[] args)
        {
            Exception exception;
            bool connection = IsConnected("http://www.bing.com", out exception
                , CredentialCache: new NetworkCredential
                {
                    Domain = "",
                    UserName = "",
                    Password = ""
                }
                );
            Console.WriteLine("Internet {0}\n{1}",
                connection ? "connected" : "!connected!",
                exception != null ? exception.Message : String.Empty
            );
        }

        static bool IsConnected(string webAddress,out Exception exception
            ,NetworkCredential CredentialCache = null)
        {
            bool result = false;
            exception = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webAddress);
                if (CredentialCache != null)
                    request.Proxy.Credentials = CredentialCache;
                request.Method = "HEAD";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
                result = true;
            }
            catch(Exception excp)
            {
                exception = excp;
            }
            return result;
        }
    }
}
