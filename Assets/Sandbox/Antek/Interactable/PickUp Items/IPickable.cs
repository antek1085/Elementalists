using System.Collections;
using UnityEngine;

public interface IPickable
{
    GameObject PickUp();
    [field: SerializeField] public Spell.spellType SpellType { get; set; }
    public IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
    }
}