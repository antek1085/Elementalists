using System.Collections;
using UnityEngine;

public interface IPickable
{
    GameObject PickUp();
    bool IcanBePickedUp();
    [field: SerializeField] public Spell.spellType SpellType { get; set; }
    public IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
    }
}