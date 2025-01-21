using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class FMODFadingOutEmitter: MonoBehaviour
{
    [FMODUnity.EventRef] public string eventPath;  
    private EventInstance eventInstance;  
    private bool isFadingOut = false;
    public float fadeDuration = 1.5f; 

    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(eventPath);
        eventInstance.start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFadingOut)  // Spacebar as a trigger for fade out
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        isFadingOut = true;

        float initialVolume = 1.0f;  // Full volume
        float targetVolume = 0.0f;   // Target volume after fading out
        float elapsedTime = 0.0f;

        // While the volume hasn't fully faded out, gradually lower it
        while (elapsedTime < fadeDuration)
        {
            float volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / fadeDuration);
            eventInstance.setVolume(volume);  // Set the volume of the FMOD event instance
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }

        // Ensure the volume is set to 0 at the end
        eventInstance.setVolume(targetVolume);
        
        // Optionally, stop the event after fading out
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        isFadingOut = false;
    }

    void OnDestroy()
    {
        // Release the event instance when the object is destroyed
        eventInstance.release();
    }
}