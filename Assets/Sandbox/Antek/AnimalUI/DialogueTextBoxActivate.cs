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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (dialoguePopupSound.IsNull == false)
            {
                RuntimeManager.PlayOneShot(dialoguePopupSound);
            }
            AnimalUiEvents.current.DialogueStringPopUp(dialogue,time,animalName);
        }
        Destroy(this.gameObject);
    }
}
