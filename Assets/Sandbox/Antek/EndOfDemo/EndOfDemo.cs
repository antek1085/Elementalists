using System;
using UnityEngine;
using System.Collections;

public class EndOfDemo : MonoBehaviour
{
    public static EndOfDemo instance;
    public bool canBeInteractedWith;
    public int itemToFix;

    [SerializeField] private GameObject prefabToSpawn;

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;     // czas trwania fade in/out
    [SerializeField] private float blackScreenDuration = 0.2f; // ile czasu czarny ekran trwa

    void Awake()
    {
        instance = this;
        canBeInteractedWith = false;
    }

    public void EndingDemo()
    {
        StartCoroutine(BlinkEffectAndContinue());
    }

    private IEnumerator BlinkEffectAndContinue()
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Trzymamy czarny ekran przez moment (jakby "mrugnięcie")
        yield return new WaitForSeconds(blackScreenDuration);

        // Fade back to visible
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        // Teraz wykonujemy resztę logiki
        prefabToSpawn.SetActive(true); // lis pojawia się
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
        }
    }
}