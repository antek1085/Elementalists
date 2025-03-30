using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;


public class FlyingSpirit : MonoBehaviour
{ 
   public SpiritType typeOfSpirit;
   public GameObject objectToFollow;

   void Start()
   {
       StartCoroutine(MoveSpirit());
   }
   IEnumerator MoveSpirit()
   {
       yield return new WaitForSeconds(1f);
       transform.DOMove(objectToFollow.transform.position, 2f).SetEase(Ease.OutBack);
       StartCoroutine(MoveSpirit());
   }

   public void OnPenDestiantion()
   {
       StopAllCoroutines();
       transform.DOMove(objectToFollow.transform.position, 3f).SetEase(Ease.OutBack);
   }
}


