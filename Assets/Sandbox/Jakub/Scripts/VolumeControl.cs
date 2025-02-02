using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    //tldr - create empty and assign sliders 
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    
    
    private const string MusicBus = "bus:/Music";
    private const string SoundBus = "bus:/SFX";

    private FMOD.Studio.Bus musicBus;
    private FMOD.Studio.Bus soundBus;

    private void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus(MusicBus);
        soundBus = FMODUnity.RuntimeManager.GetBus(SoundBus);
        
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.5f);

        SetMusicVolume(musicSlider.value);
        SetSoundVolume(soundSlider.value);
        
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
    }

    private void OnMusicSliderChanged(float value)
    {
        SetMusicVolume(value);
    }

    private void OnSoundSliderChanged(float value)
    {
        SetSoundVolume(value);
    }

    private void SetMusicVolume(float value)
    {
        musicBus.setVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void SetSoundVolume(float value)
    {
        soundBus.setVolume(value);
        PlayerPrefs.SetFloat("SoundVolume", value);
    }
}
