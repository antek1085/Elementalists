using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMODUnity.STOP_MODE;

[RequireComponent(typeof(ItemInformation))]
public class FireFlowerPickUp : MonoBehaviour,IPickable
{
    [SerializeField] GameObject fireFlowerItem;
    
    //public Spell.spellType spellType;
    [SerializeField] bool canBePickedUp;
    [field: SerializeField] public Spell.spellType SpellType { get; set; }

    [Header("SOUNDS")] 
    [SerializeField] private EventReference pickUpSound;
    [SerializeField] private EventReference flowerLoop;
    private EventInstance flowerLoopInstance; 
    void Awake()
    {
        flowerLoopInstance = RuntimeManager.CreateInstance(flowerLoop);
        flowerLoopInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
        flowerLoopInstance.start();
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
    }
}