using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueText : MonoBehaviour
{

    GameObject dialogueTextObject;
    TextMeshProUGUI dialogueText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dialogueTextObject = transform.GetChild(1).gameObject;
        for (int i = 0; i < dialogueTextObject.transform.childCount; i++)
        {
            switch (dialogueTextObject.transform.GetChild(i).name)
            {
                case "Message":
                    dialogueText = dialogueTextObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                    break;
                default:
                    break;
            }
        }
    }

    void Start()
    {
        AnimalUiEvents.current.OnDialogueStringPopUp += DialogueStringPopUp;
    }
    void DialogueStringPopUp(List<string> message, List<float> timeToTextDissapear, string animalName)
    {
        if (dialogueTextObject != null && dialogueTextObject.activeSelf == false)
        {
            dialogueTextObject.SetActive(true);
        }
        StopAllCoroutines();
        StartCoroutine(SetTextTimer(message, timeToTextDissapear, animalName));
    }

    // Update is called once per frame
    IEnumerator SetTextTimer(List<string> message,List<float> timeToTextDissapear, string animalName)
    {
        dialogueText.text = animalName + ": " + message[0];
        yield return new WaitForSeconds(timeToTextDissapear[0]);
        dialogueTextObject.SetActive(false);
    }
}
