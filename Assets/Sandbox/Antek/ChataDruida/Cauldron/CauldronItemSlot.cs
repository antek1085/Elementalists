using UnityEngine;
using UnityEngine.UI;

public class CauldronItemSlot : MonoBehaviour
{
    public GameObject slot;
    public Sprite defaultSprite;
    Image iconImage;

    public bool isCraftingSlot;
    CauldronCrafting cauldronCrafting;
    
    
    void Start()
    {
        iconImage = transform.GetChild(0).GetComponent<Image>();
        cauldronCrafting = GetComponentInParent<CauldronCrafting>();
    }

    public void StoreItem(GameObject item)
    {
        if (isCraftingSlot)
        {
            slot = item;
            iconImage.sprite = slot.GetComponent<ItemInformation>().icon; 
            cauldronCrafting.ingredients.Add(item);   
        }
    }
    public void CraftedItem(GameObject item)
    {
        slot = item;
        iconImage.sprite = slot.GetComponent<ItemInformation>().icon; 
        cauldronCrafting.ingredients.Add(item);   
    }
    
    public GameObject GetItem()
    {
        var item = slot;
        slot = null;
        iconImage.sprite = defaultSprite;
        cauldronCrafting.ingredients.Remove(item);
        return item;
    }
    

    public bool isSlotEmpty()
    {

        if (slot == null)
        {
            return true;
        }
        return false;
    }
}
