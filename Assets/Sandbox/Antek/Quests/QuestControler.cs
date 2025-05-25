using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestControler : MonoBehaviour
{
    public static QuestControler instance;
    
    public Dictionary<int,string[]> quests = new Dictionary<int,string[]>();
    
    private QuestUIControler questUIControler;
    private QuestJournalControler questJournalControler;

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
        if (questJournalControler == null)
        {
            questJournalControler = QuestJournalControler.instance;   
        }
        
        quests.Add(questId,questInfo);
        questUIControler.UpdateQuest(questId,false);
        questJournalControler.AddNewQuestJournal(questId);
    }

    public void EndQuest(int questId)
    {
        if (quests.ContainsKey(questId))
        {
            questUIControler.UpdateQuest(questId,true);   
            questJournalControler.EndQuestJournal(questId);
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
