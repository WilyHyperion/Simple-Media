using System;
using System.Net;
using SimpleMedia.Abstract;
namespace SimpleMedia{
    public class Server{
    public static Server Instance;
    const string DOMAIN = "*";
     public HttpListener listener = new HttpListener();
     public void Start( int port = 8000){
        Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        foreach( Type t in types ){
            if(t.IsSubclassOf(typeof(Page))){
                try{
                pages.Add((Page)Activator.CreateInstance(t));
                }catch(Exception e){
                    Console.WriteLine("Failed to create a page: " + e.Message);
                }
            }
        }
        listener.Prefixes.Add($"http://{DOMAIN}:{port}/");
        Console.WriteLine($"Server started on {port}");
        listener.Start();
        while(true){
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            String r = getResponse(request, response);
            Console.WriteLine("Request to: " + request.Url + " Type of request: " + request.HttpMethod);
            //TODO type checking here
            response.AddHeader("Content-Type", "text/html");
            response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(r));
            response.OutputStream.Close();
         }
      }
      List<Page> pages = new List<Page>();
      public string getResponse(HttpListenerRequest request, HttpListenerResponse response){
        //TODO weights
        foreach( Page p in pages ){
            if( p.isCurrentPage(request)){
                return p.getResponse(request, response);
            }
        }
        return "404 Not Found";
    }

        internal static string RenderFile(string v, Dictionary<string, string> p)
        {
            String s = File.ReadAllText(v);
            foreach (KeyValuePair<string, string> entry in p)
            {
                s = s.Replace("{{" + entry.Key + "}}", entry.Value);
            }
            return s;
        }
        internal static string RenderFile(string v)
        {
            String s = File.ReadAllText(v);
            return s;
        }
        internal static string Redirect(string v)
        {
            Console.WriteLine("Redirecting to " + v);
            return File.ReadAllText("Frontend/Redirect.html").Replace("{{URL}}", v);
        }
    }
}