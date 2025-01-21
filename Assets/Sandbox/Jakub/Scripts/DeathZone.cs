using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Tooltip("The target position to which the player will be transported.")]
    public Transform transportTarget;

    [Tooltip("Tag to identify the player.")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} entered the trigger");
        // Check if the object entering the trigger has the specified tag
        if (other.CompareTag(playerTag))
        {
            // Transport the player to the target position
            other.transform.position = transportTarget.position;

            // Optional: Reset the player's velocity if it has a Rigidbody
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.linearVelocity = Vector3.zero; // Reset velocity
                playerRigidbody.angularVelocity = Vector3.zero; // Reset angular velocity
            }

            Debug.Log($"{other.name} was transported to {transportTarget.position}");
            
        }
        }
    }
