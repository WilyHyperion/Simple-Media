using System.Net;

public static class Util{
    public static bool isVaildImage(byte[] data){
        if (data.Length < 4)
        {
            return false;
        }
        //png
        if (!isGif(data) && !isJpeg(data) && !isPng(data))
        {
            Console.WriteLine("Invalid file");
            return false;
        }
        return true;
    }
    public static bool isPng(byte[] data){
        if (data.Length < 4)
        {
            return false;
        }
        //png
        if (data[0] != 0x89 || data[1] != 0x50 || data[2] != 0x4E || data[3] != 0x47)
        {
            return false;
        }
        return true;
    }
    public static bool isJpeg(byte[] data){
        if (data.Length < 4)
        {
            return false;
        }
        //jpeg
        if (data[0] != 0xFF || data[1] != 0xD8 || data[2] != 0xFF || data[3] != 0xE0)
        {
            return false;
        }
        return true;
    }
    public static bool isGif(byte[] data){
        if (data.Length < 4)
        {
            return false;
        }
        //gif
        if (data[0] != 0x47 || data[1] != 0x49 || data[2] != 0x46 || data[3] != 0x38)
        {
            return false;
        }
        return true;
    }
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