using System;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DrawSpells_Chat : MonoBehaviour
{
    [SerializeField] Camera drawCamera;
    [SerializeField] GameObject brush;

    [Header("FMOD Events")]
    [SerializeField] EventReference drawSound;
    [SerializeField] EventReference eraseSound;

    LineRenderer lineRenderer;
    EventInstance drawSoundInstance;

    List<GameObject> drawList = new List<GameObject>();

    Vector3 lastPos;
    public LayerMask layerMask;
    bool isCasting;

    void Awake()
    {
        CreateDrawSoundInstance();
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

        // Cleaning the canvas of spell
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.Mouse0)) return;
            RemoveLines();
            RuntimeManager.PlayOneShot(eraseSound);
        }

        // Sending information to Neural Network
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScreenShotHandler.TakeScreenshot_Static();
        }
    }

    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = drawCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                CreateBrush(hit.point);
                drawSoundInstance.start(); // Start the looping sound
            }
        }

        if (lineRenderer != null && Input.GetKey(KeyCode.Mouse0))
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

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAndRecreateDrawSound();
            lineRenderer = null;
        }
    }

    void CreateBrush(Vector3 hitpoint)
    {
        GameObject brushInstance = Instantiate(brush);
        drawList.Add(brushInstance);
        lineRenderer = brushInstance.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, hitpoint);
        lineRenderer.SetPosition(1, hitpoint);
    }

    void AddPoint(Vector3 pointPos)
    {
        lineRenderer.positionCount++;
        int positionIndex = lineRenderer.positionCount - 1;
        lineRenderer.SetPosition(positionIndex, pointPos);
    }

    void RemoveLines()
    {
        foreach (var line in drawList)
        {
            Destroy(line);
        }
        drawList.Clear();
    }

    void StopAndRecreateDrawSound()
    {
        drawSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        drawSoundInstance.release();
        CreateDrawSoundInstance();
    }

    void CreateDrawSoundInstance()
    {
        drawSoundInstance = RuntimeManager.CreateInstance(drawSound);
    }

    void OnDisable()
    {
        RemoveLines();
        drawSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Ensure sound stops when script is disabled
        drawSoundInstance.release(); // Release FMOD resources
    }
}
