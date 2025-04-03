using System.Collections.Generic;
using UnityEngine;

public class CauldronCrafting : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    [SerializeField] CauldronItemSlot outpItemSlot;
    

    
    
    public void StartCrafting()
    {
     
        /*var crafteditem = null
        EndCrafting(crafteditem);*/
    }

    void EndCrafting(GameObject craftedItem)
    {
        outpItemSlot.StoreItem(craftedItem);
    }
}
