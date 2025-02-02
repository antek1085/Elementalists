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
        EQEvent.current.OnSlotChangedUI += SlotChangedUI;
        EQEvent.current.OnItemStateChanged += StateChanged;
    }
    
    
    
    //True == added to EQ False == used/dropped and anything else
    void StateChanged(bool state)
    {
        switch (state)
        {
            case true:
                transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().sprite = EQList.objectList[choosedSlot].GetComponent<ItemInformation>().icon;
                var color2 = transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().color;
                color2.a = 1;
                transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().color = color2;
                break;
            case false:
                transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().sprite = null;
                var color1 = transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().color;
                color1.a = 0;
                transform.GetChild(choosedSlot).GetChild(0).GetComponent<Image>().color = color1;
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
