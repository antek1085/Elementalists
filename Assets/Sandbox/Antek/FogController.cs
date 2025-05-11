using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float speed;
    bool fogChange;

    void Start()
    {
        fogChange = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fogChange = true;
        }
    }

    void Update()
    {
        if (fogChange)
        { 
             DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, color, speed);  
            if (RenderSettings.fogColor == color)
            {
                fogChange = false;
            }
        }
    }
}
