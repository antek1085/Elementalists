using UnityEngine;
using UnityEngine.UI;

public class PageToggle : MonoBehaviour
{
    [Header("Strony")]
    public GameObject[] allPages;      // Wszystkie strony
    public GameObject thisPage;        // Strona przypisana do tego przycisku

    [Header("Przycisk (opcjonalnie automatycznie znajdzie)")]
    public Button button;              // Przycisk UI

    void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: Nie znaleziono komponentu Button!");
        }

        if (thisPage == null)
        {
            Debug.LogError($"{gameObject.name}: Nie przypisano strony (thisPage)!");
        }
    }

    void OnButtonClick()
    {
        if (thisPage == null) return;

        Debug.Log($"{gameObject.name}: Kliknięto przycisk. Aktywuję: {thisPage.name}");

        // Wyłącz wszystkie inne strony
        foreach (GameObject page in allPages)
        {
            if (page != null && page != thisPage && page.activeSelf)
            {
                page.SetActive(false);
                Debug.Log($"Wyłączono: {page.name}");
            }
        }

        // Aktywuj tę stronę
        thisPage.SetActive(true);
        Debug.Log($"Włączono: {thisPage.name}");
    }
}
