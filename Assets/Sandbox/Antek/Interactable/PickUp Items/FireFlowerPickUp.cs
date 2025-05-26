using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.VFX;
using STOP_MODE = FMODUnity.STOP_MODE;

[RequireComponent(typeof(ItemInformation))]
public class FireFlowerPickUp : MonoBehaviour,IPickable
{
    [SerializeField] GameObject fireFlowerItem;
    
    //public Spell.spellType spellType;
    [SerializeField] bool canBePickedUp;
    
    [SerializeField] List<GameObject> fireFliesToTurnOff = new List<GameObject>();
    public bool IcanBePickedUp()
    {
        return canBePickedUp;
    }
    [field: SerializeField] public Spell.spellType SpellType { get; set; }

    [Header("SOUNDS")] 
    [SerializeField] private EventReference pickUpSound;
    [SerializeField] private EventReference flowerLoop;
    [SerializeField] private EventReference pickableSound;
    private EventInstance flowerLoopInstance; 
    
    Notes _notes;
    void Awake()
    {
        flowerLoopInstance = RuntimeManager.CreateInstance(flowerLoop);
        flowerLoopInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
        flowerLoopInstance.start();
        _notes = GetComponent<Notes>();

        if (canBePickedUp)
        {
            GetComponentInChildren<VisualEffect>().Stop();
        }
        /*canBePickedUp = false;*/
    }

    private void Update()
    {
        flowerLoopInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Spell>() != null && other.GetComponent<Spell>().Type == SpellType)
        {
            canBePickedUp = true;
            Debug.Log("Pickable");
            RuntimeManager.PlayOneShotAttached(pickableSound,gameObject); // pickable sound feedback
            GetComponentInChildren<VisualEffect>().Stop();
            if (_notes != null)
            {
                _notes.AddNote();
            }
        }
    }

    public GameObject PickUp()
    {
        if (!canBePickedUp) return null;

        RuntimeManager.PlayOneShotAttached(pickUpSound, gameObject); //PlayOneShot/PlayOneShotAttached
        flowerLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Fade out the loop
        StartCoroutine(DestroyObject());
        return fireFlowerItem;
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        flowerLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        flowerLoopInstance.release();
        foreach (var t in fireFliesToTurnOff)
        {
            if (t != null)
            {
                t.SetActive(false);   
            }
        }
        if (_notes != null)
        {
            _notes.HideNote();
        }
    }
}