using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class IntroController : MonoBehaviour
{
       public VideoPlayer videoPlayer;
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("Komponent VideoPlayer nie został przypisany!");
            return;
        }

        
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.S))
        {
            SkipToMainMenu();
        }
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        SkipToMainMenu();
    }
    void SkipToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    void OnDestroy()
    {
        
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}