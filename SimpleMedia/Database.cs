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
    private static string path = "Database.db";
    private static Dictionary<Type, List<ISaveable>> objects = new Dictionary<Type, List<ISaveable>>();
    public static void AddObject(ISaveable obj){
        if(!objects.ContainsKey(obj.GetType())){
            objects.Add(obj.GetType(), new List<ISaveable>());
        }
        objects[obj.GetType()].Add(obj);
    }
    public static void SaveAllObjects(){
        using(var stream = new FileStream(path, FileMode.Create)){
            using(var writer = new BinaryWriter(stream)){
                foreach(var type in objects.Keys){
                    writer.Write(type.FullName);
                    writer.Write(objects[type].Count);
                    foreach(var obj in objects[type]){
                        var data = obj.Save();
                        writer.Write(data.Length);
                        writer.Write(data);
                    }
                }
            }
        }
    }
    public static void LoadAllObjects(){
        using(var stream = new FileStream(path, FileMode.Open)){
            using(var reader = new BinaryReader(stream)){
                while(reader.BaseStream.Position != reader.BaseStream.Length){
                    var type = Type.GetType(reader.ReadString());
                    var count = reader.ReadInt32();
                    for(int i = 0; i < count; i++){
                        var obj = Activator.CreateInstance(type) as ISaveable;
                        obj.Load(reader.ReadBytes(reader.ReadInt32()));
                        AddObject(obj);
                    }
                }
            }
        }
    }
}