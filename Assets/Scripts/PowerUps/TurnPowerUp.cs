using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/TurnInc")]
public class TurnPowerUp : PowerUpEffect
{
    public float turnSpeedAmount;

    public TurnPowerUp(float turnSpeedAmount)
    {
        this.turnSpeedAmount = turnSpeedAmount;
    }
    public override void Effect(GameObject player)
    {
        if (player.GetComponent<Player>().turnInc != null) {
            player.GetComponent<Player>().StopCoroutine(player.GetComponent<Player>().turnInc);
            player.GetComponent<Player>().turnInc = player.GetComponent<Player>().StartCoroutine(LimitedEffect(player));
        } else {
            player.GetComponent<Player>().turnSpeed += turnSpeedAmount;
            player.GetComponent<Player>().turnInc = player.GetComponent<Player>().StartCoroutine(LimitedEffect(player));
        }
    }

    public void EndEffect(GameObject player)
    {
        player.GetComponent<Player>().turnSpeed -= turnSpeedAmount;
    }

    IEnumerator LimitedEffect(GameObject player) {
        yield return new WaitForSeconds(10f);
        EndEffect(player);
        player.GetComponent<Player>().turnInc = null;
    }

}
