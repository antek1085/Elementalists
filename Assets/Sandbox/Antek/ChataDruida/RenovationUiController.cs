using System;
using TMPro;
using UnityEngine;

public class RenovationUiController : MonoBehaviour
{
    public int numberOfObstacles;
    public int obstacleRenovatedCounter;
    [SerializeField] GameObject parentTextObject;
    private TextMeshProUGUI obstaclesRenovatedText;
    QuestControler questControler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        parentTextObject.SetActive(false);
        obstaclesRenovatedText = parentTextObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        obstaclesRenovatedText.text = obstacleRenovatedCounter.ToString();
    }
    void Start()
    {
        parentTextObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = numberOfObstacles.ToString();
        questControler = QuestControler.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& questControler.quests.ContainsKey(3))
        {
            parentTextObject.SetActive(true);
            numberOfObstacles = EndOfDemo.instance.itemToFix;
            parentTextObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = numberOfObstacles.ToString();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentTextObject.SetActive(false);
        }
    }

    public void UpdateRenovationUI()
    {
        obstacleRenovatedCounter++;
        obstaclesRenovatedText.text = obstacleRenovatedCounter.ToString();
        if (obstacleRenovatedCounter == numberOfObstacles)
        {
            parentTextObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    
    
}
