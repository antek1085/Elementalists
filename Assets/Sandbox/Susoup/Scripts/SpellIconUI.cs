using UnityEngine;
using UnityEngine.UI;

public class SpellIconUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image fireSpellIcon;  
    [SerializeField] private Image waterSpellIcon;  
    [SerializeField] private float raycastDistance = 10f;  
        [SerializeField] private LayerMask interactionLayerMask;  
  
    public enum SpellType { Fire, Water }
    [SerializeField] private SpellType currentSpellType;

    private void Update()
    {
        HandleSpellIconDisplay();
    }

    
    private void HandleSpellIconDisplay()
    {
        RaycastHit hitInfo;

        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, raycastDistance, interactionLayerMask))
        {
            
            SpellItem spellItem = hitInfo.transform.GetComponent<SpellItem>();
            if (spellItem != null)
            {
               
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

       
        HideAllSpellIcons();
    }

   
    private void ShowFireSpellIcon()
    {
        fireSpellIcon.enabled = true; 
        waterSpellIcon.enabled = false;  
    }

    
    private void ShowWaterSpellIcon()
    {
        waterSpellIcon.enabled = true; 
        fireSpellIcon.enabled = false;  
    }

   
    private void HideAllSpellIcons()
    {
        fireSpellIcon.enabled = false;
        waterSpellIcon.enabled = false;
    }
}