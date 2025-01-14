using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Object List", menuName = "Game Object List")]
public class SO_GameObject_List : ScriptableObject
{
    public List<GameObject> objectList = new List<GameObject>();
}
