using System;
using System.Collections;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [SerializeField] Color color;
    Color originalColor;
    [SerializeField] float speed;
    float startTime;
    bool fogChange;

    void Start()
    {
        startTime = Time.time;
        fogChange = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            originalColor = RenderSettings.fogColor;
            fogChange = true;
        }
    }

    void Update()
    {
        if (fogChange)
        { 
            float tick = (Time.time - startTime) * speed;
            RenderSettings.fogColor = Color.Lerp(originalColor, color, tick);
            Debug.Log(tick);
            if (tick >= 1)
            {
                fogChange = false;
            }
        }
    }
}
