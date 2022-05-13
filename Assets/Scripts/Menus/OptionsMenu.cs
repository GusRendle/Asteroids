using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class OptionsMenu : MonoBehaviour
{
    private bool isMuted = false;
    public Text muteButtonText;
    public Slider volumeSlider;
    public CanvasGroup defaultMenu;
    public CanvasGroup keybindsMenu;
    private GameObject[] keybindButtons;

    public void Awake() {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    
    void Start() {
        if (!PlayerPrefs.HasKey("muted") || !PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetInt("muted", 0);
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        Load();
    }

    public void OnMuteButtonPress() {
        if (isMuted) {
            isMuted = false;
            AudioListener.pause = false;
            muteButtonText.text = "MUTE";
        } else {
            isMuted = true;
            AudioListener.pause = true;
            muteButtonText.text = "UN-MUTE";
        }
    }

    public void OnBackButtonPress ()
    {
        SceneManager.LoadScene("MainMenu");
        CurrentProfile.Instance.updateProfileFile();
        ProfileManager.SaveProfiles();
    }

    public void OnKeybindBackButtonPress ()
    {
        toggleKeybindsMenu();
    }

    public void OnKeybindButtonPress ()
    {
        toggleKeybindsMenu();
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void toggleKeybindsMenu() {
        if (defaultMenu.alpha > 0) {
            defaultMenu.alpha = 0;
            defaultMenu.blocksRaycasts = false;
            defaultMenu.interactable = false;
            keybindsMenu.interactable = true;
            keybindsMenu.alpha = 1;
            keybindsMenu.blocksRaycasts = true;
            CurrentProfile p = CurrentProfile.Instance;
            if (p.thrustKey != KeyCode.None)
            {
                UpdateKeyText("UP", p.thrustKey);
                UpdateKeyText("LEFT", p.leftKey);
                UpdateKeyText("DOWN", p.backKey);
                UpdateKeyText("RIGHT", p.rightKey);
                UpdateKeyText("SHOOT", p.shootKey);
            }



        } else {
            defaultMenu.alpha = 1;
            defaultMenu.blocksRaycasts = true;
            defaultMenu.interactable = true;
            keybindsMenu.interactable = false;
            keybindsMenu.alpha = 0;
            keybindsMenu.blocksRaycasts = false;
        }
    }

    public void UpdateKeyText(string key, KeyCode code) {
        Text keyName = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        keyName.text = code.ToString();
    }

    private void Load() {
        isMuted = PlayerPrefs.GetInt("isMuted") == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save() {
        PlayerPrefs.SetInt("muted", isMuted ? 1:0);
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
