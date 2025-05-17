using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad; // Nazwa sceny do załadowania
    public float fadeDuration = 1.0f; // Czas trwania efektu wygaszania
    public Color fadeColor = Color.black; // Kolor wygaszania (domyślnie czarny)

    private bool _isLoading = false;

    // Canvas Group do kontrolowania przezroczystości Canvasu
    private CanvasGroup _fadeCanvasGroup;

    // Obiekt Canvas
    private GameObject _fadeCanvasObject;

    void Start()
    {
        // Stwórz Canvas dynamicznie
        _fadeCanvasObject = new GameObject("FadeCanvas");
        Canvas canvas = _fadeCanvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _fadeCanvasObject.AddComponent<CanvasScaler>();
        _fadeCanvasObject.AddComponent<GraphicRaycaster>();

        // Ustaw Canvas jako najwyższy w hierarchii
        _fadeCanvasObject.transform.SetAsLastSibling();

        // Stwórz panel jako child Canvasu
        GameObject fadePanel = new GameObject("FadePanel");
        fadePanel.transform.SetParent(_fadeCanvasObject.transform);
        Image image = fadePanel.AddComponent<Image>();
        image.color = fadeColor;

        // Rozciągnij panel na cały ekran
        RectTransform panelRectTransform = fadePanel.GetComponent<RectTransform>();
        panelRectTransform.anchorMin = Vector2.zero;
        panelRectTransform.anchorMax = Vector2.one;
        panelRectTransform.offsetMin = Vector2.zero;
        panelRectTransform.offsetMax = Vector2.zero;

        // Dodaj CanvasGroup do panelu
        _fadeCanvasGroup = fadePanel.AddComponent<CanvasGroup>();
        _fadeCanvasGroup.alpha = 0f; // Początkowa przezroczystość
        _fadeCanvasGroup.blocksRaycasts = false; // Zapobiega blokowaniu interakcji

        // Upewnij się, że Canvas jest aktywny
        _fadeCanvasObject.SetActive(true);
    }

    // Funkcja do rozpoczęcia procesu zmiany sceny z wygaszaniem
    public void FadeAndLoadScene()
    {
        if (_isLoading)
            return; // Zapobiegaj wielokrotnemu ładowaniu
        _isLoading = true;
        StartCoroutine(FadeAndLoadSceneCoroutine());
    }

    private IEnumerator FadeAndLoadSceneCoroutine()
    {
        // Rozpocznij wygaszanie ekranu i czekaj na zakończenie
        yield return StartCoroutine(Fade(1f));

        // Załaduj nową scenę po całkowitym wygaszeniu
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = _fadeCanvasGroup.alpha;
        float timer = 0f;

        _fadeCanvasGroup.blocksRaycasts = true; // Blokuj interakcje podczas wygaszania

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }

        _fadeCanvasGroup.alpha = targetAlpha; // Upewnij się, że osiągnięto ostateczną wartość
        _fadeCanvasGroup.blocksRaycasts = false; // Odblokuj interakcje po wygaszeniu
        if (targetAlpha == 0f)
            _fadeCanvasObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Upewnij się, że używasz odpowiedniego tagu dla gracza
        {
            FadeAndLoadScene(); // Wywołaj zmianę sceny po wejściu gracza w trigger
        }
    }
}
