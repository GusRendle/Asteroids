
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public InputHandler handler;
    public Text currentUser;
    public Button NewGame;
    public Animator transAnim;

    private void Awake()
    {
        if (CurrentProfile.Instance != null )
        {
            currentUser.text = CurrentProfile.Instance.username;
            NewGame.interactable = true;
            NewGame.GetComponentInChildren<Text>().color = Color.white;
        } else {
            currentUser.text = "SELECT A PROFILE";
            NewGame.interactable = false;
            NewGame.GetComponentInChildren<Text>().color = Color.gray;
        }
        if (currentUser.text == "") {
            currentUser.text = "SELECT A PROFILE";
        }
    }

   public void ProfileMenuButton ()
    {
        SceneManager.LoadScene("ProfileMenu");
    }

    IEnumerator LoadGameScene() {
        transAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    } 

    public void PlayButton ()
    {
        StartCoroutine(LoadGameScene());
    }
    public void OptionsButton ()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void LoadButton ()
    {
        SceneManager.LoadScene("Game");
    }
    public void AchButton ()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
