using TMPro;
using UnityEngine;

public class QuestUi : MonoBehaviour
{
    public int id;
    [SerializeField] TextMeshProUGUI questInfo;
    [SerializeField] TextMeshProUGUI name;
    QuestControler questController;

    void Awake()
    {
        questController = QuestControler.instance;
    }
    
    void Start()
    {
        questInfo.text = questController.GetDescription(id);
        name.text = questController.GetName(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
