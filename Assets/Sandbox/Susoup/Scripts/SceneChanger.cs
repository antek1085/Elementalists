using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   
    // Nazwa sceny, do której ma nastąpić powrót

    private void Start()
    {
        Cursor.visible = true;
    }

    // Funkcja przypisana do guzika
    public void GoToMenu()
    {
       
            SceneManager.LoadScene("MainMenu");
        
    }
    public void GoToGame()
    {
       
            SceneManager.LoadScene("Blockout_Map");
        
    }
}

