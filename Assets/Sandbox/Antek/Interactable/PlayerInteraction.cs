using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float rayCastDistance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] SO_GameObject_List EQList;
    int slotChoosed;
    Camera mainCamera;
    bool input;



    void Start()
    {
        mainCamera = Camera.main;
        EQEvent.current.onSlotChanged += i => slotChoosed = i;
        StopInputEvent.current.OnStopInput += ChangeInput;
        input = true;
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
        
        if (Input.GetKeyUp(KeyCode.E))
        {
            RaycastHit hitInfo;
            
            if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward,out hitInfo,rayCastDistance,layerMask))
            {
                IPickable pickable = hitInfo.transform.GetComponent<IPickable>();
                if (pickable != null)
                {
                    Debug.LogFormat("1");
                    PickUpItemToEq(pickable);
                    return;
                }


                IAnimalInteractable animalInteractable = hitInfo.transform.GetComponent<IAnimalInteractable>();
                if (animalInteractable != null)
                {
                    AnimalInteraction(animalInteractable);
                    return;
                }

            }
        }
    } 
    void AnimalInteraction(IAnimalInteractable animalInteractable)
    {
        //Player Interact with Animal
        
        switch (animalInteractable.AnimalInteraction(EQList.objectList[slotChoosed]))
        {

            case true:
                EQList.objectList[slotChoosed] = null;
                EQEvent.current.ItemStateChanged(false);
                break;
            case false:
                Debug.Log("Something went wrong");
                break;
        }
    }
    
    
    //Player tries to Pick Up Item To Inventory
    void PickUpItemToEq(IPickable pickable)
    {
        switch (EQList.objectList[slotChoosed] == null)
        {
            //Item Picked Up
            case true: 
                GameObject pickUp = pickable.PickUp();
                if (pickUp != null)
                {
                    EQList.objectList[slotChoosed] = pickable.PickUp(); 
                    EQEvent.current.ItemStateChanged(true);
                }
                break;
            
            //Slot is not empty
            case false: 
                Debug.Log("Full Slot"); 
                break;
        }  
    }
}
