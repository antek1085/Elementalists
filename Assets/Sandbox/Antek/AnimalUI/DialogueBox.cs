using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    private bool DialogueBoxOpen = false;
    private bool dialogueBreak;
    private List<string> dialogueList = new List<string>();
    private string animalName;
    private Sprite animalSprite;
    
    
    GameObject dialogueTexGameObject, animaNameTextGameObject, animalIconGameObject;
    GameObject dialogueBoxGameObject;
    int dialogueIndex;

    void Awake()
    {
        dialogueBoxGameObject = transform.GetChild(0).gameObject;
        for (int i = 0; i < dialogueBoxGameObject.transform.childCount; i++)
        {
            switch (dialogueBoxGameObject.transform.GetChild(i).name)
            {
                case "Message":
                    dialogueTexGameObject = transform.GetChild(0).GetChild(i).gameObject;
                    break;
                case "Name":
                    animaNameTextGameObject = transform.GetChild(0).GetChild(i).gameObject;
                    break;
                case "Animal Icon":
                    animalIconGameObject = transform.GetChild(0).GetChild(i).gameObject;
                    break;
               default:
                   break;
            }
        }
       
    }
    void Start()
    {
        AnimalUiEvents.current.OnDialogueBoxPopUp += OnDialogueBoxPopUp;
    }
    void OnDialogueBoxPopUp(List<string> dialogueListEvent, string animalNameEvent, Sprite iconEvent)
    {
        dialogueIndex = 0;
        
        dialogueList = dialogueListEvent;
        animalName = animalNameEvent;
        animalSprite = iconEvent;
        
        DialogueBoxOpen = true;
        dialogueBoxGameObject.SetActive(true);
        
        StopInputEvent.current.StopInput(gameObject);
        
        StartCoroutine(OpenDialogueBox());
    }

    void Update()
    {
        if (!DialogueBoxOpen) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && dialogueBreak)
        {
            StartCoroutine(OpenDialogueBox());
        }
    }

    IEnumerator OpenDialogueBox()
    {
        dialogueBreak = false;
        ApplyDialogueData();
        yield return new WaitForSeconds(1f);
        dialogueBreak = true;
    }


    void ApplyDialogueData()
    {
        animaNameTextGameObject.GetComponent<TextMeshProUGUI>().text = animalName;
        animalIconGameObject.GetComponent<Image>().sprite = animalSprite;
        if (dialogueIndex < dialogueList.Count)
        {
            dialogueTexGameObject.GetComponent<TextMeshProUGUI>().text = dialogueList[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            dialogueIndex = 0;
            DialogueBoxOpen = false;
            StopInputEvent.current.StopInput(null);
            dialogueBoxGameObject.SetActive(false);
        }
    }
}
