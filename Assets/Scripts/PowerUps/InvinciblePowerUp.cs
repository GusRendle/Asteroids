using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Invincible")]
public class InvinciblePowerUp : PowerUpEffect
{
    public InvinciblePowerUp()
    {
    }
    public override void Effect(GameObject player)
    {
        if (player.GetComponent<Player>().invincible != null) {
            player.GetComponent<Player>().StopCoroutine(player.GetComponent<Player>().invincible);
            player.GetComponent<Player>().invincible = player.GetComponent<Player>().StartCoroutine(LimitedEffect(player));
        } else {
            player.gameObject.layer = LayerMask.NameToLayer("Invincible");
            player.GetComponent<Player>().invincible = player.GetComponent<Player>().StartCoroutine(LimitedEffect(player));
        }
    }

    public void EndEffect(GameObject player)
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator LimitedEffect(GameObject player) {
        yield return new WaitForSeconds(10f);
        EndEffect(player);
    }

}
