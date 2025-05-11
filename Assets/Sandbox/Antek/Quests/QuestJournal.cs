using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestJournal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image image;
    private QuestControler controler;

    public int questId;
    
    void Start()
    {
        controler = QuestControler.instance;
    }

    public void NewQuest()
    {
        nameText.text = controler.GetName(questId);
        descriptionText.text = controler.GetDescription(questId);
    }
    
    public void EndQuest()
    {
        image.enabled = true;
    }
}
