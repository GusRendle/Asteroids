using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Heart")]
public class HeartPowerUp : PowerUpEffect
{
    public override void Effect(GameObject player)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SetLives(gameManager.lives + 1);
    }
}
