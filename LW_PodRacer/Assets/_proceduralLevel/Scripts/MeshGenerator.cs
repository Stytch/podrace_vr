using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    //VARIABLES
    private MapGenerator mapGen;
    [HideInInspector]
    public textureChunk[,] textureChunks;
    [HideInInspector]
    public GameObject allTrackMesh = null;

    private int xChunk;
    private int zChunk;

    public bool debugModeTexture = true;
    public Material trackMaterial;
    public Color mapColor;
    public float HeightMapFactor = 30f;

    //FUNCTIONS
    public void Clean()
    {

        if (allTrackMesh != null)
        {
#if UNITY_EDITOR
            DestroyImmediate(allTrackMesh);
#else
            Destroy(allTrackMesh);
#endif
        }
    }

    public bool IniVariable()
    {
        mapGen = transform.GetComponent<MapGenerator>();
        if (mapGen != null)
        {
            allTrackMesh = new GameObject();
            allTrackMesh.name = "allTrackMesh";

            textureChunks = mapGen.textureChunks;
            xChunk = textureChunks.GetLength(0);
            zChunk = textureChunks.GetLength(1);
            
            return true;
        }
        else
        {
            Debug.Log("WARNING:MAPGENERATOR N'EST PAS ATTACHER");
            return false;
        }
    }

    public MeshData GenerateMesh(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width,height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //4 car 100 (10 * 10) / 25 (résolution) 50 pour test
                meshData.vertices[vertexIndex] = new Vector3(-(topLeftX+x), heightMap[x, y]*HeightMapFactor /*+ Random.Range(0.0f,1.0f)*/, (topLeftZ-y));

                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                //ADD TRIANGLES
                if(x < width -1 && y < height -1)
                {
                    meshData.AddTriangles(vertexIndex, vertexIndex +width, vertexIndex + width + 1);
                    meshData.AddTriangles(vertexIndex + width + 1, vertexIndex + 1, vertexIndex);
                }

                vertexIndex++;
            }
        }

        //FLAT SHADING
        meshData.FlatShading();

        return meshData;
    }

    //MAIN
    public void GenerateAllMesh()
    {
        if(IniVariable())
        {
            for (int z = 0; z < zChunk; z++) 
            {
                for (int x = 0; x < xChunk; x++)
                {
                    textureChunk tc = textureChunks[x, z];
                    MeshData meshData = GenerateMesh(tc.heightMap);

                    GameObject go = new GameObject();
                    if (allTrackMesh != null) go.transform.parent = allTrackMesh.transform;

                    go.layer = 8;

                    go.name = "Track Mesh [" + x + "," + z + "]";

                    go.transform.position = tc.plane.transform.position;
                    go.transform.rotation = tc.plane.transform.rotation;
                    go.transform.localScale = new Vector3((tc.plane.transform.localScale.x ) * ((float)(tc.chunkScale) / tc.resolutionMap) + tc.scaleFactor,1, (tc.plane.transform.localScale.z ) * ((float)( tc.chunkScale) / tc.resolutionMap) + tc.scaleFactor);

                    //MESH FILTER
                    MeshFilter mf = go.AddComponent<MeshFilter>(); 
                    mf.sharedMesh = meshData.CreateMesh();

                    //MESH COLLIDER
                    MeshCollider mc = go.AddComponent<MeshCollider>();
                    mc.sharedMesh = mf.sharedMesh;

                    //MESH RENDERER
                    MeshRenderer mr = go.AddComponent<MeshRenderer>();

                    if(trackMaterial != null)
                    {
                        mr.material = trackMaterial;
                        if (debugModeTexture) mr.material.mainTexture = mapGen.TextureFromPlane(tc.pixels);                        
                    }
                    else
                    {
                        mr.sharedMaterial = new Material(Shader.Find("Standard"));
                        if (debugModeTexture) mr.sharedMaterial.mainTexture = mapGen.TextureFromPlane(tc.pixels);
                        else
                        {
                            mr.sharedMaterial.color = mapColor;
                        }
                    }

                    textureChunks[x, z].MeshObject = go;
                }
            }
        }
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    
    int trianglesIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];

        uvs = new Vector2[meshWidth * meshHeight];
    }

    public void AddTriangles(int a, int b, int c)
    {
        triangles[trianglesIndex] = a;
        triangles[trianglesIndex + 1] = b;
        triangles[trianglesIndex + 2] = c;

        trianglesIndex += 3;
    }

    Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[vertices.Length];
        int triangleCount = triangles.Length / 3;
        for (int i =0; i <triangleCount; i++)
        {
            int normalTrianglesIndex = i * 3;
            int vertexIndexA = triangles[normalTrianglesIndex];
            int vertexIndexB = triangles[normalTrianglesIndex + 1];
            int vertexIndexC = triangles[normalTrianglesIndex + 2];

            Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);
            vertexNormals[vertexIndexA] += triangleNormal;
            vertexNormals[vertexIndexB] += triangleNormal;
            vertexNormals[vertexIndexC] += triangleNormal;
        }

        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = vertices[indexA];
        Vector3 pointB = vertices[indexB];
        Vector3 pointC = vertices[indexC];

        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;

        return Vector3.Cross(sideAB, sideAC).normalized;
    }

    //Fonction qui donne un effet de Low Poly
    public void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[triangles.Length];
        Vector2[] flatShadedUvs = new Vector2[triangles.Length];

        for (int i =0; i < triangles.Length; i++)
        {
            flatShadedVertices[i] = vertices[triangles[i]];
            flatShadedUvs[i] = uvs[triangles[i]];
            triangles[i] = i;
        }

        vertices = flatShadedVertices;
        uvs = flatShadedUvs;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        //mesh.normals = CalculateNormals();
        return mesh;
    }
}
