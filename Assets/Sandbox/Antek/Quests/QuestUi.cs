using TMPro;
using UnityEngine;

public class QuestUi : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI questInfo;

    void Awake()
    {
        questInfo = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
