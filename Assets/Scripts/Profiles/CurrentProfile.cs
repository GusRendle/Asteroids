using UnityEngine;

public class CurrentProfile : MonoBehaviour {
    public static CurrentProfile Instance {get; private set;}
    
    public string profileId;
    public string username;

    public bool[] achievements = new bool[11];
    public bool newPlayer = true;
    public int score = 0;

    public KeyCode thrustKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode backKey = KeyCode.S;
    public KeyCode shootKey = KeyCode.Space;

    private void Awake() {
        if(Instance){
            Destroy(gameObject);
    
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void updateProfileFile() {
        Profile profile = ProfileManager.FindProfile(profileId);
        profile.profileId = profileId;
        profile.username = username;
        profile.achievements = achievements;
        profile.newPlayer = newPlayer;
        profile.score = score;

        profile.thrust = thrustKey.ToString();
        profile.left = leftKey.ToString();
        profile.right = rightKey.ToString();
        profile.back = backKey.ToString();
        profile.shoot = shootKey.ToString();
    }

    public void changeCurrentProfile(string profileId) {
        Profile profile = ProfileManager.FindProfile(profileId);
        this.profileId = profile.profileId;
        username = profile.username;
        achievements = profile.achievements;
        newPlayer = profile.newPlayer;
        score = profile.score;

        thrustKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), profile.thrust);
        leftKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), profile.left);
        rightKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), profile.right);
        backKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), profile.back);
        shootKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), profile.shoot);
    }
}