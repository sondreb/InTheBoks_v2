namespace InTheBoks.Web
{
    using System;
    using System.Web;

    public class AppCacheHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.Cache.SetExpires(DateTime.Now.AddYears(-1));
            response.ContentType = "text/cache-manifest";

            response.Write("CACHE MANIFEST\r\n");

#if DEBUG
            response.Write(string.Format("# This file was generated at {0} {1}\r", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
#endif

            response.Write("\nCACHE:\r");

            response.Write(string.Format("{0}\r", "Content/themes/ui-darkness/jquery-ui.css"));
            response.Write(string.Format("{0}\r", "Content/site.css"));
            response.Write(string.Format("{0}\r", "Scripts/modernizr-2.6.2.js"));

            response.Write("\nNETWORK:\r");
            response.Write("*\r");

            response.Write("\nFALLBACK:\r\r");

            //response.Write("/ /offline.html");

            //string[] filePatterns = TestWebSvc.Properties.Settings.Default.FilePatterns.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            //List<string> cacheFileList = new List<string>();

            //foreach (string filePattern in filePatterns)
            //{
            //    cacheFileList.AddRange(Directory.GetFiles(request.PhysicalApplicationPath, filePattern, SearchOption.AllDirectories));
            //}

            //cacheFileList.Sort();

            //foreach (string filePath in cacheFileList)
            //{
            //    response.Write(String.Format("{0}/{1}\n", request.ApplicationPath, filePath.Replace(request.PhysicalApplicationPath, "").Replace(@"\", "/")));
            //}

            response.Flush();
            response.Close();
        }
    }
}