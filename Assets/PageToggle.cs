using UnityEngine;
using UnityEngine.UI;

public class PageToggle : MonoBehaviour
{
    public GameObject[] allPages;      // Lista wszystkich stron/paneli
    public GameObject thisPage;        // Strona przypisana do tego przycisku
    public Button button;              // Przycisk UI przypisany do tej strony

    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (button != null)
            button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Wyłącz wszystkie inne strony
        foreach (GameObject page in allPages)
        {
            if (page != thisPage && page.activeSelf)
            {
                page.SetActive(false);
            }
        }

        // Włącz przypisaną stronę
        thisPage.SetActive(true);
    }
}