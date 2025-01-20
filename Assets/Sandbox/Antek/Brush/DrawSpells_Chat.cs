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
    bool isCasting;

    void Awake()
    {
    }
    void Start()
    {
        isCasting = false;
        DrawingSpellsEvent.current.OnSpellStartCasting += SpellStartCasting;
        DrawingSpellsEvent.current.OnSpellCast += floats => isCasting = false;
    }
    void SpellStartCasting()
    {
        isCasting = !isCasting;
        if (isCasting)
        {
            RemoveLines();
        }
    }


    void Update()
    {
        Draw();
        
        //Cleaning the canvas of spell
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                return;
            }
            RemoveLines();
        }
        
        //Sending information to Neural Network
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenShotHandler.TakeScreenshot_Static();
        }
    }

    void Draw()
    {
        
        //Drawing the spell
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

            Ray ray = drawCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                CreateBrush(hit.point);
            }
        }
        
        if(lineRenderer == null) return;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = drawCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.point != lastPos)
                {
                    AddPoint(hit.point);
                    lastPos = hit.point;
                }
            }
        }
        else
        {
            lineRenderer = null;
        }
    }
    
    
    void CreateBrush(Vector3 hitpoint)
    {
        GameObject brushInstance = Instantiate(brush);
        drawList.Add(brushInstance);
        lineRenderer = brushInstance.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0,hitpoint);
        lineRenderer.SetPosition(1,hitpoint);
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

    void OnDisable()
    {
        RemoveLines();
    }
}
