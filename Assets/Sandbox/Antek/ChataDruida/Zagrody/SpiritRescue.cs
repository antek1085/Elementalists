using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SpiritRescue : MonoBehaviour
{
    [Header("Spell Type")]
    [SerializeField] Spell.spellType spellType;
    
    [Header("Dialogue")]
    [SerializeField] List<string> message = new List<string>();
    [SerializeField] List<float> duration = new List<float>();
    [SerializeField] string spirtiName;

    [Header("SpiritFollow")]
    [SerializeField] GameObject objectToFollow;
    [SerializeField] GameObject spiritObject;

    [Header("Sound")] 
    [SerializeField] private EventReference ghostFollowSound;
    
    void Start()
    { 
        EnableAfterFoxEvent.current.OnHelpingFox += EnableSpirit;
        var firePenSpiritSpots = FirePenSpiritSpots.instance;
        firePenSpiritSpots.spiritToDeliver++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            var spell = other.GetComponent<Spell>().Type;
            if (spell == spellType)
            {
                AnimalUiEvents.current.DialogueStringPopUp(message,duration,spirtiName);
                var spawnedObject = Instantiate(spiritObject, transform.position, Quaternion.identity);
                spawnedObject.GetComponent<FlyingSpirit>().objectToFollow = objectToFollow;
                if (!ghostFollowSound.IsNull)
                {
                    RuntimeManager.PlayOneShot(ghostFollowSound, transform.position);
                }
                Destroy(gameObject);
            }
        }
    }

    void EnableSpirit()
    {
        gameObject.SetActive(true);
        
        if (!ghostFollowSound.IsNull)
        {
            RuntimeManager.PlayOneShot(ghostFollowSound,transform.position);
        }
    }
    
}
