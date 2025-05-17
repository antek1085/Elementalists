using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 5f; // Prędkość obrotu w stopniach na sekundę.
    
    void Update()
    {
        // Obracamy obiekt wokół osi Y o 'rotationSpeed' stopni na sekundę, używając Time.deltaTime.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}