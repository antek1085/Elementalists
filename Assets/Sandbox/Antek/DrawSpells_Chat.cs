using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpells_Chat : MonoBehaviour
{
    [SerializeField] Camera drawCamera;
    [SerializeField] GameObject brush;

    LineRenderer lineRenderer;

    List<GameObject> drawList = new List<GameObject>();

    
    Vector3 lastPos;
    public LayerMask layerMask;


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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(hit.transform.position);
                if (mousePosition != lastPos)
                {
                    AddPoint(mousePosition);
                    lastPos = mousePosition;
                }
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
