using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsGenerator : MonoBehaviour
{
    //VARIABLE
    private TrackGenerator tg;
    private GameObject allProps;

    public bool debugMode = false;

    //path
    private string pathRock = "Prefab/props/rock/rock";
    //FUNCTION
    //INIT VARIABLE
    bool IniVariable()
    {
        tg = transform.GetComponent<TrackGenerator>();
        if (tg == null) return false;

        return true;
    }

    private GameObject InstanceProps(Vector3 pos, float yRot, Vector3 sca)
    {
        GameObject instance = Instantiate(Resources.Load(pathRock, typeof(GameObject))) as GameObject;
        instance.transform.position = pos;
        instance.transform.eulerAngles += new Vector3(0, yRot, 0);
        instance.transform.localScale = sca;

        return instance;
    }

    void TextureDebugMode()
    {
        MeshRenderer[] allMat = allProps.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in allMat)
        {
            m.sharedMaterial.color = Color.red;
        }
    }

    public void GenerateAllProps()
    {
        if (IniVariable())
        {
            if (allProps != null) Destroy(allProps);
            allProps = new GameObject();
            allProps.name = "AllProps";

            foreach (Track_part t in tg.all_tracks)
            {
                foreach (Point p in t.points)
                {
                    int test = Random.Range(0, 10000);
                    if (test < 50)
                    {
                        float sca = Random.Range(0.1f, 2f);
                        Vector3 pos = p.position + p.normal * Random.Range(0.1f, 10.0f) + Vector3.down *3;
                        GameObject rock = InstanceProps(pos, Random.Range(0, 360), new Vector3(sca, sca, sca));
                        rock.transform.parent = allProps.transform;
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
