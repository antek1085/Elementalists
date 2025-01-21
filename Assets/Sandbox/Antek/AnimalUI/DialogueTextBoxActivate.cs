using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTextBoxActivate : MonoBehaviour
{
    [SerializeField] string animalName;
    [SerializeField] List<float> time;
    [SerializeField] List<string> dialogue;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AnimalUiEvents.current.DialogueStringPopUp(dialogue,time,animalName);
        }
        Destroy(this.gameObject);
    }
}
