using System;
using UnityEngine;

public enum SpiritType{Fire,Water,Ice}

public class FirePenSpiritSpots : MonoBehaviour
{
    public static FirePenSpiritSpots instance;
    Quest quest;
    public bool isReconstructed = false;

    [SerializeField] private GameObject objectToSpawnAfterAllSpirits;
    void Start()
    {
        instance = this;
        quest = GetComponent<Quest>();
    }
    
    [SerializeField] SpiritType typeOfPen; 
    int spiritCounter = 0;
    public int spiritToDeliver;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spirit") && isReconstructed)
        {
            var flyingSpirit = other.GetComponent<FlyingSpirit>();
            if (flyingSpirit.typeOfSpirit == typeOfPen)
            {
                flyingSpirit.objectToFollow = transform.GetChild(spiritCounter)?.gameObject;
                spiritCounter++;
                flyingSpirit.OnPenDestiantion();
            }

            if (spiritCounter == spiritToDeliver)
            {
                quest.ControlQuest();
                objectToSpawnAfterAllSpirits.SetActive(true);
            }
        }
    }
}
