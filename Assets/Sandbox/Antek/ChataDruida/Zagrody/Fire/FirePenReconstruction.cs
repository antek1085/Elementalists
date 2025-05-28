using System;
using TMPro;
using UnityEngine;

public class FirePenReconstruction : MonoBehaviour
{
   private int numberOfObstacles;
   private int obstacleDestroyedCounter;
   [SerializeField] GameObject parentTextObject;
   private TextMeshProUGUI obstaclesDestroyedText;
   [SerializeField] FirePenSpiritSpots penSpots;
   bool isTextSet = false;
   
   void Awake()
   {
      parentTextObject.SetActive(false);
      numberOfObstacles = transform.childCount;
      obstaclesDestroyedText = parentTextObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
   }

   void OnTriggerEnter(Collider other)
   {
      if(other.CompareTag("Player"))
      {
         parentTextObject.SetActive(true);
         
         for (int i = 0; i < transform.childCount; i++)
         {
            transform.GetChild(i).GetComponent<FirePenObstacle>().particleSystem.GetComponent<ParticleSystem>().Play();
         }
         if (isTextSet == false)
         {
            parentTextObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = numberOfObstacles.ToString();
            obstaclesDestroyedText.text = obstacleDestroyedCounter.ToString();
            isTextSet = true;
         }
      }
   }

   void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         for (int i = 0; i < transform.childCount; i++)
         {
            transform.GetChild(i).GetComponent<FirePenObstacle>().particleSystem.transform.LookAt(other.transform);
         }
      }
   }


   void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         parentTextObject.SetActive(false);
         for (int i = 0; i < transform.childCount; i++)
         {
            transform.GetChild(i).GetComponent<FirePenObstacle>().particleSystem.GetComponent<ParticleSystem>().Stop();
         }
      }
   }
   public void ObstacleDestroyed()
   {
      obstacleDestroyedCounter++;
      obstaclesDestroyedText.text = obstacleDestroyedCounter.ToString();
      if (obstacleDestroyedCounter == numberOfObstacles)
      {
         parentTextObject.SetActive(false);
         gameObject.SetActive(false);
         penSpots.isReconstructed = true;
      }
   }
}
