using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMesh : MonoBehaviour {
    private Vector3[] vertices;
    private int[] triangles;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private void Awake()
    {

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
    }

    public void Generate(Ground ground, LevelData levelData)
    {
        SimpleNoiseFilter noise = new SimpleNoiseFilter(levelData);
        float[,] heightMap = new float[ground.X, ground.Z];
        MeshData md;

        if (levelData.useNoise)
        {
            for (int x = 0; x < ground.X; x++)
            {
                for (int y = 0; y < ground.Z; y++)
                {
                    heightMap[x, y] = noise.Evaluate(new Vector3(x, 0, y));
                }

            }
        }

        md = GenerateTerrainMesh(heightMap);

        meshFilter.mesh = md.CreateMesh();
        meshCollider.sharedMesh = meshFilter.mesh;
    }

    public static MeshData GenerateTerrainMesh(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x, y], topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

            vertexIndex++;
            }
        }

        return meshData;

    }
    

    public class MeshData
    {
        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;

        int triangleIndex;

        public MeshData(int meshWidth, int meshHeight)
        {
            vertices = new Vector3[meshWidth * meshHeight];
            uvs = new Vector2[meshWidth * meshHeight];
            triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            return mesh;
        }

    }

    public struct Ground
    {
        int x, z;

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = Math.Abs(value);
            }
        }

        public int Z
        {
            get
            {
                return z;
            }

            set
            {
                z = Math.Abs(value);
            }
        }
    }
}
