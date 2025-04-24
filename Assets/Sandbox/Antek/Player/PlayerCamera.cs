using System;
using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float mouseX, mouseY;
    [SerializeField] float sesitivityX, sesitivityY;
    float xRotation, yRotation;
    bool input;
    bool delay = false;

    [SerializeField] Transform orientation;
    [SerializeField] Vector3 startingScreenPostion;

    
    void Awake()
    {
        input = true;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        StopInputEvent.current.OnStopInput += ChangeInput;
        DrawingSpellsEvent.current.OnSpellCast += SpellCast;
        StartCoroutine(DelayStart());
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
    void SpellCast(float[] obj)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (input == true && delay)
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

    IEnumerator DelayStart()
    {
        yield return new WaitForSecondsRealtime(1f);
        delay = true;
    }
}
