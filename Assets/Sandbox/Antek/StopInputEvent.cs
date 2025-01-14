using System;
using UnityEngine;

public class StopInputEvent : MonoBehaviour
{
    public static StopInputEvent current;
    void Awake()
    {
        current = this;
    }

    public event Action<GameObject> OnStopInput;

    
    //Send gameobject if you need to stop input; send Null to give input back to everything
    public void StopInput(GameObject obj)
    {
        if (OnStopInput != null)
        {
            OnStopInput(obj);
        }
    }
}
