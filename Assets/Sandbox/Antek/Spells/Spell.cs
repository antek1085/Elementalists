using System;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    [SerializeField] VisualEffect blowEffect;
    
    
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
        }
        Destroy(gameObject);
    }
}
