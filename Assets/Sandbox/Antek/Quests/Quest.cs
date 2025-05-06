using UnityEngine;
using UnityEngine.Serialization;

public class Quest : MonoBehaviour
{
    QuestControler controller;
    
    [SerializeField] int questId;
    [FormerlySerializedAs("questInfo")]
    [SerializeField] string questDescription;
    [SerializeField] string questName;
    string[] questInfo;
    [SerializeField] private bool isEndingQuest;
    
    void Start()
    {
        controller = QuestControler.instance;
        questInfo = new string[2];
        questInfo[0] = questName;
        questInfo[1] = questDescription;
    }

    public void ControlQuest()
    {
        if (isEndingQuest)
        {
            EndQuest();
        }
        else
        {
            StartQuest();
        }
    }

    // Update is called once per frame
     void StartQuest()
    {
        controller.StartNewQuest(questId, questInfo);
    }

     void EndQuest()
    {
        controller.EndQuest(questId);
    }
}
