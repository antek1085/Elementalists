using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(AnimalUI))]
public class AnimalInteractionScript : MonoBehaviour,IAnimalInteractable
{
    [SerializeField] GameObject objectToGive;
    AnimalUI animalUI;
    bool interacted;
    [SerializeField] int numberOfFlowersToGive;
    public int numberOfFlowersGiven;
    bool didHeGiveAllItems;
    [SerializeField] SO_Bool isHeAbleToCast;
    

    void Awake()
    {
        didHeGiveAllItems = false;
        animalUI = GetComponent<AnimalUI>();
        interacted = false;
    }

    public bool AnimalInteraction(GameObject item)
    {
        Debug.Log(item);
        if (item == objectToGive)
        {
            animalUI.GiveItemUIPopUp(true,didHeGiveAllItems);
            if (interacted == false)
            {
                interacted = true;
                return false;
            }
            numberOfFlowersGiven += 1;
            if(numberOfFlowersGiven >= numberOfFlowersToGive) didHeGiveAllItems = true;
            GivenRightItem();
            return true;
        }
        else
        {
            interacted = true;
            GivenWrongItem();
            animalUI.GiveItemUIPopUp(false,didHeGiveAllItems);
            return false;
        }
    }


    void GivenRightItem()
    {
        if (didHeGiveAllItems)
        {
            isHeAbleToCast.value = true;
            EnableAFterFox.current.HelpingFox();
        }
    }

    void GivenWrongItem()
    {
        Debug.Log("GivenWrongItem");
    }
}


