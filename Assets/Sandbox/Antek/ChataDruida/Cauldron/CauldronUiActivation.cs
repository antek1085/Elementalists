using System;
using UnityEngine;

public class CauldronUiActivation : MonoBehaviour
{
   GameObject canvas;
   void Awake()
   {
      canvas = transform.GetChild(0).gameObject;
   }

   void OnTriggerEnter(Collider other)
   {
      if(other.CompareTag("Player"))canvas.SetActive(true);
   }

   void OnTriggerExit(Collider other)
   {
      if(other.CompareTag("Player"))canvas.SetActive(false);
   }
}
