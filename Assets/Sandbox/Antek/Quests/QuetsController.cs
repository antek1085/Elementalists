using System;
using System.Collections.Generic;
using UnityEngine;

public class QuetsController : MonoBehaviour
{
    public static QuetsController instance;
    Dictionary<int,string> quests = new Dictionary<int,string>();
    private QuestUIControler questUIControler;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        questUIControler = QuestUIControler.instance;
    }

    public void StartNewQuest(int questId, string questInfo)
    {
        quests.Add(questId,questInfo);
        questUIControler.UpdateQuest(questId,questInfo,false);
    }

    public void EndQuest(int questId)
    {
        quests.Remove(questId); 
        questUIControler.UpdateQuest(questId,null,true);
    }
}
