using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDemo : MonoBehaviour
{
    
    public bool canBeInteractedWith;
    [SerializeField] string sceneToLoad;


    void Awake()
    {
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
}
