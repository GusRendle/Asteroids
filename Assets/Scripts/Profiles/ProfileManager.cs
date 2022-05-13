using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class ProfileManager{
    private static List<Profile> profiles = new List<Profile>();

    public static void SaveProfiles(){
        SaveSystem.SaveProfiles(profiles.ToArray());
    }

    public static List<Profile> LoadProfiles(){
        profiles = new List<Profile>(SaveSystem.LoadProfiles().list);
        return profiles;
    }

    public static Profile FindProfile(string profileId){
        return profiles.Find( x => x.profileId == profileId);
    }

    public static void AddProfile(Profile profile){
        profiles.Add(profile);
        SaveProfiles();
    }
}