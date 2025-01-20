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

    void Awake()
    {
        animalUI = GetComponent<AnimalUI>();
        interacted = false;
    }

    public bool AnimalInteraction(GameObject item)
    {
        if (item == objectToGive)
        {
            GivenRightItem();
            animalUI.GiveItemUIPopUp(true);
            if (interacted == false)
            {
                interacted = true;
                return false;
            }
            return true;
        }
        else
        {
            interacted = true;
            GivenWrongItem();
            animalUI.GiveItemUIPopUp(false);
            return false;
        }
    }


    void GivenRightItem()
    {
        Debug.Log("GivenRightItem");
    }

    void GivenWrongItem()
    {
        Debug.Log("GivenWrongItem");
    }
}


