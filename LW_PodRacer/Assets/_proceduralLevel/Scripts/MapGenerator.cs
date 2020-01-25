using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct textureChunk
{
    public string name { get; set; }

    public Vector2 position { get; set; }

    public GameObject plane { get; set; }

    public int width { get; set; }

    public int height { get; set; }

    public Color[] pixels { get; set; }

    public float[,] heightMap { get; set; }

    public int resolutionMap { get; set; }

    public float scaleFactor { get; set; }

    public int chunkScale { get; set; }
}

public class MapGenerator : MonoBehaviour
{
    //VARIABLE
    //Taille en pixel de la texture
    [HideInInspector]
    public int resolutionMap = 100;

    [HideInInspector]
    public float scaleFactor = 0.01125f;

    //dimension du mesh en param unity
    public int chunkScale = 10;

    //Tout les chunks
    [HideInInspector]
    public textureChunk[,] textureChunks;

    //si on veut rajouter un chunk de sécurité à chauqe côté
    public int SecureChunk = 0;

    //NOISE PUBLIC
    public int noiseOctave = 4;
    public float noiseScale = 0.5f;
    public float noisePersistance = 1.8f;
    public float noiseLacunarity = 0.8f;


    //nombre de chunk en x
    private int xChunks;
    //nombre de chunk en z
    private int zChunks;

    //le x du chunk minimum et maximum
    private int xChunksMin;
    private int xChunksMax;

    private int zStart = 0;

    //on récupere le tracé procédural généré
    private TrackGenerator trackGen;

    private GameObject allPlane =null;

    //FUNCTIONS
    public void Clean()
    {
        if (allPlane != null) Destroy(allPlane);
    }

    //INITIALISE LES VARIABLES
    public bool IniVariable()
    {
        trackGen = transform.GetComponent<TrackGenerator>();
        if (trackGen != null)
        {
            int xMin = -(int)Mathf.Ceil((Mathf.Abs(trackGen.xMin)) / (chunkScale * 10f)) - SecureChunk;
            int xMax = (int)Mathf.Ceil(Mathf.Abs(trackGen.xMax) / (chunkScale * 10f)) + SecureChunk;
            int z = (int)Mathf.Ceil(Mathf.Abs(trackGen.zMax) / (chunkScale * 10f)) + SecureChunk;

            xChunks = Mathf.Abs(xMin) + xMax;
            zChunks = z;
            xChunksMin = xMin;

            textureChunks = new textureChunk[xChunks, z];

            allPlane = new GameObject();
            allPlane.name = "All_RefPlane";

            return true;
        }
        else
        {
            Debug.Log("WARNING:TRACKGENERATOR N'EST PAS ATTACHER");
            return false;
        }
    }

    //INITIALISE UN TABLEAU DE COLOR
    public Color[] InitializeColors()
    {
        Color[] tmpColors = new Color[resolutionMap * resolutionMap];
        for(int i = 0; i< resolutionMap; i++)
            for (int j = 0; j < resolutionMap; j++)
            {
                tmpColors[i * resolutionMap  +  j] = Color.black;
            }
        return tmpColors;
    }

    public float[,] InitalizeheightMap()
    {
        float[,] tmpheightMap = new float[resolutionMap, resolutionMap];

        for (int i = 0; i < resolutionMap; i++)
            for (int j = 0; j < resolutionMap; j++)
            {
                tmpheightMap[i, j] = 1.0f;
            }

        return tmpheightMap;
    }

    //PREPARE LA TEXTURE 
    public Texture2D TextureFromPlane(Color[] colors)
    {
        Texture2D texture = new Texture2D(resolutionMap, resolutionMap);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colors);
        texture.Apply();
        
        return texture;
    }

    //Test le point par rapport au chunk et redefinit le point et le chunk en conséquence pour ensuite le mettre à 0
    public bool SetPointHeighMap( (int,int) chunk, (int,int) newPoint )
    {
        if (newPoint.Item1 < 0)
        {
            if (chunk.Item1 == 0) return false;

            chunk.Item1 = chunk.Item1 - 1;
            newPoint.Item1 = textureChunks[chunk.Item1, chunk.Item2].heightMap.GetLength(1) + newPoint.Item1;
        }
        else if (newPoint.Item1 >= textureChunks[chunk.Item1, chunk.Item2].heightMap.GetLength(1))
        {
            if (chunk.Item1 == textureChunks.GetLength(0) - 1) return false;

            chunk.Item1 = chunk.Item1 + 1;
            newPoint.Item1 = newPoint.Item1 - textureChunks[chunk.Item1, chunk.Item2].heightMap.GetLength(1);
        }

        if (newPoint.Item2 < 0)
        {
            if (chunk.Item2 == 0) return false;

            chunk.Item2 = chunk.Item2 - 1;
            newPoint.Item2 = textureChunks[chunk.Item1, chunk.Item2].heightMap.GetLength(0) + newPoint.Item2;
        }
        else if (newPoint.Item2 >= textureChunks[chunk.Item1, chunk.Item2].heightMap.GetLength(0))
        {
            if (chunk.Item2 == textureChunks.GetLength(1) - 1) return false;

            chunk.Item2 = chunk.Item2 + 1;
            newPoint.Item2 = newPoint.Item2 -textureChunks[chunk.Item1, chunk.Item2].heightMap.GetLength(0) ;
        }

        textureChunks[chunk.Item1, chunk.Item2].heightMap[newPoint.Item1, newPoint.Item2] = 0.0f;

        return true;
    }

    //FONCTIOn Qui va vérifier les bords des chuncks et vérifier si le bord est sensé être uen falaise ou non
    void CleanSideHeighMap()
    {
        for (int x = 0; x < textureChunks.GetLength(0); x++)
            for (int z = 0; z < textureChunks.GetLength(1); z++)
            {
                for (int i = 0; i < resolutionMap; i++)
                    for (int j = 0; j < resolutionMap; j++)
                    {
                        if (textureChunks[x, z].heightMap[i, j] != 0.0f)
                        {
                            //SIDE
                            if (i == 0 && x != 0)
                            {
                                if (textureChunks[x - 1, z].heightMap[resolutionMap - 1, j] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }

                            if (j == 0 && z != 0)
                            {
                                if (textureChunks[x, z - 1].heightMap[i, resolutionMap - 1] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }

                            if (i == resolutionMap - 1 && x != textureChunks.GetLength(0) - 1)
                            {
                                if (textureChunks[x + 1, z].heightMap[0, j] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }

                            if (j == resolutionMap - 1 && z != textureChunks.GetLength(1) - 1)
                            {
                                if (textureChunks[x, z + 1].heightMap[i, 0] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }

                            //CORNER
                            //[0,0]
                            if(i == 0 && x != 0 && j == 0 && z != 0)
                            {
                                if (textureChunks[x-1, z - 1].heightMap[resolutionMap - 1, resolutionMap - 1] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }

                            //[0,1]
                            if (i == 0 && x != 0 && j == resolutionMap - 1 && z != textureChunks.GetLength(1) - 1)
                            {
                                if (textureChunks[x - 1, z + 1].heightMap[resolutionMap - 1, 0] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }

                            //[1,1]
                            if (i == resolutionMap - 1 && x != textureChunks.GetLength(0) - 1 && j == resolutionMap - 1 && z != textureChunks.GetLength(1) - 1)
                            {
                                if (textureChunks[x + 1, z +1].heightMap[0, 0] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }

                            //[1,0]
                            if (i == resolutionMap - 1 && x != textureChunks.GetLength(0) - 1 && j == 0 && z != 0)
                            {
                                if (textureChunks[x + 1, z - 1].heightMap[0, resolutionMap - 1] == 0.0f)
                                    textureChunks[x, z].heightMap[i, j] = 0.0f;
                            }
                        }
                    }
            }
    }

    //CREATION D'UN PLANE
    public textureChunk CreateNewChunk(int x, int z)
    {
        textureChunk tp = new textureChunk();

        tp.position = new Vector2(((xChunksMin + x) * 10f * chunkScale) + 5 * chunkScale, z * 10f * chunkScale + 5 * chunkScale);

        tp.width = 10 * chunkScale;
        tp.height = 10 * chunkScale;

        tp.plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        tp.plane.name = "refPlane[" + x + "," + z + "]";
        if(allPlane != null)tp.plane.transform.parent = allPlane.transform;

        tp.plane.transform.position = new Vector3(tp.position.x, -5, tp.position.y);
        tp.plane.transform.Rotate(0, 180, 0);
        tp.plane.transform.localScale = new Vector3(chunkScale, 1, chunkScale);
        
        tp.plane.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Diffuse"));
        tp.plane.SetActive(false);
        tp.pixels = InitializeColors();
        tp.heightMap = InitalizeheightMap();

        tp.resolutionMap = resolutionMap;
        tp.scaleFactor = scaleFactor;
        tp.chunkScale = chunkScale;

        //tp.plane.GetComponent<Renderer>().sharedMaterial.mainTexture = TextureFromPlane(tp.pixels);
        return tp;
    }

    //CREER TOUT LES PLANES
    public textureChunk CreateChunks()
    {
        for (int z = 0; z < zChunks; z++)
        {
            for (int x = 0; x < xChunks; x++)
            {
                textureChunks[x, z] = CreateNewChunk(x,z);
            }
        }
        return new textureChunk();
    }

    public void CreateAllHeightMap()
    {
        //pour chaque point généré par le Track Generator, on recherche dans quel chunk il est et à quel pixel du chunk il correspond
        //Respectivement pour les valeurs x et z de la position du point. On le met par rapport au repere du premier chunk
        //que l'on divise par la taille du chunk. Ainsi l'entier devant la virgule represente l'index du chunk en x et z.
        //enfin le nombre apres la virgule est à remettre dans le repere du chunk par rapport à la résolution de map voulu ce qui donne
        //ses coordonnées de pixels
        //On a plus qu'a transformers ce pixel en blancs
        foreach (Track_part t in trackGen.all_tracks)
        {
            foreach(Point p in t.points)
            {
                //X
                float x = Mathf.Abs((p.position.x - (xChunksMin * 10f * chunkScale)) / (10f * chunkScale));
                int cx = Mathf.FloorToInt(x);
                float fx = x - cx;
                int realPx = (int)Mathf.Abs(((fx * resolutionMap) / (10f * chunkScale)) * 100);
                
                //Z
                float z = Mathf.Abs(p.position.z / (10f * chunkScale));            
                int cz = Mathf.FloorToInt(z);
                float fz = z - cz;
                int realPz = (int)Mathf.Abs(((fz * resolutionMap) / (10f * chunkScale))*100);
                
                //PIXEL
                textureChunks[cx, cz].pixels[realPz*resolutionMap + realPx] = Color.white;
                
                //heightMap //Pixel complet
                SetPointHeighMap((cx, cz), (realPx, realPz));
                SetPointHeighMap((cx, cz), (realPx+1, realPz));
                SetPointHeighMap((cx, cz), (realPx, realPz+1));
                SetPointHeighMap((cx, cz), (realPx+1, realPz+1));

                //TEST MASK (A améliorer)
                SetPointHeighMap((cx, cz), (realPx, realPz - 1));
                SetPointHeighMap((cx, cz), (realPx+1, realPz-1));
                
                SetPointHeighMap((cx, cz), (realPx -1, realPz));
                SetPointHeighMap((cx, cz), (realPx - 1, realPz+1));
                SetPointHeighMap((cx, cz), (realPx - 1, realPz - 1));

                SetPointHeighMap((cx, cz), (realPx + 2, realPz));
                SetPointHeighMap((cx, cz), (realPx + 2, realPz + 1));
                SetPointHeighMap((cx, cz), (realPx + 2, realPz - 1));

                SetPointHeighMap((cx, cz), (realPx - 2, realPz));
                SetPointHeighMap((cx, cz), (realPx - 2, realPz + 1));
                SetPointHeighMap((cx, cz), (realPx - 2, realPz - 1));
            }
        }

        //Permet de cleaner les erreurs des pixels se trouvant sur le bord des chunks et devant être une falaise
        CleanSideHeighMap();

        //NOISE
        for (int x = 0; x < textureChunks.GetLength(0); x++)
            for (int z = 0; z < textureChunks.GetLength(1); z++)
            {
                int width = textureChunks[x, z].heightMap.GetLength(0);
                int height = textureChunks[x, z].heightMap.GetLength(1);
                float[,] noise = Noise.GenerateNoiseMap(resolutionMap, resolutionMap, trackGen.seed, noiseScale, noiseOctave, noisePersistance, noiseLacunarity, new Vector2(x * (width-1), z * (height-1)));
                for (int i = 0; i < resolutionMap; i++)
                    for (int j = 0; j < resolutionMap; j++)
                    {
                        textureChunks[x, z].heightMap[i, j] += 0.1f* noise[i, j];
                    }
            }
    }

    //AFFICHE LA TEXTURE D'heightMap sur les planes
    public void  DisplayHeightMap()
    {
        for (int z = 0; z < zChunks; z++)
        {
            for (int x = 0; x < xChunks; x++)
            {
                textureChunk tp = textureChunks[x, z];
                tp.plane.GetComponent<Renderer>().sharedMaterial.mainTexture = TextureFromPlane(tp.pixels);
            }
        }
    }

    //FONCTION MAIN
    public void Generate()
    {
        if(IniVariable())
        {
            CreateChunks();
            CreateAllHeightMap();
            //DisplayheightMap();
        }

    }
}
