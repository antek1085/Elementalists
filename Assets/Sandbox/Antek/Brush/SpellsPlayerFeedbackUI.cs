using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellsPlayerFeedbackUI : MonoBehaviour
{
    [SerializeField] SO_FloatMinMAx spellCastThreshold;
    [SerializeField] Image fireSpell,waterSpell;
    [SerializeField] Gradient fireSpellGradient,waterSpellGradient;
    [SerializeField] TextMeshProUGUI fireSpellText,waterSpellText;
    void Start()
    {
        DrawingSpellsEvent.current.OnSpellFloatUI += OnSpellFloatUI;
    }
    
    // Fire Spell 0  ||| Water Spell 1
    void OnSpellFloatUI(float[] obj)
    {
        Debug.Log(obj[0] + "Fire");
        Debug.Log(obj[1] + "Water");
        fireSpell.color = fireSpellGradient.Evaluate(obj[1]);
        waterSpell.color = waterSpellGradient.Evaluate(obj[0]);

        if (spellCastThreshold.value < obj[0])
        {
            fireSpellText.enabled = true;
        }
        else
        {
            fireSpellText.enabled = false;
        }
        if (spellCastThreshold.value < obj[1])
        {
            waterSpellText.enabled = true;
        }
        else
        {
            waterSpellText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
