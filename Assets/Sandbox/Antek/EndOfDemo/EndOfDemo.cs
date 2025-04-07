using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDemo : MonoBehaviour
{
    public static EndOfDemo instance;
    public bool canBeInteractedWith;
    [SerializeField] string sceneToLoad;
    public int itemToFix;


    void Awake()
    {
        instance = this;
        canBeInteractedWith = false;
    }
    public void EndingDemo()
    {
        if (canBeInteractedWith)
        {
            Debug.Log("Ending Demo");
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void DemoButton()
    {
        SceneManager.LoadScene("MainMenu");
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
