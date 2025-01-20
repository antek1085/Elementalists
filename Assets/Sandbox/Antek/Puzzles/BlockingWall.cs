using UnityEngine;

public class BlockingWall : MonoBehaviour
{
    public Spell.spellType SpellType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        Destroy(gameObject);
    }
}
