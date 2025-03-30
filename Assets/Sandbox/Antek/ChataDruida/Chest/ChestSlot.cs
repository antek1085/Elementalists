using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{

   public GameObject slot;
   Image iconImage;

   void Start()
   {
       iconImage = GetComponent<Image>();
   }

   public void StoreItem(GameObject item)
   {
       slot = item;
       iconImage.sprite = slot.GetComponent<ItemInformation>().icon;
   }
    
    public GameObject GetItem()
    {
        var item = slot;
        slot = null;
        iconImage.sprite = null;
      return item;
   }

   public bool isSlotEmpty()
   {
       if (slot == null)
       {
           return true;
       }
       return false;
   }
}
