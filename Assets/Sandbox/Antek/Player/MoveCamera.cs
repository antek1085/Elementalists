using System;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] GameObject lookAt;

    // Update is called once per frame

    void Awake()
    {
        mainCamera.rotation = Quaternion.Euler(1.5f, -90f, 0f); 
    }


    void Update()
    {
        mainCamera.position = transform.position;
    }
}
