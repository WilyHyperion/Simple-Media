namespace SimpleMedia;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public static class Util{
     public const string invalidChars = " !@#$%^&*()_+{}|:\"<>?[];',./\\";
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
    public static string Hash(String s){
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(s.GetBytes());
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
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
            return "";   
        }
        return body;
    }
} 