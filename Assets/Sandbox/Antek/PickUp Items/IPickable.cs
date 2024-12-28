using System.Collections;
using UnityEngine;

public interface IPickable
{
    GameObject PickUp();
    
     public IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
    }
}
