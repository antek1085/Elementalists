using System;
using System.Collections;
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
    [SerializeField] GameObject spellNoteFirstInteraction;
    
    [Header("Second Interaction")]
    [SerializeField] List<String> messageAfterGivenRightItem = new List<String>();
    List<String> messageAfterGivenRightItemWithNumber = new List<String>();
    
    [Header("Given ALl Items")]
    [SerializeField] List<String> messageAfterGivenAllItems = new List<String>();
    [SerializeField] public int questId;
    [SerializeField] private GameObject noteSpellEnable;
    
    [Header("Wrong Item")]
    [SerializeField] List<String> shortMessageAfterGivenWrongItem = new List<String>();
    [SerializeField] List<float> timeToTextDisappear = new List<float>();

    [Header("SOUNDS")] 
    [SerializeField] private EventReference dialoguePopupSound;
    
    bool wasInteracted = false;
    Quest quest;
    Notes _notes;
    AnimalInteractionScript animalInteractionScript;
    void Awake()
    {
        quest = gameObject.GetComponent<Quest>();
        _notes = gameObject.GetComponent<Notes>();
        animalInteractionScript = gameObject.GetComponent<AnimalInteractionScript>();
    }
     public void GiveItemUIPopUp(bool isRightItem,bool givenAllItems)
    {
        PlayPopupSound();
        if (!wasInteracted)
        {
            wasInteracted = true;
            AnimalUiEvents.current.DialogueBoxPopUp(message,animalName,animalSprite);
            
            if (quest != null)
            { 
                quest.ControlQuest();
            }
            if (_notes != null)
            {
                _notes.AddNote();
            }
            if (spellNoteFirstInteraction != null)
            {
                spellNoteFirstInteraction.SetActive(true);
            }
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
                        var number = animalInteractionScript.numberOfFlowersToGive - animalInteractionScript.numberOfFlowersGiven;
                        messageAfterGivenRightItemWithNumber.Add(messageAfterGivenRightItem[0] + number);
                        AnimalUiEvents.current.DialogueBoxPopUp(messageAfterGivenRightItemWithNumber, animalName, animalSprite);
                        messageAfterGivenRightItemWithNumber.Clear();
                        break;
                }
                break;
            case false:
                switch (givenAllItems)
                {

                    case true:
                        AnimalUiEvents.current.DialogueBoxPopUp(messageAfterGivenAllItems,animalName,animalSprite);
                        noteSpellEnable.SetActive(true);
                        QuestControler.instance.EndQuest(questId);
                        //Demo Script
                        endOfDemo.canBeInteractedWith = true;
                        
                        if(objectToShow != null)objectToShow.SetActive(true);
                        
                        
                        Destroy(this.gameObject);
                        Debug.Log(1);
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
