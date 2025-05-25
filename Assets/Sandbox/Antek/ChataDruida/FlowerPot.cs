using UnityEngine;

public class FlowerPot : RenovateItem
{
    [Header("Objects")]
    [SerializeField] GameObject objectToEnable;
    [SerializeField] GameObject objectToDisable;
    public override void OnSpellHit()
    {
        base.OnSpellHit();
        objectToDisable.SetActive(false); // turn off old model
        objectToEnable.SetActive(true); // turn on new model
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
