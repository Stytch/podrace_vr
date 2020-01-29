using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private TrackGenerator trackG;
    private MapGenerator mapG;
    private MeshGenerator meshG;

    [HideInInspector]
    public bool cleanEnd = true;

    [HideInInspector]
    public Vector3 EndPosition;

    public Vector3 ScaleTrack = new Vector3(35, 35, 35);

    private bool IniVariable()
    {
        trackG = transform.GetComponent<TrackGenerator>();
        mapG = transform.GetComponent<MapGenerator>();
        meshG = transform.GetComponent<MeshGenerator>();

        if (trackG == null || mapG == null || meshG == null) return false;

        return true;
    }

    public void GenerateTrack()
    {
        if(IniVariable())
        {
            trackG.GenerateTrack(HideTracks:true);
            mapG.Generate();
            meshG.GenerateAllMesh();

            if (cleanEnd) CleanEnd();
            meshG.allTrackMesh.transform.localScale = ScaleTrack;
        }
        else
        {
            Debug.Log("TRACKMANAGER:ERROR INIVARIABLE");
        }
    }

    //Clean All
    public void CleanAll()
    {
        trackG.Clean();
        mapG.Clean();
        meshG.Clean();
    }

    public void CleanEnd()
    {
        int secure = mapG.SecureChunk;
        int xMax = meshG.textureChunks.GetLength(0);
        int zMax = meshG.textureChunks.GetLength(1);

        for (int z = zMax - 1 - secure; z < zMax; z++)
        {
            for (int x = 0; x < xMax; x++)
            {
#if UNITY_EDITOR
                DestroyImmediate(meshG.textureChunks[x, z].MeshObject);
#else
                Destroy(meshG.textureChunks[x, z].MeshObject);
#endif
            }
        }
    }

    public Vector3 GetStartPosition()
    {
        if (trackG.all_tracks != null && trackG.all_tracks.Count > 0) return trackG.all_tracks[0].a0;
        else return Vector3.zero;
    }

    public Vector3 GetEndPosition()
    {
        if(cleanEnd)
        {
            int secure = mapG.SecureChunk;
            int xMax = meshG.textureChunks.GetLength(0);
            int zMax = meshG.textureChunks.GetLength(1);
            int z = zMax - 2 - secure;

            int medianCount =0;
            Vector3 medianVec = Vector3.zero;
            for (int x = 0; x < xMax; x++)
            {
                if(mapG.textureChunks[x, z].isCanyonPart)
                {
                    Mesh mesh = mapG.textureChunks[x, z].MeshObject.GetComponent<MeshFilter>().sharedMesh;
                    Vector3[] vertices = mesh.vertices;

                    int count = 0;
                    Vector3 vec = Vector3.zero;
                    foreach (Vector3 v in vertices)
                    {
                        if (v.y < 0.5 && v.z == -12)
                        {
                            count++;
                            vec += v;
                        }
                    }
                    if(count != 0)
                    {
                        Vector3 meshPosition = mapG.textureChunks[x, z].MeshObject.transform.position;
                        meshPosition -= (vec / count) * mapG.textureChunks[x, z].MeshObject.transform.localScale.x;

                        medianVec += meshPosition;
                        medianCount++;
                    }
                }
            }

            if(medianCount > 0)
            {
                return (medianVec / medianCount);
            }
        }
        else
        {
            return trackG.all_tracks[trackG.all_tracks.Count - 1].a1;
        }

        return Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
