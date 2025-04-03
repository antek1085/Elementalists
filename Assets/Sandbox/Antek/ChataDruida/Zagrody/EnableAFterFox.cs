using System;
using System.Collections.Generic;
using UnityEngine;

public class EnableAFterFox : MonoBehaviour
{
    public static EnableAFterFox current;

    void Awake()
    {
        current = this;
    }

    public event Action OnHelpingFox;

    public void HelpingFox()
    {
        if (OnHelpingFox != null)
        {
            OnHelpingFox();
        }
    }
}
