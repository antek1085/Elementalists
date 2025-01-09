using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimalUiEvents : MonoBehaviour
{
    public static AnimalUiEvents current;

    void Awake()
    {
        current = this;
    }

    public event Action<List<String>,string,Sprite> OnDialogueBoxPopUp;

    public void DialogueBoxPopUp(List<String> dialogueBox,string animalName,Sprite icon)
    {
        if (OnDialogueBoxPopUp != null)
        {
            OnDialogueBoxPopUp(dialogueBox,animalName,icon);
        }
    }
    
    public event Action<List<String>,List<float>,string> OnDialogueStringPopUp;

    public void DialogueStringPopUp(List<String> dialofueString, List<float> timeToTextDissapear, string animalName)
    {
        if (OnDialogueStringPopUp != null)
        {
            OnDialogueStringPopUp(dialofueString, timeToTextDissapear, animalName);
        }
    }
}
