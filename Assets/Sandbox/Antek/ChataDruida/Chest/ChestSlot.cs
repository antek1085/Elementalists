using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{

   public GameObject slot;
   public Sprite defaultSprite;
   Image iconImage;

   void Start()
   {
       iconImage = transform.GetChild(0).GetComponent<Image>();
   }

   public void StoreItem(GameObject item)
   {
       slot = item;
       iconImage.sprite = slot.GetComponent<ItemInformation>().icon;
   }
    
    public GameObject GetItem()
    {
        Debug.Log(slot.name);
        var item = slot;
        slot = null;
        iconImage.sprite = defaultSprite;
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
