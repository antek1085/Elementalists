using System;
using UnityEngine;
using UnityEngine.UI;

public class EQUIController : MonoBehaviour
{
    public Color colorUnusedSlot;
    public Color colorUsedSlot;
    Color color;
    int choosedSlot;

    [SerializeField] SO_GameObject_List EQList;
    void Start()
    {
        EQEvent.current.onSlotChangedUI += SlotChangedUI;
        EQEvent.current.onItemStateChanged += StateChanged;
    }
    
    
    
    //True == added to EQ False == used/dropped and anything else
    void StateChanged(bool state)
    {
        switch (state)
        {
            case true:
                transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().sprite = EQList.objectList[choosedSlot].GetComponent<ItemInformation>().icon;
                break;
            case false:
                transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().sprite = null;
                break;
        }
    }
    void Awake()
    {
        color = transform.GetChild(0).GetComponent<Image>().color;
        color = colorUsedSlot;
        choosedSlot = 0;
        transform.GetChild(0).GetComponent<Image>().color = color;
    }
    void SlotChangedUI(int slot)
    {
        if (slot != choosedSlot)
        {
            
            //Changes last item to unused
            color = transform.GetChild(choosedSlot).GetComponent<Image>().color;
            color = colorUnusedSlot;
            transform.GetChild(choosedSlot).GetComponent<Image>().color = color;
            
            choosedSlot = slot;
            
            
            //Changes new item to used
            color = transform.GetChild(choosedSlot).GetComponent<Image>().color;
            color = colorUsedSlot;
            transform.GetChild(choosedSlot).GetComponent<Image>().color = color;
        }
    }
}
