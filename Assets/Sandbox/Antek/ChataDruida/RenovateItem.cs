using System;
using UnityEngine;

public class RenovateItem : MonoBehaviour
{
    GameObject VFXGameObject;
    public Spell.spellType SpellType;
    void Awake()
    {
        VFXGameObject = transform.GetChild(0).gameObject;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VFXGameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))VFXGameObject.transform.LookAt(other.transform.position);
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VFXGameObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            if (collision.gameObject.GetComponent<Spell>().Type == SpellType)
            {
               OnSpellHit();
            }
        }
    }

    public virtual void OnSpellHit()
    {
        VFXGameObject.GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
