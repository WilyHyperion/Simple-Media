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
            if(t.IsSubclassOf(typeof(Page)) ){
                pages.Add((Page)Activator.CreateInstance(t));
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

        internal static string RenderFile(string v)
        {
            String s = File.ReadAllText(v);
            return s;
        }
        internal static string Redirect(string v, HttpListenerRequest re, HttpListenerResponse r)
        {
            r.Redirect(v);

            return File.ReadAllText("Frontend/Redirect.html").Replace("{{URL}}", v);
        }
    }
}