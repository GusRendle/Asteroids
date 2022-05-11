using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Wipe")]
public class WipePowerUp : PowerUpEffect
{
    public override void Effect(GameObject player)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.ClearAsteroids();
    }
}
