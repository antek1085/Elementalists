using System;
using UnityEngine;

public class EQController : MonoBehaviour
{
   [SerializeField] SO_GameObject_List EQList;
   float mouseScrollWheelValue;
   int selectedEQ;

   void Start()
   {
      for (int i = 0; i < EQList.objectList.Count; i++)
      {
         EQList.objectList[i] = null;
      }
   }


   void Update()
   {
      KeyboardControllEQ();

      ScrollWheelControllEQ();

   }
   void KeyboardControllEQ()
   {

      if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
      {
         SendEventsOnChangingSlot(0);
         selectedEQ = 0;
      }
      if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
      {
         SendEventsOnChangingSlot(1);
         selectedEQ = 1;
      }
      if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
      {
         SendEventsOnChangingSlot(2);
         selectedEQ = 2;
      }
      if (Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Keypad4))
      {
         SendEventsOnChangingSlot(3);
         selectedEQ = 3;
      }
      if (Input.GetKeyUp(KeyCode.Alpha5)|| Input.GetKeyUp(KeyCode.Keypad5))
      {
         SendEventsOnChangingSlot(4);
         selectedEQ = 4;
      }
   }
   void ScrollWheelControllEQ()
   {

      mouseScrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
      if (mouseScrollWheelValue != 0)
      {
         switch (mouseScrollWheelValue)
         {
            case -0.1f :
               selectedEQ += 1;
               if(selectedEQ > 4) selectedEQ = 0;
               SendEventsOnChangingSlot(selectedEQ);
               break;
            case 0.1f :
               selectedEQ -= 1;
               if(selectedEQ < 0) selectedEQ = 4;
               SendEventsOnChangingSlot(selectedEQ);
               break;
            default :
               break;
         }
      }
   }
   void SendEventsOnChangingSlot(int slot)
   {

      EQEvent.current.SlotChanged(slot);
      EQEvent.current.SlotChangedUI(slot);
   }
}
