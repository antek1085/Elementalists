using UnityEngine;

public class AmbientZoneTrigger : MonoBehaviour
{
    [SerializeField] private string zoneParameterName; // Assign in Inspector
    private AmbientZone zoneManager;
    bool isInside;

    void Start()
    {
        zoneManager = GetComponentInParent<AmbientZone>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Sprawdzenie jaka gra muzyka
            //wyłaczasz ja
            //właczasz nowa 
            
            zoneManager.SetZoneState(zoneParameterName, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zoneManager.SetZoneState(zoneParameterName, false);
        }
    }
}