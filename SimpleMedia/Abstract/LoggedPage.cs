namespace SimpleMedia.Models.Abstract {
    using System.Net;
    using System;
    public abstract class LoggedPage : Page {
        public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response) {
            if (LoginManager.LoggedIn(request)) {
                return GetLogged(request, response);
            } else {
                return GetNotLogged(request, response);
            }
        }
        public override byte[] Post(HttpListenerRequest request, HttpListenerResponse response) {
            if (LoginManager.LoggedIn(request)) {
                return PostLogged(request, response);
            } else {
                return PostNotLogged(request, response);
            }
        }
        public virtual byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return base.Get(request, response);
            }
        public virtual byte[] GetNotLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return Server.Redirect("/Login");
        }
        public virtual byte[] PostLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return "Invalid Method".GetBytes();
        }
        public virtual byte[] PostNotLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return "Invalid Method".GetBytes();
        }
    }
}