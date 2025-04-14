using UnityEngine;

public class FireCastUIController : MonoBehaviour
{
    public SO_Bool boolValue;          
    public GameObject uiObjectToToggle; 

    void Update()
    {
        if (uiObjectToToggle != null && boolValue != null)
        {
            uiObjectToToggle.SetActive(boolValue.value);
        }
    } 
}