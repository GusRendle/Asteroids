using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileMenuManager : MonoBehaviour
{
    public static ProfileMenuManager instance;
    public InputField usernameInput;
    [SerializeField] GameObject profilePrefab;
    private GameObject[] achievementButtons;

    public Text achievementName;
    public Text achievementDesc;
    public Text profileName;
    public Text charMax;

    public static ProfileMenuManager MyInstance {
        get {
            if (instance = null) {
                instance = FindObjectOfType<ProfileMenuManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        achievementButtons = GameObject.FindGameObjectsWithTag("Achievement");
        List<Profile> profiles = ProfileManager.LoadProfiles();
        foreach (Profile profile in profiles){
            AddProfileToList(profile);
        }
        UpdateAchievements(new bool[12]);
    }

    public void UpdateMenu() {
        if(CurrentProfile.Instance.profileId != null) {
            profileName.text = CurrentProfile.Instance.username;
            UpdateAchievements(CurrentProfile.Instance.achievements);
        }
    }

    private void UpdateAchievements(bool[] achievements) {
        int i = 0;
        foreach (GameObject achievementButton in achievementButtons)
        {
            if (achievements[i] == true)
            {
                achievementButton.GetComponent<Image>().color = Color.white;
            } else {
                achievementButton.GetComponent<Image>().color = new Color(0.14f,0.14f,0.14f,1f);
            }

            achievementButton.GetComponent<AchievementGameObject>().achievement = AchievementManager.achievements[i];
            i++;
        }
    }

    public void UpdateAchievementText(Achievement achievement) {
        achievementName.text = achievement.title;
        achievementDesc.text = achievement.description;
    }

    public void AddProfileToList(Profile profile)
    {
        GameObject newProfile = Instantiate(profilePrefab);
        GameObject panel = GameObject.FindGameObjectWithTag("ProfilesPanel");
        newProfile.transform.SetParent(panel.transform, false);

        newProfile.transform.Find("Name").GetComponent<Text>().text = profile.username;
        newProfile.transform.Find("Score").GetComponent<Text>().text = profile.score.ToString();
        ProfileGameObject profileObject = newProfile.GetComponent<ProfileGameObject>();
        profileObject.profile = profile;
    }

    public void AddProfileButton() {
        string inputUsername = usernameInput.text.ToString();
        if (inputUsername.Length > 18 ||inputUsername.Length < 1)
        {
            charMax.text = "18 Char Max";
        } else {
            string profileId = System.Guid.NewGuid().ToString();
            Profile profile = new Profile(profileId, inputUsername);
            ProfileManager.AddProfile(profile);
            AddProfileToList(profile);
            CurrentProfile.Instance.changeCurrentProfile(profileId);
            charMax.text = "";
        }
        
    }

    public void PlayButton ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackButton ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
