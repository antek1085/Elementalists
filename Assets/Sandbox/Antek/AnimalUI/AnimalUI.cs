using System;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimalUI : MonoBehaviour
{
    [Header("Only for Demo")]
    [SerializeField] EndOfDemo endOfDemo;
    [SerializeField] GameObject objectToShow;
    
    
    [Header("Main Information")]
    [SerializeField] string animalName;
    [SerializeField] Sprite animalSprite; 
    
    [Header("First Interaction")]
    [SerializeField] List<String> message = new List<String>();
    
    [Header("Second Interaction")]
    [SerializeField] List<String> messageAfterGivenRightItem = new List<String>();
    
    [Header("Given ALl Items")]
    [SerializeField] List<String> messageAfterGivenAllItems = new List<String>();
    
    [Header("Wrong Item")]
    [SerializeField] List<String> shortMessageAfterGivenWrongItem = new List<String>();
    [SerializeField] List<float> timeToTextDisappear = new List<float>();

    [Header("SOUNDS")] 
    [SerializeField] private EventReference dialoguePopupSound;
    bool wasInteracted = false;
    void Awake()
    {
    }
     public void GiveItemUIPopUp(bool isRightItem,bool givenAllItems)
    {
        PlayPopupSound();
        if (!wasInteracted)
        {
            wasInteracted = true;
            AnimalUiEvents.current.DialogueBoxPopUp(message,animalName,animalSprite);
            return;
        }

        switch (isRightItem)
        {

            case true:
                switch (givenAllItems)
                {

                    case true:
                        break;
                    case false:
                        AnimalUiEvents.current.DialogueBoxPopUp(messageAfterGivenRightItem,animalName,animalSprite);   
                        break;
                }
                break;
            case false:
                switch (givenAllItems)
                {

                    case true:
                        AnimalUiEvents.current.DialogueBoxPopUp(messageAfterGivenAllItems,animalName,animalSprite);
                        
                        //Demo Script
                        endOfDemo.canBeInteractedWith = true;
                        
                        if(objectToShow != null)objectToShow.SetActive(true);
                        
                        
                        Destroy(this.gameObject);
                        break;
                    case false:
                        AnimalUiEvents.current.DialogueStringPopUp(shortMessageAfterGivenWrongItem,timeToTextDisappear,animalName);
                        break;
                }
                break;
        }
    }

     private void PlayPopupSound()
     {
         if (!dialoguePopupSound.IsNull)
         {
             RuntimeManager.PlayOneShot(dialoguePopupSound);
         }
     }
}
