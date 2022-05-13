using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementGameObject : MonoBehaviour
{
    public Achievement achievement;
    public void HandleOnClick(){
        FindObjectOfType<ProfileMenuManager>().UpdateAchievementText(achievement);
    }  
}
