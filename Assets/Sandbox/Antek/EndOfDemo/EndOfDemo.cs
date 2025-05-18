using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndOfDemo : MonoBehaviour
{
    public static EndOfDemo instance;
    public bool canBeInteractedWith;
    public int itemToFix;
    
    [FormerlySerializedAs("objectsToEnable")] 
    [SerializeField] private List<GameObject> objectsAfterSleep = new List<GameObject>();
    [Header("Objects To Disable")]
    [SerializeField] private List<GameObject> objectsToDisable = new List<GameObject>();
    
    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;     // czas trwania fade in/out
    [SerializeField] private float blackScreenDuration = 0.2f; // ile czasu czarny ekran trwa
    Quest _quest;

    void Awake()
    {
        _quest = GetComponent<Quest>();
        instance = this;
        canBeInteractedWith = false;
        GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < objectsAfterSleep.Count; i++)
        {
            objectsAfterSleep?[i].SetActive(false);
        }
    }

    public void EndingDemo()
    {
        if (canBeInteractedWith)
        {
            StartCoroutine(BlinkEffectAndContinue()); 
            for (int i = 0; i < objectsAfterSleep.Count; i++)
            {
                objectsAfterSleep?[i].SetActive(true);
            }
        }
    }

    private IEnumerator BlinkEffectAndContinue()
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Trzymamy czarny ekran przez moment (jakby "mrugnięcie")
        yield return new WaitForSeconds(blackScreenDuration);

        // Fade back to visible
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        
        for (int i = 0; i < objectsToDisable.Count; i++)
        {
            objectsToDisable[i]?.SetActive(false);
        }
        
        // Możesz dodać tutaj inne efekty: np. teleportacja gracza, duchy itp.
        
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;

        if (fadeCanvasGroup == null)
        {
            Debug.LogWarning("Fade Canvas Group not assigned!");
            yield break;
        }

        fadeCanvasGroup.blocksRaycasts = true;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;

        if (endAlpha == 0f)
        {
            fadeCanvasGroup.blocksRaycasts = false;
        }
    }

    public void ItemFixed()
    {
        itemToFix--;
        if (itemToFix == 0)
        {
            canBeInteractedWith = true;
            GetComponent<BoxCollider>().enabled = true;
            _quest.ControlQuest();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}