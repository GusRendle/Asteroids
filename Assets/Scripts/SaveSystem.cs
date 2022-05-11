
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
public static class SaveSystem{

    public static void SaveGame(Asteroid [] asteroids, GameManager manager,  Player player, string profileId){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + profileId + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        AsteroidData [] asteroidsDataList = new AsteroidData [asteroids.Length];
        PlayerData playerData = new PlayerData(manager, player);

        for (int i = 0; i < asteroids.Length; i++) {
            AsteroidData data = new AsteroidData(asteroids[i]);
            asteroidsDataList[i] = data;
        }
        Asteroids asteroidsList = new Asteroids(asteroidsDataList);
        GameSave game = new GameSave(asteroidsList, playerData);
        formatter.Serialize(stream, game);
        stream.Close();
    }

    public static void SaveProfiles(Profile[] profiles){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/profiles.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        ProfilesData profilesList = new ProfilesData(profiles);

        formatter.Serialize(stream, profilesList);
        stream.Close();

    }

    public static ProfilesData LoadProfiles(){
        string path = Application.persistentDataPath + "/profiles.fun";
        if(File.Exists(path)){
            //open the binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //open the file stream on already existing save file
            FileStream stream = new FileStream(path, FileMode.Open);

            //read from the stream, change it to readable format and save it 
            ProfilesData data = formatter.Deserialize(stream) as ProfilesData;
            stream.Close();
            //Return the data in the readable format
            return data;
        }else{
           //Log error
           Debug.LogError("Save file not found in"+path); 
           return new ProfilesData(new Profile[] {});
        }

    }

    public static GameSave LoadGame(string profileId){
        string path = Application.persistentDataPath + "/"+ profileId + ".fun";
        if(File.Exists(path)){
            //open the binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //open the file stream on already existing save file
            FileStream stream = new FileStream(path, FileMode.Open);

            //read from the stream, change it to readable format and save it 
            GameSave data = formatter.Deserialize(stream) as GameSave;
            stream.Close();
            //Return the data in the readable format
            return data;
        }else{
           //Log error
           Debug.LogError("Save file not found in"+path); 
           return null;
        }

    }
}