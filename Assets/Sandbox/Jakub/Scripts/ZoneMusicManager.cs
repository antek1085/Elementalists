using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicZoneManager : MonoBehaviour
{
    [SerializeField] private EventReference musicEvent;
    private EventInstance musicInstance;
    
    [System.Serializable]
    public class MusicZone
    {
        public string parameterName; // Name of the FMOD parameter for this zone
        public Collider triggerZone; // The trigger collider that defines the zone
    }

    [SerializeField] private List<MusicZone> musicZones = new List<MusicZone>();
    private Dictionary<string, float> zoneStates = new Dictionary<string, float>();

    void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
        
        // Initialize all zones to 0 (not active)
        foreach (var zone in musicZones)
        {
            zoneStates[zone.parameterName] = 0f;
            musicInstance.setParameterByName(zone.parameterName, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var zone in musicZones)
            {
                if (other == zone.triggerZone)
                {
                    SetMusicZone(zone.parameterName, 1f);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var zone in musicZones)
            {
                if (other == zone.triggerZone)
                {
                    SetMusicZone(zone.parameterName, 0f);
                }
            }
        }
    }

    private void SetMusicZone(string parameterName, float value)
    {
        if (zoneStates.ContainsKey(parameterName))
        {
            zoneStates[parameterName] = value;
            musicInstance.setParameterByName(parameterName, value);
        }
    }

    private void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}