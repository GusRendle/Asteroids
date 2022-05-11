using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileListItem : MonoBehaviour {
    public string profileId  {get; set;}
    public void HandleOnClick(){
        //get the profile id of the clicked item to access the save file of the current profile id
        //tell next scene which profile has been chosen
        ProfileSingleton.instance.profileId = profileId;
        //load the game scene
        SceneManager.LoadScene("MainMenu");
        //switch the scene
    }   
}