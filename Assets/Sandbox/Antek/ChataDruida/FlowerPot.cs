using UnityEngine;

public class FlowerPot : RenovateItem
{
    public override void OnSpellHit()
    {
        base.OnSpellHit();
        transform.GetChild(1).gameObject.SetActive(false); // turn off old model
        transform.GetChild(2).gameObject.SetActive(true); // turn on new model
    }
}
