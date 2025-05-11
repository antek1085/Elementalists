using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AmbientZone : MonoBehaviour
{
    [SerializeField] private EventReference ambientEvent;
    [SerializeField] private List<string> zoneParameters;

    private EventInstance ambientInstance;
    private Dictionary<string, int> activeZoneCounters = new Dictionary<string, int>(); 

    void Start()
    {
        ambientInstance = RuntimeManager.CreateInstance(ambientEvent);
        ambientInstance.start();

        foreach (string param in zoneParameters)
        {
            activeZoneCounters[param] = 0;
            ambientInstance.setParameterByName(param, 0f); 
        }
    }

    public void SetZoneState(string zoneName, bool isInside)
    {
        if (activeZoneCounters.ContainsKey(zoneName))
        {
            if (isInside)
            {
                activeZoneCounters[zoneName]++;
            }
            else
            {
                activeZoneCounters[zoneName]--;
                if (activeZoneCounters[zoneName] < 0) 
                {
                    activeZoneCounters[zoneName] = 0;
                }
            }

            float targetValue = (activeZoneCounters[zoneName] > 0) ? 1f : 0f;
            
            float currentValue;
            ambientInstance.getParameterByName(zoneName, out currentValue);

            if (currentValue != targetValue)
            {
                ambientInstance.setParameterByName(zoneName, targetValue);
            }
        }
    }

    void OnDestroy()
    {
        ambientInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambientInstance.release();
    }
}