using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedInc")]
public class SpeedPowerUp : PowerUpEffect
{
    public float thrustSpeedAmount;

    public SpeedPowerUp(float thrustSpeedAmount)
    {
        this.thrustSpeedAmount = thrustSpeedAmount;
    }
    public override void Effect(GameObject player)
    {
        if (player.GetComponent<Player>().speedInc != null) {
            player.GetComponent<Player>().StopCoroutine(player.GetComponent<Player>().speedInc);
            player.GetComponent<Player>().speedInc = player.GetComponent<Player>().StartCoroutine(LimitedEffect(player));
        } else {
            player.GetComponent<Player>().thrustSpeed += thrustSpeedAmount;
            player.GetComponent<Player>().speedInc = player.GetComponent<Player>().StartCoroutine(LimitedEffect(player));
        }
    }

    public void EndEffect(GameObject player)
    {
        player.GetComponent<Player>().thrustSpeed -= thrustSpeedAmount;
    }

    IEnumerator LimitedEffect(GameObject player) {
        yield return new WaitForSeconds(10f);
        EndEffect(player);
        player.GetComponent<Player>().speedInc = null;
    }

}
