using System;
using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
   public int targetFrameRate = 60;

   void Start()
   {
      Application.targetFrameRate = targetFrameRate;
   }
}
