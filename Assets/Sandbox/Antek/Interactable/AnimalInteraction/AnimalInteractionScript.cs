using System.Collections.Generic;
using UnityEngine;

public class AnimalInteractionScript : MonoBehaviour,IAnimalInteractable
{
    [SerializeField] GameObject objectToGive;
    public bool AnimalInteraction(GameObject item)
    {
        if (item == objectToGive)
        {
            GivenRightItem();
            return true;
        }
        else
        {
            GivenWrongItem();
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


