using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AnimalUI))]
public class AnimalInteractionScript : MonoBehaviour,IAnimalInteractable
{
    [SerializeField] GameObject objectToGive;
    AnimalUI animalUI;

    void Awake()
    {
        animalUI = GetComponent<AnimalUI>();
    }

    public bool AnimalInteraction(GameObject item)
    {
        if (item == objectToGive)
        {
            GivenRightItem();
            animalUI.GiveItemUIPopUp(true);
            return true;
        }
        else
        {
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


