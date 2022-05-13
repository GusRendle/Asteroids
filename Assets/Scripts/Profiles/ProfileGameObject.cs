using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileGameObject : MonoBehaviour
{
    public Profile profile;
    public void HandleOnClick(){
        CurrentProfile.Instance.changeCurrentProfile(profile.profileId);
        FindObjectOfType<ProfileMenuManager>().UpdateMenu();
    }  
}
