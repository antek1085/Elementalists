using System;
using System.Collections;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(ItemInformation))]
public class FireFlowerPickUp : MonoBehaviour,IPickable
{
    [SerializeField] GameObject fireFlowerItem;
    
    
    //public Spell.spellType spellType;
    [SerializeField] bool canBePickedUp;
   [field: SerializeField] public Spell.spellType SpellType { get; set; }

   [SerializeField] private EventReference loopEventPath;
   private EventInstance loopEventInstance;
   void Awake()
    {
        /*canBePickedUp = false;*/
    }

   private void OnEnable()
   {
       loopEventInstance = RuntimeManager.CreateInstance(loopEventPath);
       loopEventInstance.start();
   }

   void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Spell>() != null && other.GetComponent<Spell>().Type == SpellType)
        {
            canBePickedUp = true;
        }
    }

    public GameObject PickUp()
    {   Debug.Log("LLLL");
        if (!canBePickedUp) return null;
        StartCoroutine(DestroyObject());
        return fireFlowerItem;
    }
    
    public IEnumerator DestroyObject()
    {
        float fadeDuration = 1.0f; // fade-out duration
        float startVolume = 1.0f;
        float targetVolume = 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            float volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
            loopEventInstance.setVolume(volume);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }
        
        // Set the volume to 0 at the end and stop the event
        loopEventInstance.setVolume(targetVolume);
        loopEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        yield return new WaitForEndOfFrame();
        loopEventInstance.release(); // release event
        Destroy(gameObject);
        
    }
}
