namespace SimpleMedia.Models;
public interface ISaveable{
    public byte[] Save();
    public void Load(byte[] data);
}