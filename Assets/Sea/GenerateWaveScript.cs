using UnityEngine;
using System.Collections;

public class GenerateWaveScript : MonoBehaviour {

    [Range (0.1f, 20.0f)]
    public float heightScale = 5.0f;

    [Range(0.1f, 40.0f)]
    public float detailScale = 5.0f;

    private Mesh myMesh;
    private Vector3[] vertices;

    void Update()
    {
        GenerateWave();
    }

    void GenerateWave()
    {
        myMesh = this.GetComponent<MeshFilter>().mesh;
        vertices = myMesh.vertices;

        int counter = 0;    //i
        int yLevel = 0;     //j

        for(int i = 0; i < 11; i++)
        {
            for(int j = 0; j < 11; j++)
            {
                CalculationMethod(counter, yLevel);
                counter++;
            }
            yLevel++;
        }
        myMesh.vertices = vertices;
        myMesh.RecalculateBounds();
        myMesh.RecalculateNormals();

        Destroy(gameObject.GetComponent<MeshCollider>());
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = null;
        collider.sharedMesh = myMesh;
    }

    public bool waves = false;
    public float wavesSpeed = 5.0f;
    void CalculationMethod(int i, int j)
    {
        if (waves)
        {
            vertices[i].z = Mathf.PerlinNoise(
                Time.time/wavesSpeed + 
                (vertices[i].x + this.transform.position.x) / detailScale, 
                Time.time/wavesSpeed +
                (vertices[i].y + transform.position.y) / detailScale) 
                * heightScale;
            vertices[i].z -= j;
        }
        else
        {
            vertices[i].z = Mathf.PerlinNoise(
                (vertices[i].x + this.transform.position.x) / detailScale,
                (vertices[i].y + transform.position.y) / detailScale)
                * heightScale;
            vertices[i].z -= j;
        }
    }

}
