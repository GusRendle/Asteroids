
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
public static class SaveSystem{
    public static void SaveProfiles(Profile[] profiles){
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/profiles.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        ProfilesList profilesList = new ProfilesList(profiles);

        binaryFormatter.Serialize(stream, profilesList);
        stream.Close();

    }

    public static ProfilesList LoadProfiles(){
        string path = Application.persistentDataPath + "/profiles.fun";
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ProfilesList data = binaryFormatter.Deserialize(stream) as ProfilesList;
            stream.Close();
            return data;
        }else{
           //Log error
           Debug.LogError("Save file not found in"+path); 
           return new ProfilesList();
        }

    }
}