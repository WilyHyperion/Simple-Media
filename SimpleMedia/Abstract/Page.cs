using System.Net;
using System;
namespace SimpleMedia.Abstract;
public abstract class Page {
    public virtual string getResponse(HttpListenerRequest request){
        return "Hello World";
    }
    public virtual string GetUrl(){
        return "/" + this.GetType().Name.Replace(".", "/");
    }
    public virtual bool LoginRequired(){
        return false;
    }
    public virtual bool isCurrentPage(HttpListenerRequest request){
        if(LoginRequired() && LoginManager.LoggedIn(request) == false){
            return false;
        }
        string s = request.Url.AbsolutePath.ToLower();
        string t = "/" + this.GetUrl().ToLower();
        Console.WriteLine($"Comparing {s} to {t}");
        return s == t;
    }
}