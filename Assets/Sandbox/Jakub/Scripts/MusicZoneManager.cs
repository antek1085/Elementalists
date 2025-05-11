using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class MusicZoneManager : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private EventReference musicEventReference;
    [SerializeField] private List<string> musicZoneParameters;

    private EventInstance musicInstance;

    private Dictionary<string, int> activeZoneCounters = new Dictionary<string, int>();

    public static MusicZoneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Znaleziono duplikat MusicZoneManager. Niszczenie nowego obiektu.", gameObject);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (musicZoneParameters == null || musicZoneParameters.Count == 0)
        {
            Debug.LogError("MusicZoneManager: Lista parametrów (musicZoneParameters) jest pusta! Dodaj nazwy parametrów w inspektorze.", gameObject);
            return;
        }

        musicInstance = RuntimeManager.CreateInstance(musicEventReference);
        musicInstance.start();

        InitializeParameters();
    }

    private void InitializeParameters()
    {
        if (!musicInstance.isValid()) return;

        foreach (string paramName in musicZoneParameters)
        {
            activeZoneCounters[paramName] = 0;
            SetParameter(paramName, 0f);
        }
        Debug.Log("MusicZoneManager: Parametry zainicjalizowane.");
    }

    public void PlayerEnteredZone(string parameterName)
    {
        if (!musicZoneParameters.Contains(parameterName))
        {
            Debug.LogWarning($"MusicZoneManager: Próba aktywacji nieznanego parametru: {parameterName}");
            return;
        }

        if (!activeZoneCounters.ContainsKey(parameterName))
        {
            activeZoneCounters[parameterName] = 0;
        }

        activeZoneCounters[parameterName]++;

        if (activeZoneCounters[parameterName] == 1)
        {
            Debug.Log($"MusicZoneManager: Gracz wszedł do strefy: {parameterName}. Aktywacja muzyki.");
            SetParameter(parameterName, 1f);
        }
    }

    public void PlayerExitedZone(string parameterName)
    {
        if (!musicZoneParameters.Contains(parameterName))
        {
             Debug.LogWarning($"MusicZoneManager: Próba deaktywacji nieznanego parametru: {parameterName}");
            return;
        }

         if (!activeZoneCounters.ContainsKey(parameterName))
        {
            Debug.LogWarning($"MusicZoneManager: Próba wyjścia z nieaktywnej strefy: {parameterName}");
            return;
        }

        activeZoneCounters[parameterName]--;

        if (activeZoneCounters[parameterName] < 0)
        {
            activeZoneCounters[parameterName] = 0;
        }

        if (activeZoneCounters[parameterName] == 0)
        {
            Debug.Log($"MusicZoneManager: Gracz opuścił ostatni trigger strefy: {parameterName}. Deaktywacja muzyki.");
            SetParameter(parameterName, 0f);
        }
    }

    private void SetParameter(string name, float value)
    {
        if (!musicInstance.isValid()) return;

        FMOD.RESULT result = musicInstance.setParameterByName(name, value);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError($"MusicZoneManager: Nie udało się ustawić parametru FMOD '{name}' na {value}. Błąd: {result}");
        }
    }

    private void OnDestroy()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
