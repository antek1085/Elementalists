using System;
using UnityEngine;

public class DrawingSpellsEvent : MonoBehaviour
{
    public static DrawingSpellsEvent current;
    void Awake()
    {
        current = this;
    }


    public event Action OnSpellStartCasting;

    public void SpellStartCasting()
    {
        if (OnSpellStartCasting != null)
        {
            OnSpellStartCasting();
        }
    }

    public event Action<float[]> OnSpellCast;

    public void SpellCast(float[] output)
    {
        if (OnSpellCast != null)
        {
            OnSpellCast(output);
        }
    }
    public event Action<float[]> OnSpellFloatUI;
    public void SpellFloatUI(float[] output)
    {
        if (OnSpellFloatUI != null)
        {
            OnSpellFloatUI(output);
        }
    }
}
