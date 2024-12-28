using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float rayCastDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject pickedUpObject;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            RaycastHit hitInfo;
            
            if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hitInfo,rayCastDistance,layerMask))
            {
               pickedUpObject = hitInfo.transform.GetComponent<IPickable>().PickUp();
            }
        }
    }
}
