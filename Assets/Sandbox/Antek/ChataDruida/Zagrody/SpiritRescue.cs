using System;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRescue : MonoBehaviour
{
    [Header("Spell Type")]
    [SerializeField] Spell.spellType spellType;
    
    [Header("Dialogue")]
    [SerializeField] List<string> message = new List<string>();
    [SerializeField] List<float> duration = new List<float>();
    [SerializeField] string spirtiName;

    [Header("SpiritFollow")]
    [SerializeField] GameObject objectToFollow;
    [SerializeField] GameObject spiritObject;


    void Start()
    { 
        EnableAfterFoxEvent.current.OnHelpingFox += EnableSpirit;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            var spell = other.GetComponent<Spell>().Type;
            if (spell == spellType)
            {
                AnimalUiEvents.current.DialogueStringPopUp(message,duration,spirtiName);
                var spawnedObject = Instantiate(spiritObject, transform.position, Quaternion.identity);
                spawnedObject.GetComponent<FlyingSpirit>().objectToFollow = objectToFollow;
                Destroy(gameObject);
            }
        }
    }

    void EnableSpirit()
    {
        gameObject.SetActive(true);
    }
    
}
