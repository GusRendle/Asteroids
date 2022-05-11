
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public InputHandler handler;
    public Text currentUser;
    public Profile profile;
    public Button NewGame;
    public Button LoadGame;

    private void Awake()
    {
        if (ProfileManager.FindProfile(ProfileSingleton.instance.profileId) != null)
        {
            profile = ProfileManager.FindProfile(ProfileSingleton.instance.profileId);
            currentUser.text = profile.username;
            NewGame.interactable = true;
            LoadGame.interactable = true;
            NewGame.GetComponentInChildren<Text>().color = Color.white;
            LoadGame.GetComponentInChildren<Text>().color = Color.white;
        } else {
            currentUser.text = "SELECT A PROFILE";
            NewGame.interactable = false;
            LoadGame.interactable = false;
            NewGame.GetComponentInChildren<Text>().color = Color.gray;
            LoadGame.GetComponentInChildren<Text>().color = Color.gray;
        }
    }

   public void ProfileMenuButton ()
    {
        SceneManager.LoadScene("ProfilesMenu");
    }

    public void PlayButton ()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadButton ()
    {
        SceneManager.LoadScene("Game");
    }
    public void AchButton ()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void BackButton ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
