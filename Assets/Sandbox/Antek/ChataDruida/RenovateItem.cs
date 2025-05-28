using System;
using UnityEngine;

public class RenovateItem : MonoBehaviour
{
    public EndOfDemo endOfDemo;
    public GameObject VFXGameObject;
    public Spell.spellType SpellType;
    
    QuestControler questControler;
    
    [SerializeField] GameObject VFXPrefab;
    public virtual void Awake()
    {
        VFXGameObject = transform.GetChild(0).gameObject;
    }
    public virtual void Start()
    {
        endOfDemo = EndOfDemo.instance;
        endOfDemo.itemToFix++;
        questControler = QuestControler.instance;
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if(!questControler.quests.ContainsKey(3)) return;
        
        if (other.CompareTag("Player") && VFXGameObject != null)
        {
            VFXGameObject.GetComponent<ParticleSystem>()?.Play();
        }
        if (other.CompareTag("Spell") && VFXGameObject != null)
        {
            if (other.GetComponent<Spell>().Type == SpellType)
            {
                OnSpellHit();
            }
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && VFXGameObject != null)VFXGameObject.transform.LookAt(other.transform.position);
    }
    
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && VFXGameObject != null)
        {
            VFXGameObject.GetComponent<ParticleSystem>()?.Stop();
        }
    }

    public virtual void OnSpellHit()
    {
        VFXGameObject.GetComponent<ParticleSystem>()?.Stop();
        Destroy(VFXGameObject);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        endOfDemo.ItemFixed();
        Instantiate(VFXPrefab, transform.position, transform.rotation);
    }
}
