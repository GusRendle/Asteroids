using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchObj : MonoBehaviour
{
    public Text title;
    public string titleString;
    public Text description;
    public string descString;
    public Image image;

    private void Start()
    {
        title.text = titleString;
        description.text = descString;
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
