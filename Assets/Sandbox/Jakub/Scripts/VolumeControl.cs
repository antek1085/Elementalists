using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider UISlider; 

   
    private const string MusicBusPath = "bus:/Music";
    private const string SoundBusPath = "bus:/SFX";
    private const string UIBusPath = "bus:/UI"; 

   
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";
    private const string UIVolumeKey = "UIVolume"; 
    
    private FMOD.Studio.Bus musicBus;
    private FMOD.Studio.Bus soundBus;
    private FMOD.Studio.Bus uiBus; 

    private void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus(MusicBusPath);
        soundBus = FMODUnity.RuntimeManager.GetBus(SoundBusPath);
        uiBus = FMODUnity.RuntimeManager.GetBus(UIBusPath); 
        
        musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        soundSlider.value = PlayerPrefs.GetFloat(SoundVolumeKey, 1f);
        UISlider.value = PlayerPrefs.GetFloat(UIVolumeKey, 1f); 
        
        SetMusicVolume(musicSlider.value);
        SetSoundVolume(soundSlider.value);
        SetUIVolume(UISlider.value); 
        
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
        UISlider.onValueChanged.AddListener(OnUISliderChanged); 
    }
    
    private void OnMusicSliderChanged(float value)
    {
        SetMusicVolume(value);
    }
    
    private void OnSoundSliderChanged(float value)
    {
        SetSoundVolume(value);
    }
    
    private void OnUISliderChanged(float value)
    {
        SetUIVolume(value);
    }
    
    private void SetMusicVolume(float value)
    {
        musicBus.setVolume(value);
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }
    
    private void SetSoundVolume(float value)
    {
        soundBus.setVolume(value);
        PlayerPrefs.SetFloat(SoundVolumeKey, value);
    }
    
    private void SetUIVolume(float value)
    {
        uiBus.setVolume(value);
        PlayerPrefs.SetFloat(UIVolumeKey, value);
    }
}