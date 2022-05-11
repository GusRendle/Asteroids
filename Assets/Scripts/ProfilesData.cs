using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfilesData{
    public Profile[] profiles;

    public ProfilesData(Profile[] profiles){
        this.profiles = profiles;
    }

}