using TMPro;
using UnityEngine;

public class SpellsPlayerFeedbackUI : MonoBehaviour
{
    [SerializeField] SO_FloatMinMAx spellCastThreshold;
    [SerializeField] TextMeshProUGUI fireSpell,waterSpell;
    void Start()
    {
        DrawingSpellsEvent.current.OnSpellFloatUI += OnSpellFloatUI;
    }
    
    // Fire Spell 0  ||| Water Spell 1
    void OnSpellFloatUI(float[] obj)
    {
        fireSpell.text = obj[0].ToString();
        waterSpell.text = obj[1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
