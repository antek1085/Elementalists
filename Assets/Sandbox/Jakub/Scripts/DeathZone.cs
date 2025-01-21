using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform transportTarget; // The target position to transport the player to

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has a Rigidbody (or any other specific component)
        Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
        if (playerRigidbody != null) // This ensures we're interacting with an object with a Rigidbody
        {
            // Transport the player to the target position
            other.transform.position = transportTarget.position;

            // Optional: Reset the Rigidbody velocity
            playerRigidbody.linearVelocity = Vector3.zero;

            Debug.Log($"{other.name} transported to {transportTarget.position}");
        }
    }
}