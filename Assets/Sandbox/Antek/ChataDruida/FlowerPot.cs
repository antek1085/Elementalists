using UnityEngine;

public class FlowerPot : RenovateItem
{
    public override void OnSpellHit()
    {
        base.OnSpellHit();
        transform.GetChild(1).gameObject.SetActive(false); // turn off old model
        transform.GetChild(2).gameObject.SetActive(true); // turn on new model
    }

    public override void Awake()
    {
        base.Awake();
    }
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
