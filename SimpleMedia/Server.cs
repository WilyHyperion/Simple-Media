using System;
using System.Net;
using System.Net.Sockets;
using SimpleMedia.Abstract;
namespace SimpleMedia
{
    public class Server
    {
        public static bool Running = true;
        public static Server Instance;
        const string DOMAIN = "*";
        public static HttpListener listener = new HttpListener();
        public void Start(int port = 8000)
        {
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type t in types)
            {
                if (t.IsSubclassOf(typeof(Page)))
                {
                    try
                    {
                        pages.Add((Page)Activator.CreateInstance(t));
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            listener.Prefixes.Add($"http://{DOMAIN}:{port}/");
            Console.WriteLine($"Server started on {port}");
            listener.Start();
            try
            {
                while (Running)
                {
                    try
                    {
                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;
                        Console.WriteLine("Request to: " + request.Url + " Type of request: " + request.HttpMethod);
                        Byte[] r = getResponse(request, response);
                        if(r == null) {
                            continue;
                        }
                        String rs = System.Text.Encoding.Default.GetString(r);
                        if (rs.Contains("<!DOCTYPE html>"))
                        {
                           // response.AddHeader("Content-Type", "text/html");
                        }
                        response.AddHeader("Content-Type", "text/html");
                        response.OutputStream.Write(r, 0, r.Length);
                        response.OutputStream.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("caught an exception in Main loop: " + e);
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed: + " + e);
            }
            Console.WriteLine("Server stopped, awaiting end call from event handler - Enter CTRL + C to start end sequence");   
            while(true){
                System.Threading.Thread.Sleep(1000);
            }

        }
        List<Page> pages = new List<Page>();
        public byte[] getResponse(HttpListenerRequest request, HttpListenerResponse response)
        {
            //TODO weights
            foreach (Page p in pages)
            {
                if (p.isCurrentPage(request))
                {
                    return p.getResponse(request, response);
                }
            }
            return "404 Not Found".GetBytes();
        }
        internal static byte[] RenderFile(string v, User p){
            String s = File.ReadAllText(v);
            String[] replace = typeof(User).GetProperties().Select(x => x.Name).ToArray();
            foreach (String r in replace)
            {
                Console.WriteLine("Replacing " + r + " with " + p.GetType().GetProperty(r).GetValue(p));
                s = s.Replace("{{" + r + "}}", p.GetType().GetProperty(r).GetValue(p).ToString());
            }
            return s.GetBytes();
        }
        
        internal static byte[] RenderFile(string v, Dictionary<string, string> p)
        {
            String s = File.ReadAllText(v);
            foreach (KeyValuePair<string, string> entry in p)
            {
                s = s.Replace("{{" + entry.Key + "}}", entry.Value);
            }
            return s.GetBytes();
        }
        public static byte[] ReadFile(String path)
        {
            return File.ReadAllBytes(path);
        }
        internal static byte[] RenderFile(string v)
        {
            String s = File.ReadAllText(v);
            return s.GetBytes();
        }
        /// <summary>
        /// returns a page that redirects to the given url
        /// </summary>
        /// <param name="v">the url</param>
        /// <returns></returns>
        internal static byte[] Redirect(string v)
        {
            return File.ReadAllText("Frontend/Redirect.html").Replace("{{URL}}", v).GetBytes();
        }

        internal static bool FileExists(string url)
        {
            return File.Exists(url);
        }
    }
}