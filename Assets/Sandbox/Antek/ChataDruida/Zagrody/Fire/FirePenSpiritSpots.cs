using System;
using UnityEngine;

public enum SpiritType{Fire,Water,Ice}

public class FirePenSpiritSpots : MonoBehaviour
{
    [SerializeField] SpiritType typeOfPen;
    int spiritCounter = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spirit"))
        {
            var flyingSpirit = other.GetComponent<FlyingSpirit>();
            if (flyingSpirit.typeOfSpirit == typeOfPen)
            {
                flyingSpirit.objectToFollow = transform.GetChild(spiritCounter)?.gameObject;
                spiritCounter++;
                flyingSpirit.OnPenDestiantion();
            }
        }
    }
}
