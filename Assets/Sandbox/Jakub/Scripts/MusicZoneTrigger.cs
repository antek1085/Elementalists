using UnityEngine;

public class MusicZoneTrigger : MonoBehaviour
{
    [Tooltip("Parameter Name")]
    [SerializeField] private string fmodParameterName;

    private void Start()
    {
        if (string.IsNullOrEmpty(fmodParameterName))
        {
            Debug.LogError($"MusicZoneTrigger na obiekcie '{gameObject.name}' nie ma przypisanej nazwy parametru FMOD!", gameObject);
        }
        Collider col = GetComponent<Collider>();
        if (col == null || !col.isTrigger)
        {
             Debug.LogWarning($"MusicZoneTrigger na obiekcie '{gameObject.name}' potrzebuje komponentu Collider z włączoną opcją 'Is Trigger'.", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (MusicZoneManager.Instance != null)
            {
                MusicZoneManager.Instance.PlayerEnteredZone(fmodParameterName);
            }
            else
            {
                Debug.LogError("MusicZoneTrigger: Nie znaleziono instancji MusicZoneManager w scenie!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MusicZoneManager.Instance != null)
            {
                MusicZoneManager.Instance.PlayerExitedZone(fmodParameterName);
            }
             else
            {
                Debug.LogError("MusicZoneTrigger: Nie znaleziono instancji MusicZoneManager w scenie!");
            }
        }
    }
}