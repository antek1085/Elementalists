using UnityEngine;
using UnityEngine.UI;

public class CauldronItemSlot : MonoBehaviour
{
    public GameObject slot;
    public Sprite defaultSprite;
    Image iconImage;

    [SerializeField] bool isCraftingSlot;
    CauldronCrafting cauldronCrafting;
    
    
    void Start()
    {
        iconImage = transform.GetChild(0).GetComponent<Image>();
        cauldronCrafting = GetComponentInParent<CauldronCrafting>();
    }

    public void StoreItem(GameObject item)
    {
        slot = item;
        iconImage.sprite = slot.GetComponent<ItemInformation>().icon;
        if (isCraftingSlot)
        {
            cauldronCrafting.ingredients.Add(item);   
        }
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
        if (slot == null && isCraftingSlot)
        {
            return true;
        }
        return false;
    }
}
