using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform transportTarget;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
        if (playerRigidbody != null) 
        {
            other.transform.position = transportTarget.position;
            
            playerRigidbody.linearVelocity = Vector3.zero;

            Debug.Log($"{other.name} transported to {transportTarget.position}");
        }
    }
}