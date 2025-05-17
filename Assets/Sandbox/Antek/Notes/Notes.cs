using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    NotesController _notesController;
    [SerializeField] string noteDescription;
    [SerializeField] GameObject noteDisplayObject; 
    [SerializeField] float displayDuration = 3f; 

    void Start()
    {
        _notesController = NotesController.instance;
        
        if (noteDisplayObject != null)
        {
            noteDisplayObject.SetActive(false);
        }
    }

    public void AddNote()
    {
        _notesController.AddNote(noteDescription);
        StartCoroutine(ShowAndHideNote());
    }

    IEnumerator ShowAndHideNote()
    {
        if (noteDisplayObject != null)
        {
            noteDisplayObject.SetActive(true);
            yield return new WaitForSeconds(displayDuration);
            noteDisplayObject.SetActive(false);
        }
    }
}

/*using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    NotesController _notesController;
    AnimalUiEvents _animalUiEvents;
   
    [SerializeField] string noteDescription;
    [SerializeField] List<String> notes = new List<String>();
    [SerializeField] List<float> noteDurations = new List<float>();
    public void AddNote()
    {
        _notesController = NotesController.instance;
        _animalUiEvents = AnimalUiEvents.current;
      
      
        _notesController.AddNote(noteDescription);
        StartCoroutine(notesDialogue());
    }

    IEnumerator notesDialogue()
    {
        yield return new WaitForSeconds(10f);
        _animalUiEvents.DialogueStringPopUp(notes,noteDurations,"");
    }
}
 */