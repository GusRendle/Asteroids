using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerUpEffect;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public int spriteNum;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }
    void Start()
    {
        spriteRenderer.sprite = sprites[this.spriteNum];
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player"){
            Destroy(gameObject);
            powerUpEffect.Effect(collision.gameObject);
        }
    }
}
