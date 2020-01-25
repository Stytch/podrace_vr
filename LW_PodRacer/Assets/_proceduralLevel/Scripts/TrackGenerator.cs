using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#region struct

public enum Road_type { classic, tunnel }

//la Struct contiendra les données de la partie du circuit
public struct Track_part
{
    public string name { get; set; }
    //ANCHOR
    public Vector3 a0 { get; set; }
    public Vector3 a1 { get; set; }
    //CONTROLLER
    public Vector3 c0 { get; set; }
    public Vector3 c1 { get; set; }

    //List
    public List<Point> points { get; set; }
    public LineRenderer lineRenderer { get; set; }

    //settings 
    public Road_type roadType { get; set; }
}

//Contient simplement la position et la normal 
public struct Point
{
    public Vector3 position { get; set; }
    public Vector3 normal { get; set; }
    public Vector3 tangent { get; set; }
    public Vector3 cross { get; set; } //produit vectoriel de normal et tangent
}
#endregion

#region Class
public class TrackGenerator : MonoBehaviour
{
    #region variable
    //PUBLIC
    public bool useSeed = true ;
    public int seed = 42;

    //Largeur de base du circuit même si varira
    public int BASE_WIDTH = 10;
    //Nombre de partie que contiendra le circuit
    public int NB_TRACK_PART;
    //Nombre de segment que contient chaque courve de Bezier
    public int SEGMENT_COUNT = 50;

    //toutes les parties du circuit seront inscrit dans l'ordre
    [HideInInspector]
    public List<Track_part> all_tracks = new List<Track_part>();
    [HideInInspector]
    public List<Track_part> buffer_tracks = new List<Track_part>();

    //
    [HideInInspector]
    public float zMax = 0;
    [HideInInspector]
    public float xMax = 0;
    [HideInInspector]
    public float xMin = 0;

    //
    private int INDEX_BUFFER = 6;

    private int totalTracks = 0;
    private bool resetTrack = false;



    //Parent
    private GameObject goTracks;

    //MEMO
    /*
     * Les courbes de Beziers sont faite sur seulement 2 axes
     * La direction générale du circuit sera basé sur l'axe Z
     * tandis que la variation des virages etc sera basé sur l'axe x
    */
    #endregion

    #region Icon Point
#if UNITY_EDITOR
    private void DrawIcon(GameObject go, int idx)
    {
        var largeIcons = GetTextures("sv_label_", string.Empty, 0, 8);
        var icon = largeIcons[idx];
        var egu = typeof(EditorGUIUtility);
        var flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        var args = new object[] { go, icon.image };
        var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[] { typeof(UnityEngine.Object), typeof(Texture2D) }, null);
        setIcon.Invoke(null, args);
    }

    private GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
    {
        GUIContent[] array = new GUIContent[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
        }
        return array;
    }
#endif
    #endregion

    #region Bezier
    //BEZIER FUNCTIONS
    private Vector3 GetNormal(Vector3 tangent)
    {
        Vector3 unit = new Vector3(0, 1, 0);

        return Vector3.Cross(tangent, unit);
    }

    //LINEAR
    private Point CalculateLinearInterpolation(float t, Vector3 p0, Vector3 p1)
    {
        Point p = new Point();
        p.position = (1 - t) * p0 + t * p1;
        p.tangent = (p1 - p0).normalized;
        p.normal = GetNormal(p.tangent);
        p.cross = Vector3.Cross(p.normal, p.tangent);
        //P = P0 + t(P1 – P0)
        //(1-t) P0 + tP1 
        //avec t, 0<=t<=1
        return p;
    }

    //QUADRATIC BEZIER
    private Vector3 CalculateQuadraticBezier(float t, Vector3 p0, Vector3 p1,Vector3 p2)
    {
        Vector3 interpoP0P1 = CalculateLinearInterpolation(t,p0,p1).position;
        Vector3 interpoP1P2 = CalculateLinearInterpolation(t, p1, p2).position;
        return CalculateLinearInterpolation(t, interpoP0P1, interpoP1P2).position;
    }

    //CUBIC BEZIER
    private Point CalculateCubicBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Point p = new Point();

        Vector3 interpoP0P1P2 = CalculateQuadraticBezier(t, p0, p1, p2);
        Vector3 interpoP1P2P3 = CalculateQuadraticBezier(t, p1, p2, p3);

        p = CalculateLinearInterpolation(t, interpoP0P1P2, interpoP1P2P3);

        return p;
    }

    #endregion

    #region Perlin
    //Base sur le lien si dessous avec quelques correctifs pour éviter les outOfRange etc
    //http://sdz.tdct.org/sdz/bruit-de-perlin.html
    float Get2DPerlinNoiseValue(float x, float y, float res)
    {
        float tempX, tempY;
        int x0, y0, ii, jj, gi0, gi1, gi2, gi3;
        float unit = 1.0f / Mathf.Sqrt(2);
        float tmp, s, t, u, v, Cx, Cy, Li1, Li2;
        float[,] gradient2 = new float[,]{{unit,unit},{-unit,unit},{unit,-unit},{-unit,-unit},{1,0},{-1,0},{0,1},{0,-1}};

         int[] perm =
           {151,160,137,91,90,15,131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,
            142,8,99,37,240,21,10,23,190,6,148,247,120,234,75,0,26,197,62,94,252,219,
            203,117,35,11,32,57,177,33,88,237,149,56,87,174,20,125,136,171,168,68,175,
            74,165,71,134,139,48,27,166,77,146,158,231,83,111,229,122,60,211,133,230,220,
            105,92,41,55,46,245,40,244,102,143,54,65,25,63,161,1,216,80,73,209,76,132,
            187,208,89,18,169,200,196,135,130,116,188,159,86,164,100,109,198,173,186,3,
            64,52,217,226,250,124,123,5,202,38,147,118,126,255,82,85,212,207,206,59,227,
            47,16,58,17,182,189,28,42,223,183,170,213,119,248,152,2,44,154,163,70,221,
            153,101,155,167,43,172,9,129,22,39,253,19,98,108,110,79,113,224,232,178,185,
            112,104,218,246,97,228,251,34,242,193,238,210,144,12,191,179,162,241,81,51,145,
            235,249,14,239,107,49,192,214,31,181,199,106,157,184,84,204,176,115,121,50,45,
            127,4,150,254,138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,
            156,180,151};
        
        //Adapter pour la résolution
        x /= res;
        y /= res;

        //On récupère les positions de la grille associée à (x,y)
        x0 = (int) (x);
        y0 = (int) (y);

        //Masquage
        ii = x0 & 255;
        jj = y0 & 255;

        //Pour récupérer les vecteurs
        gi0 = perm[(ii + perm[jj]) & 255] % 8;
        gi1 = perm[(ii + 1 + perm[jj]) & 255] % 8;
        gi2 = perm[(ii + perm[(jj + 1) & 255]) & 255] % 8;
        gi3 = perm[(ii + 1 + perm[(jj + 1) & 255]) & 255] % 8;

        //on récupère les vecteurs et on pondère
        tempX = x-x0;
        tempY = y-y0;
        s = gradient2[gi0,0]* tempX + gradient2[gi0,1]* tempY;

        tempX = x-(x0+1);
        tempY = y-y0;
        t = gradient2[gi1,0]* tempX + gradient2[gi1,1]* tempY;

        tempX = x-x0;
        tempY = y-(y0+1);
        u = gradient2[gi2,0]* tempX + gradient2[gi2,1]* tempY;

        tempX = x-(x0+1);
        tempY = y-(y0+1);
        v = gradient2[gi3,0]* tempX + gradient2[gi3,1]* tempY;


        //Lissage
        tmp = x-x0;
        Cx = 3 * tmp * tmp - 2 * tmp * tmp * tmp;

        Li1 = s + Cx* (t-s);
        Li2 = u + Cx* (v-u);

        tmp = y - y0;
        Cy = 3 * tmp * tmp - 2 * tmp * tmp * tmp;

        return Li1 + Cy* (Li2-Li1);
    }
    #endregion

    #region Generate Part Track
    //test Z && X max value
    void CheckZandXmax(Vector3 position)
    {
        if (position.x > xMax) xMax = position.x;

        if (position.x < xMin) xMin = position.x;

        if (Mathf.Abs(position.z) > zMax) zMax = Mathf.Abs(position.z);
    }

    //Create new LineRenderer
    private LineRenderer CreateLineRenderer(Vector3 position)
    {
        GameObject go = new GameObject("TrackPart_" + totalTracks);
        totalTracks++;
        go.transform.parent = goTracks.transform;
        go.transform.position = position;
        return go.AddComponent<LineRenderer>();
    }

    //Affiche des icones sur les points (Utile pou Debug)
    void DrawControllerPoint(Track_part tp)
    {
        GameObject a0 = new GameObject();
        a0.transform.position = tp.a0;
        a0.name = tp.name + "_ao";
        a0.transform.parent = tp.lineRenderer.transform;

        GameObject a1 = new GameObject();
        a1.transform.position = tp.a1;
        a1.name = tp.name + "_a1";
        a1.transform.parent = tp.lineRenderer.transform;

        GameObject c0 = new GameObject();
        c0.transform.position = tp.c0;
        c0.name = tp.name + "_co";
        c0.transform.parent = tp.lineRenderer.transform;

        GameObject c1 = new GameObject();
        c1.transform.position = tp.c1;
        c1.name = tp.name + "_c1";
        c1.transform.parent = tp.lineRenderer.transform;
#if UNITY_EDITOR
        DrawIcon(a0, 4);
        DrawIcon(a1, 4);
        DrawIcon(c0, 2);
        DrawIcon(c1, 2);
#endif
    }

    //Initialisation 
    private Track_part IniNewTrackPart()
    {
        
        Track_part newTrack = new Track_part();

        /*On initialise le point de depart de ce nouveau track_part
         *Si all_track est vide alors c'est l'initialisation du premier tableau
         *Si buffer_tracks est utilisé et donc pas vide alors c'ets lui qu'on remplit en priorité
         * Sinon on remplit all_tracks
         */
        if (all_tracks.Count == 0)
            newTrack.a0 = new Vector3(0, 0, 0);
        else if (buffer_tracks.Count > 0)
            newTrack.a0 = buffer_tracks[buffer_tracks.Count - 1].a1;
        else
            newTrack.a0 = all_tracks[all_tracks.Count - 1].a1;

        //Creer un GameObject lineRenderer (essentiellement pour le debug)
        newTrack.lineRenderer = CreateLineRenderer(newTrack.a0);

        //Creation d'une liste de point
        newTrack.points = new List<Point>();

        newTrack.name = newTrack.lineRenderer.gameObject.name;

        //Création du 2 eme point
        newTrack.a1 = newTrack.a0 + new Vector3(UnityEngine.Random.Range(-25, 25), 0, UnityEngine.Random.Range(25, 40));

        //Choix du type
        int sel = UnityEngine.Random.Range(0, 11);
        int tol = 8;
        if (buffer_tracks.Count > 0 && buffer_tracks[buffer_tracks.Count - 1].roadType == Road_type.tunnel) tol = 2;

        if (sel > tol)
        {
            newTrack.roadType = Road_type.tunnel;
            
        }
        else newTrack.roadType = Road_type.classic;


        return newTrack;
    }

    //Calcul et Set les point de Bezier du track part
    private Track_part GenerateBezier(Track_part tp)
    {
        tp.lineRenderer.sortingLayerID = 0;
        tp.lineRenderer.SetPosition(0, tp.a0);

        for (int i = 1; i <= SEGMENT_COUNT; i++)
        {
            float t = i / (float)SEGMENT_COUNT;
            //On a besoin seulement de l'in terpolation linéaire
            Point point = CalculateCubicBezier(t, tp.a0, tp.c0, tp.c1, tp.a1);
            Vector3 position = point.position;

            tp.points.Add(point);
            tp.lineRenderer.positionCount = i + 1;
            tp.lineRenderer.SetPosition(i, position);
            CheckZandXmax(position);
        }
        return tp;
    }

    //Algo de cat Mull Rom qui symetrise les controllesr entre chaque point d'encrage
    List<Track_part> CatMullRomForOneTrack(List<Track_part> tp, int i)
    {
        Track_part currentTrack = tp[i];

        //Calcul de C1
        if (i != tp.Count - 1)
        {
            Track_part nextTrack = tp[i + 1];

            Vector3 posC1 = (nextTrack.a1 - currentTrack.a0) / 4;
            currentTrack.c1 = currentTrack.a1 - posC1;

            nextTrack.c0 = nextTrack.a0 + posC1;

            tp[i + 1] = nextTrack;


            //DEBUG: Permet de faire un transition plus propre à linitialisation de la premiere valeur de buffer_tracks
            if (tp == buffer_tracks) currentTrack.c0 = 2*currentTrack.a0 - all_tracks[all_tracks.Count - 1].c1; 
        }
        else
        {
            currentTrack.c1 = currentTrack.a1 - (currentTrack.c0 - currentTrack.a0)/4;
        }

        //Realisation du Bezier
        currentTrack = GenerateBezier(currentTrack);
        DrawControllerPoint(currentTrack);

        tp[i] = currentTrack;
        return tp;
    }

    //Algo de generation 
    void CatMullRomAllTracks()
    {
        //Step 1: Tracer tout les points et généré la liste de track_part
        for (int i = 0; i < NB_TRACK_PART; i++) all_tracks.Add(IniNewTrackPart());

        //Step 2 parcourir la liste de track part en applicant l'algo
        for (int i = 0; i < NB_TRACK_PART; i++)
        {
            all_tracks = CatMullRomForOneTrack(all_tracks, i);
        }
    }

    //A appeler dans un update
    //Va generer un nouveau tracé régulierement
    public void InfiniteModeCatMullRom()
    {
       
        //Ini variable
        int CurrentTrack = 0;
        //Ini buffer_tracks quand la list est vide
        if (buffer_tracks.Count < 3)
            for (int i = 0; i < UnityEngine.Random.Range(13, 20); i++) buffer_tracks.Add(IniNewTrackPart());

        //Récupération de la position  du joueur
        /*
        for (int i = 0; i < all_tracks.Count; i++)
        {
            if (player.position.z > all_tracks[i].a0.z && player.position.z < all_tracks[i].a1.z)
            {
                Debug.Log("Current Track:" + i);
                CurrentTrack = i;
            }
        }*/

        // 
        if (CurrentTrack > INDEX_BUFFER)
        {
            //Detruit les anciens tarck pour ne pas surcharger la mémoire et affecter les performances
            Destroy(all_tracks[0].lineRenderer.gameObject);
            all_tracks.RemoveAt(0);
            //GenerateWall(all_tracks[0]);

            //On met à jour le buffer de circuit
            buffer_tracks = CatMullRomForOneTrack(buffer_tracks, 0);

            //On ajoute le premier circuit du buffer pour generer les falaises
            all_tracks.Add(buffer_tracks[0]);
            buffer_tracks.RemoveAt(0);

            //GenerateRoad(all_tracks[all_tracks.Count - 1]);
            
        }
        //Met à jours les dimensions du terrain
        //UpdateGround();

    }
    //Pour générer un unique circuit
    public void CreateFirstTracks()
    {
       
        //INIT VARIABLE
        if(BASE_WIDTH == 0) BASE_WIDTH = UnityEngine.Random.Range(7, 11);
        if(NB_TRACK_PART == 0) NB_TRACK_PART = UnityEngine.Random.Range(17, 25);

        resetTrack = false;
        all_tracks = new List<Track_part>();
        buffer_tracks = new List<Track_part>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        goTracks = new GameObject("Tracks");

        zMax = 0;
        xMax = 0;

        //GenerateAllTrack();
        CatMullRomAllTracks();

    }

    public void GenerateTrack(bool HideTracks = false)
    {
        if (useSeed) UnityEngine.Random.InitState(seed);
        CreateFirstTracks();

        if (HideTracks) goTracks.SetActive(false);
    }

    public void DebugLog()
    {
        Debug.Log("Z max: " + zMax);
        Debug.Log("X max: " + xMax);
        Debug.Log("X min: " + xMin);
    }
    #endregion

    #region Event Start
    public void Clean()
    {
        if (goTracks != null)
        {
#if UNITY_EDITOR
            DestroyImmediate(goTracks);
#else
            Destroy(goTracks);
#endif
        }

    }
    // Start is called before the first frame update

    void Start()
    {
        //GenerateTrack();
    }

    // Update is called once per frame
    void Update()
    {

    }
#endregion

}
#endregion