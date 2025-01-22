using System;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    [SerializeField] VisualEffect blowEffect;

    [SerializeField] FMODUnity.EventReference impactSpellSound;
    
    public spellType Type;
    public enum spellType
    {
        Fire,
        Water
    }

    void OnTriggerEnter(Collider other)
    {
        OnCollision();
    }

    void OnCollision()
    {
        if (blowEffect != null)
        {
            blowEffect.Play();
            FMODUnity.RuntimeManager.PlayOneShotAttached(impactSpellSound, gameObject);
        }
        Destroy(gameObject);
    }
}
