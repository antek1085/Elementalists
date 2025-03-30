using UnityEngine;

public class ChestUiController : MonoBehaviour
{
    GameObject UI;

    void Start()
    {
        UI = transform.GetChild(0).gameObject;
        Debug.Log(UI.name);
    }
    public void ChestUI()
    {
        UI.SetActive(!UI.activeSelf);
    }
}
