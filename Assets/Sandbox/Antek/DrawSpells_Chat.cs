using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpells_Chat : MonoBehaviour
{
    [SerializeField] Camera drawCamera;
    [SerializeField] GameObject brush;

    LineRenderer lineRenderer;

    List<GameObject> drawList = new List<GameObject>();

    
    Vector2 lastPos;


    void Update()
    {
        Draw();
    }

    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = drawCamera.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos != lastPos)
            {
                AddPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else
        {
            lineRenderer = null;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            RemoveLines();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenShotHandler.TakeScreenshot_Static();
        }
    }
    
    
    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        drawList.Add(brushInstance);
        lineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = drawCamera.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0,mousePos);
        lineRenderer.SetPosition(1,mousePos);
    }
    
    void AddPoint(Vector2 pointPos)
    {
        lineRenderer.positionCount++;
        int positionIndex = lineRenderer.positionCount - 1;
        lineRenderer.SetPosition(positionIndex, pointPos);
    }
    
    void RemoveLines()
    {
        for (int i = 0; i < drawList.Count;)
        {
            Destroy(drawList[i]);
            drawList.Remove(drawList[i]);
        }
    }
}
