using UnityEngine;
using UnityEngine.UI; 
using FMODUnity; 

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private EventReference buttonClickSound;
    
    [SerializeField] private bool findAllButtonsInScene = true;

    void Start()
    {
        if (buttonClickSound.IsNull)
        {
            Debug.LogWarning("ButtonClick: Nie przypisano dźwięku kliknięcia przycisku (buttonClickSound).");
            return;
        }

        Button[] buttonsToAssignSound;

        if (findAllButtonsInScene)
        {
            buttonsToAssignSound = Resources.FindObjectsOfTypeAll<Button>();
            buttonsToAssignSound = FindObjectsOfType<Button>(true); 
        }
        else
        {
            buttonsToAssignSound = GetComponentsInChildren<Button>(true);
        }


        foreach (Button button in buttonsToAssignSound)
        {
             button.onClick.AddListener(PlayButtonClickSound);
        }

        if (buttonsToAssignSound.Length > 0)
        {
            Debug.Log($"UIButtonSoundManager: Dodano dźwięk kliknięcia do {buttonsToAssignSound.Length} przycisków.");
        }
    }

    private void PlayButtonClickSound()
    {
        if (!buttonClickSound.IsNull)
        {
            RuntimeManager.PlayOneShot(buttonClickSound);
        }
    }
}