using System.Collections;
using UnityEngine;

public class FireFlowerPickUp : MonoBehaviour,IPickable
{
    [SerializeField] GameObject fireFlowerItem;

    public GameObject PickUp()
    {
        StartCoroutine(DestroyObject());
        return fireFlowerItem;
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
