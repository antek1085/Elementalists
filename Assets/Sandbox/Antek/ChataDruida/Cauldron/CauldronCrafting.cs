using System.Collections.Generic;
using UnityEngine;

public class CauldronCrafting : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    [SerializeField] List<GameObject> ingredientsNeeded = new List<GameObject>();
    [SerializeField] GameObject craftedItem;
    [SerializeField] CauldronItemSlot outpItemSlot;
    bool canCraft;
    
    
    
    
    public void StartCrafting()
    {
        var ingredientsCopy = new List<GameObject>(ingredients);
        for (int i = 0; i < ingredientsNeeded.Count; i++)
        {
            if (ingredientsNeeded.Contains(ingredientsCopy[i]) == false)
            {
                break;
            }
            if (i == ingredientsNeeded.Count - 1)
            {
                canCraft = true;
            }
        }
        if (canCraft)
        {
            ingredients.Clear();
            EndCrafting(craftedItem);
        }
    }

    void EndCrafting(GameObject craftedItem)
    {
        outpItemSlot.CraftedItem(craftedItem);
    }
}
