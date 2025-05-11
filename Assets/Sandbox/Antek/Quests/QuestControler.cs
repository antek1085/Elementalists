using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestControler : MonoBehaviour
{
    public static QuestControler instance;
    
    Dictionary<int,string[]> quests = new Dictionary<int,string[]>();
    
    private QuestUIControler questUIControler;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        questUIControler = QuestUIControler.instance;
    }

    public void StartNewQuest(int questId, string[] questInfo)
    {
        quests.Add(questId,questInfo);
        questUIControler.UpdateQuest(questId,false);
    }

    public void EndQuest(int questId)
    {
        if (quests.ContainsKey(questId))
        {
            questUIControler.UpdateQuest(questId,true);   
        }
    }

    public string GetName(int questId)
    {
        return quests[questId][0];
    }
    public string GetDescription(int questId)
    {
        return quests[questId][1];
    }
}
