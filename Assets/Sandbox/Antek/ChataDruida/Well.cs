using UnityEditor;
using UnityEngine;

public class Well : RenovateItem
{
   public override void OnSpellHit()
   {
      base.OnSpellHit();
      transform.GetChild(1).gameObject.SetActive(true);
   }
}
