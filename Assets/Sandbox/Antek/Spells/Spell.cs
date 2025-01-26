using System;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    [SerializeField] VisualEffect blowEffect;

    [SerializeField] private FMODUnity.EventReference castSpellSound;
    [SerializeField] private FMODUnity.EventReference impactSpellSound;
    
    public spellType Type;
    public enum spellType
    {
        Fire,
        Water
    }

    private void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(castSpellSound);
    }

    void OnTriggerEnter(Collider other)
    {
        OnCollision();
        OnImpact(); //trigger
    }

    void OnCollision() 
    {
        if (blowEffect != null)
        {
            blowEffect.Play();
        }
        Destroy(gameObject);
    }
    
    void OnImpact() //sound if the spell prefab has a trigger not collision  
    {
        if (blowEffect != null)
        {
            blowEffect.Play();
        }
        FMODUnity.RuntimeManager.PlayOneShotAttached(impactSpellSound, gameObject);
        Destroy(gameObject);
    }
}
