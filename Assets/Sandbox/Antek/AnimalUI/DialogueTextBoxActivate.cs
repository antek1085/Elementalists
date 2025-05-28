using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class DialogueTextBoxActivate : MonoBehaviour
{
    [SerializeField] string animalName;
    [SerializeField] List<float> time;
    [SerializeField] List<string> dialogue;

    [Header("Sound Settings")] [SerializeField]
    private EventReference dialoguePopupSound;

    [Header("NPC Transition")] 
    [SerializeField] private GameObject npcToSwap;
    [SerializeField] private GameObject npcSwapped;
    [SerializeField] GameObject vfx; 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (dialoguePopupSound.IsNull == false)
            {
                RuntimeManager.PlayOneShot(dialoguePopupSound);
            }
            AnimalUiEvents.current.DialogueStringPopUp(dialogue,time,animalName);
            
            //swap NPC's

            if (npcToSwap != null)
            {
                npcToSwap.SetActive(false);
                if (vfx != null)
                {
                    Instantiate(vfx,npcToSwap.transform.position,npcToSwap.transform.rotation);   
                }
            }

            if (npcSwapped != null)
            {
                npcSwapped.SetActive(true);
            }
        }
        Destroy(this.gameObject);
    }
}
