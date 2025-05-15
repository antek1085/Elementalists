using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDemo : MonoBehaviour
{
    public static EndOfDemo instance;
    public bool canBeInteractedWith;
    public int itemToFix;
    
    [SerializeField] private GameObject prefabToSpawn;

    void Awake()
    {
        instance = this;
        canBeInteractedWith = false;
    }
    public void EndingDemo()
    {
        //fadeout ekranu na chwile
        //przeniesienie gracza przed chate 
        // wlaczenie prefabu lisa z dialogami stojacego przed graczem
        // respienie duchów w lesie
        prefabToSpawn.SetActive(true); //lis
    }
    
    public void ItemFixed()
    {
        itemToFix--;
        if (itemToFix == 0)
        {
            canBeInteractedWith = true;
        }
    }
}
