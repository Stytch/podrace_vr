using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsGenerator : MonoBehaviour
{
    //VARIABLE
    private TrackGenerator tg;
    private MeshGenerator mg;

    [HideInInspector]
    public GameObject allProps;

    public bool debugMode = false;

    //path
    private string pathRock = "Assets/Blender/rock_final/rock Variant.prefab";

    //PREFAB
    public GameObject rock;
    public GameObject tower;
    public GameObject house;
    public GameObject ark;

    //FUNCTION
    //INIT VARIABLE
    bool IniVariable()
    {
        tg = transform.GetComponent<TrackGenerator>();
        if (tg == null) return false;

        mg = transform.GetComponent<MeshGenerator>();
        if (mg == null) return false;

        return true;
    }

    private GameObject InstanceProps(GameObject go, Vector3 pos, float yRot, Vector3 sca)
    {
        GameObject instance = Instantiate(go) as GameObject;
        instance.transform.position = pos;
        instance.transform.eulerAngles = new Vector3(0, yRot, 0);
        instance.transform.localScale = sca;
        instance.transform.parent = allProps.transform;
        return instance;
    }

    void TextureDebugMode()
    {
        MeshRenderer[] allMat = allProps.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in allMat)
        {
            m.material.color = Color.red;
        }
    }

    public void GenerateAllProps()
    {
        if (IniVariable())
        {
            if (allProps != null) Destroy(allProps);
            allProps = new GameObject();
            allProps.name = "AllProps";
            if (mg.allTrackMesh != null) allProps.transform.parent = mg.allTrackMesh.transform;

            foreach (Track_part t in tg.all_tracks)
            {
                
                //ARCHE
                int ind = Random.Range(-10 * t.points.Count, t.points.Count);
                if (ind >= 0)
                {
                    Point point = t.points[ind];
                    Vector3 position = point.position + Vector3.down * 7; // descend l'arche pour qu'elle touche le sol

                    GameObject a = InstanceProps(ark, position, 0.0f, new Vector3(3, 3, 3));
                    a.transform.forward = point.tangent;
                }

                //ROCHE
                foreach (Point p in t.points)
                {
                    int test = Random.Range(0, 1000);
                    if (test < 50)
                    {
                        float sca = Random.Range(0.1f, 2f);
                        Vector3 pos = p.position + p.normal * Random.Range(0.1f, 10.0f) + Vector3.down *5;
                        GameObject r= InstanceProps(rock,pos, Random.Range(0, 360), new Vector3(sca, sca * Random.Range(1,4), sca));
                        
                    }
                }
            }

            if (debugMode) TextureDebugMode();
        }
        else Debug.Log("ERROR INI VARIABLE: PROPS GENERATOR");
    }

    //EVENT
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
