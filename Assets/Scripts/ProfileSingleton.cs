using UnityEngine;

public class ProfileSingleton : MonoBehaviour {
    public static ProfileSingleton instance {get; private set;}
    

    public string profileId {get; set;}
    public string up {get;set;}
    public string back {get;set;}
    public string left {get;set;}
    public string right {get;set;}
    public string shoot {get;set;}
    public bool newPlayer = true;

    private void Awake() {
        if(instance){
            Destroy(gameObject);
    
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}