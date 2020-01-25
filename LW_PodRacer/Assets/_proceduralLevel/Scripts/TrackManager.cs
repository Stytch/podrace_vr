using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private TrackGenerator trackG;
    private MapGenerator mapG;
    private MeshGenerator meshG;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
