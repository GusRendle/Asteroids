using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class ProfileManager{
    //saves the profiles to the storage and retrieves them
    private static List<Profile> profiles = new List<Profile>();


    public static Profile currentProfile {get; private set;}

    public static void SaveProfile(Profile profile){
        SaveSystem.SaveProfiles(profiles.ToArray());
    }


    public static void AddProfile(Profile profile){
        if(!profiles.Contains(profile)) {
            profiles.Add(profile);
            SaveSystem.SaveProfiles(profiles.ToArray());
        }
        
    }

    public static void RemoveProfile(Profile profile){
        if(profiles.Contains(profile)) {
            profiles.Remove(profile);
            SaveSystem.SaveProfiles(profiles.ToArray());
        } 
    }

    public static List<Profile> GetAllProfiles(){
        profiles = new List<Profile>(SaveSystem.LoadProfiles().profiles);
        return profiles;
    }

    public static Profile FindProfile(string profileId){
        foreach(Profile profile in profiles){
            if (profile.profileId == profileId){
                return profile;
            }
        }
        return null;
    }



}