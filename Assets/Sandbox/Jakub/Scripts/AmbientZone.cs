using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AmbientZone : MonoBehaviour
{
    [SerializeField] private EventReference ambientEvent; // FMOD Event
    [SerializeField] private List<string> zoneParameters; // List of FMOD parameter names (e.g., "zone_tutorial", "zone_village", "zone_forest")

    private EventInstance ambientInstance;
    private Dictionary<string, float> activeZones = new Dictionary<string, float>(); // Track active zones

    void Start()
    {
        ambientInstance = RuntimeManager.CreateInstance(ambientEvent);
        ambientInstance.start();

        // Initialize all known zones to 0
        foreach (string param in zoneParameters)
        {
            activeZones[param] = 0f;
            ambientInstance.setParameterByName(param, 0f);
        }
    }

    public void SetZoneState(string zoneName, bool isInside)
    {
        if (activeZones.ContainsKey(zoneName))
        {
            float value = isInside ? 1f : 0f;
            activeZones[zoneName] = value;
            ambientInstance.setParameterByName(zoneName, value);
        }
    }

    void OnDestroy()
    {
        ambientInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambientInstance.release();
    }
}