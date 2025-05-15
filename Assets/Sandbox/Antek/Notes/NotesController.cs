using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> notes = new List<TextMeshProUGUI>();
    public static NotesController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        instance = this;
        transform.parent.gameObject.SetActive(false);
    }
    
    public void AddNote(string note)
    {
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].text == " ")
            {
                notes[i].text = note;
                break;
            }
        }
    }
}
