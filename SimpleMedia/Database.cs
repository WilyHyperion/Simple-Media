namespace SimpleMedia;
using System;
using System.Collections.Generic;
using System.IO;

public static class Database{
    public static List<T> GetObjects<T>(){
        if(objects.ContainsKey(typeof(T))){
            return objects[typeof(T)].Cast<T>().ToList();
        }
        return new List<T>();
    }
    private const string path = "Database.db";
    public static void ClearDictionary(){
        objects.Clear();
    }
    public static void RemoveObject(ISaveable obj){
        if(objects.ContainsKey(obj.GetType())){
            if(!(objects[obj.GetType()].Remove(obj))){
                Console.WriteLine("Failed to remove object");
            }
        }
    }
    private static Dictionary<Type, List<ISaveable>> objects = new Dictionary<Type, List<ISaveable>>();
    public static void AddObject(ISaveable obj){
        if(!objects.ContainsKey(obj.GetType())){
            objects.Add(obj.GetType(), new List<ISaveable>());
        }
        objects[obj.GetType()].Add(obj);
    }
    public static void SaveAllObjects(){
        File.Delete(path);
        using(var stream = new FileStream(path, FileMode.Create)){
            using(var writer = new BinaryWriter(stream)){
                foreach(var type in objects.Keys){
                    string? fullName = type.FullName;
                    if(fullName == null) continue;
                    writer.Write(fullName);
                    writer.Write(objects[type].Count);
                    foreach(var obj in objects[type]){
                        if(obj == null) continue;
                        var data = obj.Save();
                        writer.Write(data.Length);
                        writer.Write(data);
                    }
                }
            }
        }
    }
    public static void LoadAllObjects(){
        ClearDictionary();
        if(!File.Exists(path)){
            return;
        }
        using(var stream = new FileStream(path, FileMode.Open)){
            using(var reader = new BinaryReader(stream)){
                while(reader.BaseStream.Position != reader.BaseStream.Length){
                    var type = Type.GetType(reader.ReadString());
                    int count = reader.ReadInt32();
                    for(int i = 0; i < count; i++){
                        if(type != null){
                        var obj = Activator.CreateInstance(type) as ISaveable;
                        if(obj == null) continue;
                        obj.Load(reader.ReadBytes(reader.ReadInt32()));
                        AddObject(obj);
                        }
                    }
                }
            }
        }
    }
}