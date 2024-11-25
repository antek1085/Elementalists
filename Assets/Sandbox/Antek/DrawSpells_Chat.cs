using UnityEngine;

public class DrawSpells_Chat : MonoBehaviour
{
    public LineRenderer lineRenderer; // Assign in the Inspector
    public Camera mainCamera;        // Assign the main camera
    public float distanceFromCamera = 0f; // Distance in world space from the camera

    private int currentPointIndex = 0;

    void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer not assigned!");
            return;
        }

        // Initialize LineRenderer
        lineRenderer.positionCount = 0;

        // Assign the main camera if not already assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button starts drawing
        {
            lineRenderer.positionCount = 0; // Reset line
            currentPointIndex = 0;
        }

        if (Input.GetMouseButton(0)) // Hold left mouse button to draw
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = distanceFromCamera; // Set the depth

            // Convert screen space to world space
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // Add a new point to the line
            AddPoint(worldPosition);
        }
    }

    private void AddPoint(Vector3 point)
    {
        lineRenderer.positionCount = currentPointIndex + 1;
        lineRenderer.SetPosition(currentPointIndex, point);
        currentPointIndex++;
    }
}
