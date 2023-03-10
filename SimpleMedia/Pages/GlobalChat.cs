namespace SimpleMedia.Pages;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class Chat : LoggedPage
{
    List<HttpListenerResponse> listeners = new List<HttpListenerResponse>();
    public Chat(){
        Messages = Database.GetObjects<GlobalMessage>();
    }
    public static List<GlobalMessage> Messages;
    public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        return Server.RenderFile("Frontend/GlobalChat.html");
    }
    public override byte[] Post(HttpListenerRequest request, HttpListenerResponse response)
    {
        if(request.Headers["type"] == "send")
        {
            Console.WriteLine("Sending message");
            GlobalMessage m = new GlobalMessage(Util.ReadRequestBody(request), LoginManager.GetUser(request));
            Messages.Add(m);
            for(int i = 0; i < listeners.Count(); i++)
            {
                HttpListenerResponse r = listeners[i];
                if(r == null || !r.OutputStream.CanWrite)
                {
                    listeners.RemoveAt(i);
                    i--;
                    continue;
                }
                r.OutputStream.Write(m.ToString().GetBytes(), 0, m.ToString().Length);
                r.OutputStream.Close();
                listeners.RemoveAt(i);
                i--;
            }
            
        }
        else if (request.Headers["type"] == "register")
        {
            Console.WriteLine("Registering listener");
            listeners.Add(response);
            return null;
        }
        else if(request.Headers["type"] == "get")
        {
            int amount = int.Parse(request.Headers["amount"]);
            String r = "";
            for(int i = 0; i  < amount; i++)
            {
                if(i >= Messages.Count())
                {
                    break;
                }
                r += Messages[Messages.Count() - 1 - i].ToString() + "\n";
            }
            return r.GetBytes();
        }
        return null;
    }
}