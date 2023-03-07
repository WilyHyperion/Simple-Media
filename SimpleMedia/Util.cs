using System.Net;

public static class Util{
    public static byte[] GetBytes(this string str){
        return System.Text.Encoding.UTF8.GetBytes(str);
    }
    public static int GetStableHashCode(this string str)
    {
        unchecked
        {
            int hash1 = 5381;
            int hash2 = hash1;

            for(int i = 0; i < str.Length && str[i] != '\0'; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1 || str[i+1] == '\0')
                    break;
                hash2 = ((hash2 << 5) + hash2) ^ str[i+1];
            }

            return hash1 + (hash2*1566083941);
        }
    }
    public static string ReadRequestBody(HttpListenerRequest request){
        string body = "";
        try{
        using (System.IO.Stream bodyStream = request.InputStream)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(bodyStream, request.ContentEncoding))
            {
                body = reader.ReadToEnd();
            }
        }
        }
        catch (System.Exception)
        {
            Console.WriteLine("Error reading request body");
            return "";   
        }
        return body;
    }
} 