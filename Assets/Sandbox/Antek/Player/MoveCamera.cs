using System;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform mainCamera;

    // Update is called once per frame

  
    void Update()
    {
        mainCamera.position = transform.position;
    }
}
