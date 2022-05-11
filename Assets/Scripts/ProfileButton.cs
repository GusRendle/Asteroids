using Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//this is the class for the New Profile Button that is going to create a new profile when you click on it
public class ProfileButton : MonoBehaviour {

    public InputField username;
    public string actualUsername;


    [SerializeField]
    private AddProfileToList addProfileToList;

    public void getUsernameInput(string username){
        this.actualUsername = username;
        HandleOnClick();
    }

    public void HandleOnClick() {
        //when you click on the new profile button its going to create a new profile id
        string profileId = "";
        profileId = System.Guid.NewGuid().ToString();
         //either put textbox next to the button or new scene
         //create a new profile with the created profileid and default text
        Profile profile = new Profile(profileId, username.text.ToString());
        //add profile to the list
        ProfileManager.AddProfile(profile);
        addProfileToList.AddItem(profile);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void BackButton ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}