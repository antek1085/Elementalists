using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;

public class SkyboxLightRotation : MonoBehaviour
{
    
    [SerializeField] private Light _directionalLight;
    [SerializeField] private float _rotationSpeed;
    Quaternion originalRotation;

    // Update is called once per frame

    void Awake()
    {
        _directionalLight = GetComponent<Light>();
        originalRotation.x = _directionalLight.transform.rotation.x;
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotationSpeed * 0.5f);
        
        _directionalLight.transform.rotation *= Quaternion.Euler(0, Time.deltaTime * _rotationSpeed, 0);
    }
}
