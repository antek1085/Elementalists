using System;
using Unity.VisualScripting;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [Header("SpellCast")]
    
    [SerializeField] GameObject waterSpell;
    [SerializeField] GameObject fireSpell;

    [Tooltip("Need Testing how much Value")]
    [SerializeField] SO_FloatMinMAx boolCastThreshold;

    [Header("Speed of spell multiply")]
    
    [SerializeField] float fireForce;
    [SerializeField] float waterForce;
    
    void Start()
    {
        DrawingSpellsEvent.current.OnSpellCast += SpellCast;
    }
    
    // Fire Spell 0  ||| Water Spell 1
    void SpellCast(float[] output)
    {
        /*Debug.Log(output[0] + "Fire");
        Debug.Log(output[1] + "Water");*/
        //Fire Spell cast
        if (output[0] > boolCastThreshold.value)
        {
           GameObject spell = Instantiate(fireSpell, transform.position, transform.rotation);
           spell.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * fireForce);
        }
        
        // Water Spell Cast
        if (output[1] > boolCastThreshold.value)
        {
            GameObject spell = Instantiate(waterSpell, transform.position, transform.rotation);
            spell.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * waterForce);
        }
        
        
    }
}
