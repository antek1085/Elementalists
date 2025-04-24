using System;
using UnityEngine;

public class ActivateSpellCasting : MonoBehaviour
{
    GameObject brushObjects;
    [SerializeField] GameObject camera;
    bool input;
    void Awake()
    {
        brushObjects = transform.GetChild(0).gameObject;
        
        input = true;
    }
    
    void Start()
    {
        DrawingSpellsEvent.current.OnSpellCast += SpellCast;
        StopInputEvent.current.OnStopInput += ChangeInput;
        brushObjects.SetActive(false);
        camera.SetActive(false);
    }
    void ChangeInput(GameObject obj)
    {
        if (obj != gameObject)
        {
            input = false;
        }
        if (obj == null)
        {
            input = true;
        }
    }


    void Update()
    {
        if(input == false) return;
        
        
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            brushObjects.SetActive(!brushObjects.activeSelf);
            camera.SetActive(!camera.activeSelf);
            
            if (Cursor.visible == false)
            {
                StopInputEvent.current.StopInput(gameObject);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                StopInputEvent.current.StopInput(null);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    void SpellCast(float[] obj)
    {
        StopInputEvent.current.StopInput(null);
        brushObjects.SetActive(false);
        camera.SetActive(false);
    }
    
}
