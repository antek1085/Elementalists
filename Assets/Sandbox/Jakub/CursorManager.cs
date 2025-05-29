using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        ShowCursor();
    }

    // Możesz również dodać te metody publicznie, aby wywoływać je z innych skryptów, jeśli zajdzie potrzęba
    public void ShowCursor()
    {
        Cursor.visible = true; // Ustawia kursor na widoczny
        Cursor.lockState = CursorLockMode.None; // Odblokowuje kursor, pozwalając mu na swobodne poruszanie się
    }

    public void HideCursor()
    {
        Cursor.visible = false; // Ukrywa kursor
        Cursor.lockState = CursorLockMode.Locked; // Blokuje kursor w centrum ekranu
    }
}