namespace SimpleMedia.Pages;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class Chat : LoggedPage
{
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
            Messages.Add(new GlobalMessage(Util.ReadRequestBody(request), LoginManager.GetUser(request)));
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
        return new byte[1];
    }
}