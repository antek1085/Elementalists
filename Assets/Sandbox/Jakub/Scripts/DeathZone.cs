using UnityEngine;

using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform transportTarget; // The target position to which the player will be transported
    public string playerTag = "Player"; // Tag to identify the player

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the specified tag
        if (other.CompareTag(playerTag))
        {
            // Transport the player to the target position
            other.transform.position = transportTarget.position;

            // Optional: Reset the player's velocity if it has a Rigidbody
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.linearVelocity = Vector3.zero;
            }

            Debug.Log($"{other.name} transported to {transportTarget.position}");
        }
    }
}
