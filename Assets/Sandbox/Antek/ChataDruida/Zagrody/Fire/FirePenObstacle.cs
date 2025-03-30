using System;
using UnityEngine;

public class FirePenObstacle : MonoBehaviour
{
    public Spell.spellType SpellType; 
    public GameObject particleSystem;

    void Awake()
    {
        particleSystem = transform.GetChild(0).gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            var spell = other.GetComponent<Spell>();
            if (spell != null && spell.Type == SpellType)
            {
                OnSpellHit();
            }  
        }
    }

    void OnSpellHit()
    {
        Debug.Log("Fire Pen");
        transform.parent.GetComponent<FirePenReconstruction>().ObstacleDestroyed();
        Destroy(gameObject);
    }
}
