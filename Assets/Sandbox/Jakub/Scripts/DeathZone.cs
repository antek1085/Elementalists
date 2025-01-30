using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform transportTarget;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent == null) return;
        Rigidbody playerRigidbody = other.transform.parent.GetComponent<Rigidbody>();
        if (playerRigidbody != null) 
        {
            other.transform.parent.position = transportTarget.position;
            
            playerRigidbody.linearVelocity = Vector3.zero;

            Debug.Log($"{other.name} transported to {transportTarget.position}");
        }
    }
}