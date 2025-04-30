using System;
using UnityEngine;

public class QuestUIControler : MonoBehaviour
{
    public static QuestUIControler instance;
    [SerializeField] GameObject questUiPrefab;
    

    private void Awake()
    {
        instance = this;
    }

    public void UpdateQuest(int questId,string questInfo ,bool isCompleted)
    {
        if (isCompleted) //Remove Quest
        {
            for (int t = 0; t < transform.childCount ; t++)
            {
                int id = transform.GetChild(t).GetComponent<QuestUi>().id;
                if (id == questId)
                {
                    Destroy(transform.GetChild(t).gameObject); 
                }
            }
        }
        else //Add Quest
        {
           var createdObject = Instantiate(questUiPrefab, transform);
           createdObject.GetComponent<QuestUi>().id = questId;
           createdObject.GetComponent<QuestUi>().questInfo.text = questInfo;
           createdObject.transform.SetParent(transform);
           createdObject.transform.SetAsLastSibling();
        }
    }
}
