using System;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
public class BlockingWall : MonoBehaviour
{
    public Spell.spellType SpellType;

    [Header("SOUDNS")] 
    [SerializeField] EventReference passiveLoopSound;
    [SerializeField] EventReference destroySound;

    private EventInstance loopInstance;
    private void Start()
    {
        if (string.IsNullOrEmpty(passiveLoopSound.Path)) return; //???? null check 

        loopInstance = RuntimeManager.CreateInstance(passiveLoopSound);
        RuntimeManager.AttachInstanceToGameObject(loopInstance, gameObject);
        loopInstance.start();
    }

    void OnTriggerEnter(Collider other)
    {
        var spell = other.GetComponent<Spell>();
        if (spell != null && spell.Type == SpellType)
        {
            OnSpellHit();
        }
    }

    void OnSpellHit()
    {
        RuntimeManager.PlayOneShotAttached(destroySound, gameObject); // destroy sound
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (loopInstance.isValid()) //
        {
            loopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            loopInstance.release();
        }
    }
}
