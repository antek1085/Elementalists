using System;
using Unity.VisualScripting;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [Header("SpellCast")]
    
    [SerializeField] GameObject waterSpell;
    [SerializeField] GameObject fireSpell;
    
    [Tooltip("Need Testing how much Value")]
    [Range(0,1)]
    [SerializeField] float boolCastThreshold;

    [Header("Speed of spell multiply")]
    
    [SerializeField] float fireForce;
    [SerializeField] float waterForce;
    
    void Awake()
    {
        DrawingSpellsEvent.current.OnSpellCast += SpellCast;
    }
    
    // Fire Spell 0  ||| Water Spell 1
    void SpellCast(float[] output)
    {
        //Fire Spell cast
        if (output[0] > boolCastThreshold)
        {
           GameObject spell = Instantiate(fireSpell, transform.position, transform.rotation);
           spell.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * fireForce);
        }
        
        // Water Spell Cast
        if (output[1] > boolCastThreshold)
        {
            GameObject spell = Instantiate(waterSpell, transform.position, transform.rotation);
            spell.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * waterForce);
        }
        
        
    }
}
