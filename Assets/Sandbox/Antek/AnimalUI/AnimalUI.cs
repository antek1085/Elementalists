using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimalUI : MonoBehaviour
{
    [Header("Main Information")]
    [SerializeField] string animalName;
    [SerializeField] Sprite animalSprite; 
    
    [Header("First Interaction")]
    [SerializeField] List<String> message = new List<String>();
    
    [Header("Second Interaction")]
    [SerializeField] List<String> messageAfterGivenRightItem = new List<String>();
    
    [Header("Wrong Item")]
    [SerializeField] List<String> messageAfterGivenWrongItem = new List<String>();
    [SerializeField] List<float> timeToTextDissapear = new List<float>();
    
    bool wasInteracted = false;
    void Awake()
    {
    }
     public void GiveItemUIPopUp(bool isRightItem)
    {
        if (!wasInteracted)
        {
            wasInteracted = true;
            AnimalUiEvents.current.DialogueBoxPopUp(message,animalName,animalSprite);
            return;
        }

        switch (isRightItem)
        {

            case true:
                AnimalUiEvents.current.DialogueBoxPopUp(messageAfterGivenRightItem,animalName,animalSprite);
                break;
            case false:
                AnimalUiEvents.current.DialogueStringPopUp(messageAfterGivenWrongItem,timeToTextDissapear,animalName);
                break;
        }
    }
}
