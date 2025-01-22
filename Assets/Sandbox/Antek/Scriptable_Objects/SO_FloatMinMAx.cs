using UnityEngine;
[CreateAssetMenu(fileName = "New Game MinMaxFloat", menuName = "MinMaxFloat")]
public class SO_FloatMinMAx : ScriptableObject
{
    [Range(0,1)]
    public float value;
}
