using System;
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

    [Header("Animator Settings")]

    [SerializeField] Animator fireAnimator; 
    [SerializeField] Animator waterAnimator; 
    [SerializeField] string isCastParamName = "isCast"; 

    bool m_Cast;

    void Start()
    {
        DrawingSpellsEvent.current.OnSpellCast += SpellCast;
    }

    // Fire Spell 0  ||| Water Spell 1
    void SpellCast(float[] output)
    {
        // Fire Spell cast
        if (output[0] > boolCastThreshold.value)
        {
            GameObject spell = Instantiate(fireSpell, transform.position, transform.rotation);
            spell.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * fireForce);

            if (fireAnimator != null)
            {
                fireAnimator.SetBool(isCastParamName, true); // Włączenie animacji rzucania ognia zaraz po rzuceniu zaklęcia
            }

            Invoke(nameof(ResetFireAnimation), 0.5f); // Wyłączenie animacji po 1 sekundzie
        }

        // Water Spell cast
        if (output[1] > boolCastThreshold.value)
        {
            GameObject spell = Instantiate(waterSpell, transform.position, transform.rotation);
            spell.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * waterForce);

            if (waterAnimator != null)
            {
                waterAnimator.SetBool(isCastParamName, true); // Włączenie animacji rzucania wody zaraz po rzuceniu zaklęcia
            }

            Invoke(nameof(ResetWaterAnimation), 1f); // Wyłączenie animacji po 1 sekundzie
        }
    }

    void ResetFireAnimation()
    {
        if (fireAnimator != null)
        {
            fireAnimator.SetBool(isCastParamName, false); // Wyłączenie animacji rzucania ognia
        }
    }

    void ResetWaterAnimation()
    {
        if (waterAnimator != null)
        {
            waterAnimator.SetBool(isCastParamName, false); // Wyłączenie animacji rzucania wody
        }
    }
}