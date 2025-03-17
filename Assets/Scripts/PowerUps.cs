using System.Collections;
using UnityEngine;

public enum PowerUpType { Speed, Jump }

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType; //Set object as Speed or Jump power-up
    public float duration = 15f; //Duration of the power-up
    public float multiplier = 2f; //Multiply power-up effect by this value

    private bool activated = false;

    private void OnTriggerStay(Collider other)
    {
        //Check if player presses E key on item
        if (!activated && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            activated = true;
            //Get the player's movement script
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                //Start the boost on the player script
                if (powerUpType == PowerUpType.Speed)
                {
                    player.StartCoroutine(ApplySpeedBoost(player));
                }
                else if (powerUpType == PowerUpType.Jump)
                {
                    player.StartCoroutine(ApplyJumpBoost(player));
                }
            }
            //Destroy this object so it can't be used again
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement player)
    {
        float originalSpeed = player.speed; //Save original speed
        player.speed *= multiplier; //Increase speed
        yield return new WaitForSeconds(duration); //Wait for the boost duration
        player.speed = originalSpeed; //Reset speed to original value
    }

    private IEnumerator ApplyJumpBoost(PlayerMovement player)
    {
        float originalForce = player.force; //Save original jump force
        player.force *= multiplier; //Increase jump force
        yield return new WaitForSeconds(duration); // ait for the boost duration
        player.force = originalForce; //Reset jump force to original value
    }
}
