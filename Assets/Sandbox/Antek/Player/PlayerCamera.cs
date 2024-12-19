using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float mouseX, mouseY;
    [SerializeField] float sesitivityX, sesitivityY;
    float xRotation, yRotation;
    bool isCasting;

    [SerializeField] Transform orientation;

    
    void Awake()
    {
        isCasting = false;
        DrawingSpellsEvent.current.OnSpellStartCasting += SpellStartCasting;
        DrawingSpellsEvent.current.OnSpellCast += SpellCast;
    }
    void SpellStartCasting()
    {
        isCasting = !isCasting;
    }
    void SpellCast(float[] obj)
    {
        isCasting = !isCasting;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (isCasting == false)
        {
            mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sesitivityX;
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sesitivityY;

            yRotation -= mouseX * -1;
            xRotation += mouseY * -1;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
            transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
            orientation.rotation = Quaternion.Euler(0,yRotation,0); 
        }
    }
}
