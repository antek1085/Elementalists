using UnityEngine;

public class Quest : MonoBehaviour
{
    QuetsController controller;
    
    [SerializeField] int questId;
    [SerializeField] string questInfo;

    [SerializeField] private bool isEndingQuest;
    
    void Start()
    {
        controller = QuetsController.instance;
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
