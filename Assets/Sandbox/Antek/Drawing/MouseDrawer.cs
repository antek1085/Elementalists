using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer drawingPrefab;
    
    private Coroutine _drawingCoroutine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (Input.GetMouseButtonUp(0))
            FinishLine();
    }

    private void StartDrawing()
    {
        if(_drawingCoroutine != null)
            StopCoroutine(_drawingCoroutine);

        _drawingCoroutine = StartCoroutine(DrawLine());
    }

    private IEnumerator DrawLine()
    {
        var drawer = Instantiate(drawingPrefab, transform);
        drawer.positionCount = 0;
        
        while (true)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition -= new Vector2(Screen.width / 2f, Screen.height / 2f);
            Debug.Log(mousePosition);
            //mousePosition.z = 0;

            drawer.positionCount++;
            drawer.SetPosition(drawer.positionCount - 1, mousePosition / 100f);
            
            yield return null;
        }
    }
    
    private void FinishLine()
    {
        StopCoroutine(_drawingCoroutine);
    }
}
