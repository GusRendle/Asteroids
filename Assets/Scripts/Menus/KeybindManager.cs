using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeybindManager : MonoBehaviour
{

    private static KeybindManager instance;
    private string bindName = "";

    public static KeybindManager MyInstance {
        get {
            if (instance = null) {
                instance = FindObjectOfType<KeybindManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    public void DefaultKeys()
    {
        BindKey("UP", KeyCode.W);
        BindKey("DOWN", KeyCode.S);
        BindKey("LEFT", KeyCode.A);
        BindKey("RIGHT", KeyCode.D);
        BindKey("SHOOT", KeyCode.Space);
    }

    public void BindKey(string cmd, KeyCode keyBind) {
        CurrentProfile p = CurrentProfile.Instance;
        OptionsMenu optionsMenu = FindObjectOfType<OptionsMenu>();

        if (p.thrustKey == keyBind) {
            p.thrustKey = KeyCode.None;
            optionsMenu.UpdateKeyText("UP", KeyCode.None);
        } else if (p.leftKey == keyBind) {
            p.leftKey = KeyCode.None;
            optionsMenu.UpdateKeyText("LEFT", KeyCode.None);
        } else if (p.rightKey == keyBind) {
            p.rightKey = KeyCode.None;
            optionsMenu.UpdateKeyText("RIGHT", KeyCode.None);
        } else if (p.backKey == keyBind) {
            p.backKey = KeyCode.None;
            optionsMenu.UpdateKeyText("DOWN", KeyCode.None);
        } else if (p.shootKey == keyBind) {
            p.shootKey = KeyCode.None;
            optionsMenu.UpdateKeyText("SHOOT", KeyCode.None);
        }

        switch (cmd)
        {
            case "UP":
                p.thrustKey = keyBind;
                break;
            case "LEFT":
                p.leftKey = keyBind;
                break;
            case "RIGHT":
                p.rightKey = keyBind;
                break;
            case "DOWN":
                p.backKey = keyBind;
                break;
            case "SHOOT":
                p.shootKey = keyBind;
                break;
        }

        optionsMenu.UpdateKeyText(cmd, keyBind);
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName) {
        this.bindName = bindName;
    }

    private void OnGUI() {
        if (bindName != string.Empty) {
            Event e = Event.current;
            if (e != null) {
                if (e.isKey) {
                    BindKey(bindName, e.keyCode);
            }
            }
        }
    }

}
