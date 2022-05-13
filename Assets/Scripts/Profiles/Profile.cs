using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Profile {
    public string profileId;
    public string username;

    public bool[] achievements = new bool[12];
    public bool newPlayer = true;
    public int score = 0;

    public string thrust = KeyCode.W.ToString();
    public string left = KeyCode.A.ToString();
    public string right = KeyCode.D.ToString();
    public string back = KeyCode.S.ToString();
    public string shoot = KeyCode.Space.ToString();

    public Profile(string profileId, string username){
        this.profileId = profileId;
        this.username = username;
    }
}

[System.Serializable]
public class ProfilesList{
    public Profile[] list;

    public ProfilesList(){
        list = new Profile[0];
    }

    public ProfilesList(Profile[] profiles){
        list = profiles;
    }

}