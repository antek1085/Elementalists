using System;
using UnityEngine;

public class EQEvent : MonoBehaviour
{
   public static EQEvent current;
   void Awake()
   {
      current = this;
   }

   public event Action<int> onSlotChanged;

   public void SlotChanged(int slot)
   {
      if (onSlotChanged != null)
      {
         onSlotChanged(slot);
      }
   }

   public event Action<int> onSlotChangedUI;

   public void SlotChangedUI(int slot)
   {
      if (onSlotChangedUI != null)
      {
         onSlotChangedUI(slot);
      }
   }
   
   
   public event Action<bool> onItemStateChanged;

   
   //True == added to EQ False == used/dropped and anything else
   public void ItemStateChanged(bool state)
   {
      if (onItemStateChanged != null)
      {
         onItemStateChanged(state);
      }
   }
}
