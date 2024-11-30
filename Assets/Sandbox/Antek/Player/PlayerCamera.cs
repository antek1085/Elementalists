using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float mouseX, mouseY;
    [SerializeField] float sesitivityX, sesitivityY;
    float xRotation, yRotation;

    [SerializeField] Transform orientation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
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
