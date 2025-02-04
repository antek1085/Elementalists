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
            SceneManager.LoadScene(sceneToLoad);
        }
    }


    public void DemoButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
