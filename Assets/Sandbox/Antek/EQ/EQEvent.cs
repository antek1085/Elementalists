using System;
using UnityEngine;

public class EQEvent : MonoBehaviour
{
   public static EQEvent current;
   void Awake()
   {
      current = this;
   }

   public event Action<int> OnSlotChanged;

   public void SlotChanged(int slot)
   {
      if (OnSlotChanged != null)
      {
         OnSlotChanged(slot);
      }
   }

   public event Action<int> OnSlotChangedUI;

   public void SlotChangedUI(int slot)
   {
      if (OnSlotChangedUI != null)
      {
         OnSlotChangedUI(slot);
      }
   }
   
   
   public event Action<bool> OnItemStateChanged;

   
   //True == added to EQ False == used/dropped and anything else
   public void ItemStateChanged(bool state)
   {
      if (OnItemStateChanged != null)
      {
         OnItemStateChanged(state);
      }
   }
}
