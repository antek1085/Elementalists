using System;
using System.Collections;
using UnityEngine;

public class FireFlowerPickUp : MonoBehaviour,IPickable
{
    [SerializeField] GameObject fireFlowerItem;
    
    
    //public Spell.spellType spellType;
    [SerializeField] bool canBePickedUp;
   [field: SerializeField] public Spell.spellType SpellType { get; set; }


    void Awake()
    {
        canBePickedUp = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Spell>() != null && other.GetComponent<Spell>().Type == SpellType)
        {
            canBePickedUp = true;
        }
    }

    public GameObject PickUp()
    {
        if (!canBePickedUp) return null;
        
        StartCoroutine(DestroyObject());
        return fireFlowerItem;
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
