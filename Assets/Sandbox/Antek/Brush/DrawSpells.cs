using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpells : MonoBehaviour
{
    [SerializeField] Camera drawCamera;

    [SerializeField] GameObject brush;

    List<GameObject> drawList = new List<GameObject>();

    private LineRenderer lineRenderer;

    Vector3 lastPos;
    public float distanceFromCamera = 5f;


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
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = distanceFromCamera;
            //Vector2 mousePos = drawCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 worldPosition = drawCamera.ScreenToWorldPoint(mousePosition);

            if (worldPosition != lastPos)
            {
                AddPoint(worldPosition);
                lastPos = worldPosition;
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

        //Vector2 mousePos = drawCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = drawCamera.ScreenToWorldPoint(Input.mousePosition);
        
        lineRenderer.SetPosition(0,mousePos);
        lineRenderer.SetPosition(1,mousePos);
    }

    void AddPoint(Vector3 pointPos)
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
