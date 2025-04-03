using System;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterFoxEvent : MonoBehaviour
{
    public static EnableAfterFoxEvent current;

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
