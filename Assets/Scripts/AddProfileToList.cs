using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AddProfileToList : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        //Obtain the lost of profiles from the storage
        //create a gmae object from the profile prefab foreach profile in the obtained list
        List<Profile> profiles = ProfileManager.GetAllProfiles();
        foreach (Profile profile in profiles){
            AddItem(profile);
        }
    }


    public void AddItem(Profile profile)
    {
        //instantiate a profile game object from the profile refab
        GameObject newObject = Instantiate(prefab);
        //make the profile the child of the current game object 
        //whcih is the gridcontent bc the addprofiletolist is a component of the gridcontent
        newObject.transform.SetParent(gameObject.transform, false);
        //find a game object text that is attacjed to the profile object
        newObject.transform.Find("ProfileName").GetComponent<Text>().text = profile.username;
        newObject.transform.Find("ProfileScore").GetComponent<Text>().text = profile.score.ToString();
        ProfileListItem item = newObject.GetComponent<ProfileListItem>();
        item.profileId = profile.profileId;
    }
}
