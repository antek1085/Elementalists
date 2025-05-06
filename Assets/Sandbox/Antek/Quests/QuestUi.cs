using TMPro;
using UnityEngine;

public class QuestUi : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI questInfo;
    QuestControler questController;

    void Awake()
    {
        questInfo = GetComponent<TextMeshProUGUI>();
        questController = QuestControler.instance;
    }
    
    void Start()
    {
        questInfo.text = questController.GetDescription(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
