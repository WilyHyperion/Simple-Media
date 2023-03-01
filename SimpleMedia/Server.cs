using System;
using System.Net;
using SimpleMedia.Abstract;
namespace SimpleMedia{
    public class Server{
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
             Console.WriteLine("Request received");
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            String r = getResponse(request);
            response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(r));
            response.OutputStream.Close();
         }
      }
      List<Page> pages = new List<Page>();
      public string getResponse(HttpListenerRequest request){
        foreach( Page p in pages ){
            if( p.isCurrentPage(request) ){
                return p.getResponse(request);
            }
        }
        return "404 Not Found";
    }

        internal static string RenderFile(string v)
        {
            String s = File.ReadAllText(v);
            //Todo rendering
            return s;
        }
    }
}