using System;
using FMODUnity;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    
    // Przypisz obiekt, który ma być aktywowany
    public GameObject targetObject; // Przypisz menu pauzy
    private bool isPaused = false; // Czy gra jest w trybie pauzy
    private bool input;

    [Header("Sounds")]
    [SerializeField] private EventReference menuSoundOpen;
    [SerializeField] private EventReference menuSoundClose;
    

    void Start()
    {
        StopInputEvent.current.OnStopInput += OnChangeInput;
        input = true;
    }
    void OnChangeInput(GameObject obj)
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
        if(!input) return;
        
        // Sprawdź, czy klawisz E został naciśnięty
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (targetObject != null)
            {
                TogglePause(); // Przełącz pauzę
            }
           
        }
    }

    // Funkcja przełączająca pauzę gry
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    
   void PauseGame()
    {
        StopInputEvent.current.StopInput(gameObject);
        RuntimeManager.PlayOneShot(menuSoundOpen);// Open UI sound
        Cursor.lockState = CursorLockMode.None; // Odblokowanie kursora
        Cursor.visible = true; // Uwidocznienie kursora
        targetObject.SetActive(true); // Włącz menu pauzy
        
    }

    // Funkcja wyłączająca pauzę
     void ResumeGame()
     {
         StopInputEvent.current.StopInput(null);
        isPaused = false; 
        RuntimeManager.PlayOneShot(menuSoundClose);// Close UI Sound
        Cursor.lockState = CursorLockMode.Locked; // Zablokowanie kursora
        Cursor.visible = false; // Ukrycie kursora
        targetObject.SetActive(false); // Wyłącz menu pauzy
    
    }

     public void Continue()
    {
        if (isPaused)
        {
            ResumeGame(); 
        }
    }
}
    

