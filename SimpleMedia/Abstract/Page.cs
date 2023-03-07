using System.Net;
using System;
namespace SimpleMedia.Abstract;
public abstract class Page {

    public virtual byte[] getResponse(HttpListenerRequest request, HttpListenerResponse response){
        switch(request.HttpMethod){
            case "GET":
                return Get(request, response);
            case "POST":
                return Post(request, response);
            default:
                return InvalidedMethod(request, response);
        }
    }

    private byte[] InvalidedMethod(HttpListenerRequest request, HttpListenerResponse response)
    {
        return "Invalid Method".GetBytes();
    }

    public virtual byte[] Get(HttpListenerRequest request, HttpListenerResponse response){
        return "Invalid Method".GetBytes();
    }
    public virtual byte[] Post(HttpListenerRequest request, HttpListenerResponse response){
        return "Invalid Method".GetBytes();
    }
    public virtual String GetUrl(){
        return "/" + this.GetType().Name.Replace(".", "/");
    }
    public virtual bool LoginRequired(){
        return false;
    }
    public virtual bool isCurrentPage(HttpListenerRequest request){
    //  if(LoginRequired() && LoginManager.LoggedIn(request) == false){
     //       return false;
        //}
        string s = request.Url.AbsolutePath.ToLower();
        string t =   this.GetUrl().ToLower();
        return s == t;
    }
}