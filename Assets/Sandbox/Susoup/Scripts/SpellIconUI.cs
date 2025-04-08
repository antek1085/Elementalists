using UnityEngine;
using UnityEngine.UI;

public class SpellIconUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image fireSpellIcon;  // Ikona UI dla spella ognistego
    [SerializeField] private Image waterSpellIcon;  // Ikona UI dla spella wodnego
    [SerializeField] private float raycastDistance = 10f;  // Długość promienia raycastu
    [SerializeField] private LayerMask interactionLayerMask;  // Warstwa, na której będą obiekty związane ze spellami

    // Typ wybranego spella
    public enum SpellType { Fire, Water }
    [SerializeField] private SpellType currentSpellType;

    private void Update()
    {
        HandleSpellIconDisplay();
    }

    // Funkcja do wykrywania i wyświetlania ikony odpowiedniego spella
    private void HandleSpellIconDisplay()
    {
        RaycastHit hitInfo;

        // Strzelamy promień z kamery
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, raycastDistance, interactionLayerMask))
        {
            // Sprawdzamy, czy trafiło w obiekt, który ma komponent SpellItem
            SpellItem spellItem = hitInfo.transform.GetComponent<SpellItem>();
            if (spellItem != null)
            {
                // Zależnie od typu spella, pokazujemy odpowiednią ikonę
                if (spellItem.Type == SpellItem.SpellType.Fire)
                {
                    ShowFireSpellIcon();
                }
                else if (spellItem.Type == SpellItem.SpellType.Water)
                {
                    ShowWaterSpellIcon();
                }
                return;
            }
        }

        // Jeśli nie trafiło w żaden obiekt z SpellItem, ukryj obie ikony
        HideAllSpellIcons();
    }

    // Funkcja do wyświetlania ikony spella ognistego
    private void ShowFireSpellIcon()
    {
        fireSpellIcon.enabled = true;  // Aktywuj ikonę spella ognistego
        waterSpellIcon.enabled = false;  // Ukryj ikonę spella wodnego
    }

    // Funkcja do wyświetlania ikony spella wodnego
    private void ShowWaterSpellIcon()
    {
        waterSpellIcon.enabled = true;  // Aktywuj ikonę spella wodnego
        fireSpellIcon.enabled = false;  // Ukryj ikonę spella ognistego
    }

    // Funkcja do ukrywania obu ikon
    private void HideAllSpellIcons()
    {
        fireSpellIcon.enabled = false;
        waterSpellIcon.enabled = false;
    }
}