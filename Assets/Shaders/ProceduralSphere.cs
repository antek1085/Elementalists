using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class ProceduralSphere : MonoBehaviour
{
    public float radius = 1f;
    public int longitudeSegments = 20;
    public int latitudeSegments = 10;
    MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = GenerateSphere(radius, longitudeSegments, latitudeSegments);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            meshFilter.mesh = GenerateSphere(radius, longitudeSegments, latitudeSegments);
        }
    }

    Mesh GenerateSphere(float radius, int longitudeSegments, int latitudeSegments)
    {
        Mesh mesh = new Mesh();

        // Step 1: Generate Vertices
        Vector3[] vertices = new Vector3[(longitudeSegments + 1) * (latitudeSegments + 1)];
        Vector2[] uv = new Vector2[(longitudeSegments + 1) * (latitudeSegments + 1)];

        int vertexIndex = 0;
        for (int lat = 0; lat <= latitudeSegments; lat++)
        {
            float theta = Mathf.PI * lat / latitudeSegments; // Latitude angle
            float sinTheta = Mathf.Sin(theta);
            float cosTheta = Mathf.Cos(theta);

            for (int lon = 0; lon <= longitudeSegments; lon++)
            {
                float phi = 2 * Mathf.PI * lon / longitudeSegments; // Longitude angle
                float sinPhi = Mathf.Sin(phi);
                float cosPhi = Mathf.Cos(phi);

                // Spherical coordinates to Cartesian coordinates
                float x = radius * sinTheta * cosPhi;
                float y = radius * cosTheta;
                float z = radius * sinTheta * sinPhi;

                vertices[vertexIndex] = new Vector3(x, y, z);

                // Step 2: Calculate UVs
                float u = (float)lon / longitudeSegments;
                float v = 1 - (float)lat / latitudeSegments;

                uv[vertexIndex] = new Vector2(u, v);

                vertexIndex++;
            }
        }

        // Step 3: Generate Triangles
        int[] triangles = new int[longitudeSegments * latitudeSegments * 6];

        int triangleIndex = 0;
        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            for (int lon = 0; lon < longitudeSegments; lon++)
            {
                int topLeft = lat * (longitudeSegments + 1) + lon;
                int topRight = topLeft + 1;
                int bottomLeft = (lat + 1) * (longitudeSegments + 1) + lon;
                int bottomRight = bottomLeft + 1;

                // Reverse the order of triangles to fix backface culling
                triangles[triangleIndex++] = topLeft;
                triangles[triangleIndex++] = bottomLeft;
                triangles[triangleIndex++] = bottomRight;

                triangles[triangleIndex++] = topLeft;
                triangles[triangleIndex++] = bottomRight;
                triangles[triangleIndex++] = topRight;
            }
        }

        // Step 4: Set up Mesh data
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Step 5: Fix Normals
        mesh.RecalculateNormals();

        // Step 6: Manually flip normals if needed (in case winding order fix is not enough)
        /*FlipNormals(mesh);*/

        return mesh;
    }

    // Manually flip the normals if needed
    /*private void FlipNormals(Mesh mesh)
    {
        Vector3[] normals = mesh.normals;

        // Reverse the direction of each normal
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }

        // Assign the flipped normals back to the mesh
        mesh.normals = normals;
    }*/
}