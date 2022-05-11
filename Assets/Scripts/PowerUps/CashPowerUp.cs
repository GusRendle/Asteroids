using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Cash")]
public class CashPowerUp : PowerUpEffect
{
    public override void Effect(GameObject player)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SetScore(gameManager.score + 250);
        
    }
}
