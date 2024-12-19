using System;
using UnityEngine;

public class ActivateSpellCasting : MonoBehaviour
{
    GameObject brushObjects;
    void Awake()
    {
        DrawingSpellsEvent.current.OnSpellStartCasting += SpellStartCasting;
        DrawingSpellsEvent.current.OnSpellCast += SpellCast;
        brushObjects = transform.GetChild(0).gameObject;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            brushObjects.SetActive(!brushObjects.activeSelf);
            DrawingSpellsEvent.current.SpellStartCasting();
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    void SpellCast(float[] obj)
    {
        brushObjects.SetActive(false);
    }
    void SpellStartCasting()
    {
    }
    void Start()
    {
        brushObjects.SetActive(false);
    }
    
    
    
}
