using System.Collections.Generic;
using UnityEngine;

public class QuestJournalControler : MonoBehaviour
{
    List<QuestJournal> questJournals = new List<QuestJournal>();

    public static QuestJournalControler instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        transform.parent.transform.gameObject.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<QuestJournal>() != null)
            {
                questJournals.Add(transform.GetChild(i).GetComponent<QuestJournal>());
            }
        }
    }

    // Update is called once per frame
    public void AddNewQuestJournal(int id)
    {
        for (int i = 0; i < questJournals.Count; i++)
        {
            if (questJournals[i].questId == 0)
            {
                questJournals[i].questId = id;
                questJournals[i].NewQuest();
                break;
            }
        }
    }
    public void EndQuestJournal(int id)
    {
        for (int i = 0; i < questJournals.Count; i++)
        {
            if (questJournals[i].questId == id)
            {
                questJournals[i].EndQuest();
            }
        }
    }
}
