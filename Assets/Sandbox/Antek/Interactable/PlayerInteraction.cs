using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float rayCastDistance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] SO_GameObject_List EQList;
    int slotChoosed;
    Camera mainCamera;
    bool input;
    
    [SerializeField] TextMeshProUGUI uiInteractText;



    void Start()
    {
        uiInteractText.enabled = false;
        mainCamera = Camera.main;
        EQEvent.current.OnSlotChanged += i => slotChoosed = i;
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
        if (input == false)
        {
            uiInteractText.enabled = false;
            return;
        }
        
        if (Input.GetKeyUp(KeyCode.E))
        {
            Interaction();
        }
        
        UiInteraction();
        
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
    
    void Interaction()
    {

        RaycastHit hitInfo;

        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward,out hitInfo,rayCastDistance,layerMask))
        {
            IPickable pickable = hitInfo.transform.GetComponent<IPickable>();
            if (pickable != null)
            {
                PickUpItemToEq(pickable);
                return;
            }


            IAnimalInteractable animalInteractable = hitInfo.transform.GetComponent<IAnimalInteractable>();
            if (animalInteractable != null)
            {
                AnimalInteraction(animalInteractable);
                return;
            }

            if (hitInfo.transform.GetComponent<EndOfDemo>() != null)
            {
                hitInfo.transform.GetComponent<EndOfDemo>().EndingDemo();
            }

        }
    }

    void UiInteraction()
    {
        RaycastHit hitInfo;  
        
        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward,out hitInfo,rayCastDistance,layerMask))
        {
            IPickable pickable = hitInfo.transform.GetComponent<IPickable>();
            if (pickable != null && pickable.IcanBePickedUp() == true)
            {
                uiInteractText.enabled = true;
                uiInteractText.text = "Naciśnij E żeby podnieść";
                return;
            }


            IAnimalInteractable animalInteractable = hitInfo.transform.GetComponent<IAnimalInteractable>();
            if (animalInteractable != null)
            {
               uiInteractText.enabled = true;
               uiInteractText.text = "Naciśnij E żeby porozmawiać";
                return;
            }

            if (hitInfo.transform.GetComponent<EndOfDemo>() != null)
            {
                uiInteractText.enabled = true;
                uiInteractText.text = "Naciśnij E żeby odpocząć";
                return;
            }
        }
        
        if (uiInteractText.enabled == true)
        {
            uiInteractText.enabled = false;
        }
    }
}
