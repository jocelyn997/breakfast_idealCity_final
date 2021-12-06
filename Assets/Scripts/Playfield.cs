using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static Playfield instance;

    public int gridSizeX, gridSizeY, gridSizeZ;

    [Header("Blocks")]
    public GameObject[] blocklist;

    public GameObject[] ghostlist;

    [Header("Playfield Visuals")]
    public GameObject bottomPlane;

    public GameObject N, S, W, E;

    private int randomIndex;

    public Transform[,,] theGrid;

    private GameObject previewTetris;

    private GameObject nextTetris;
    private bool gameStarted = false;

    [Header("Preview Position")]
    public Vector3 previewTetrisPosition;

    //nev mesh

    public float Speed = 10.0f;

    public LayerMask SelectMask;
    public LayerMask PlaceMask;
    private RectTransform rect;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        theGrid = new Transform[gridSizeX, gridSizeY, gridSizeZ];
        CalculatePreview();
        //？这一行有问题？
        SpawnNewBlock();
    }

    //rounding function 滚动检测
    public Vector3 Round(Vector3 vec)
    {
        //滚动数值去整数，移动整数格
        return new Vector3(Mathf.RoundToInt(vec.x),
                             Mathf.RoundToInt(vec.y),
                             Mathf.RoundToInt(vec.z));
    }

    //检查是否在网格内部
    public bool CheckInsideGrid(Vector3 pos)

    //检车X,Y,Z是否大于0,最大值是否小于网格极大
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridSizeX &&
                 (int)pos.z >= 0 && (int)pos.z < gridSizeZ &&
                 (int)pos.y >= 0);
    }

    public void UpdatedGrid(TetrisBlock block)
    {
        //delete possible parent objects
        //parent object是四个盒子形成的形态，child是单个的盒子，更新网格成把整个的大格子换成小格子的坐标去判断。
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if (theGrid[x, y, z] != null)
                    {
                        if (theGrid[x, y, z].parent == block.transform)
                        {
                            theGrid[x, y, z] = null;
                        }
                    }
                }
            }
        }
        //fill in all child objects
        foreach (Transform child in block.transform)
        {
            Vector3 pos = Round(child.position);
            if (pos.y < gridSizeY)
            {
                theGrid[(int)pos.x, (int)pos.y, (int)pos.z] = child;
            }
        }
    }

    public Transform GetTransformOnGridPos(Vector3 pos)
    {
        if (pos.y > gridSizeY - 1)
        {
            return null;
        }
        else
        {
            return theGrid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }

    //  private int randomIndex1 = Random.Range(0, blocklist.Length);

    public void SpawnNewBlock()
    {
        Vector3 spawnPoint = new Vector3((int)(transform.position.x + (float)gridSizeX / 2),
                                             (int)transform.position.y + gridSizeY,
                                             (int)(transform.position.z + (float)gridSizeZ / 2));

        //SPAWN THE BLOCK /show new block
        GameObject newBlock = Instantiate(blocklist[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
        //GHOST
        GameObject newGhost = Instantiate(ghostlist[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
        newGhost.GetComponent<GhostBlock>().SetParent(newBlock);

        CalculatePreview();
        Previewer.instance.ShowPreview(randomIndex);

        //SET INPUTS
    }

    public void CalculatePreview()
    {
        randomIndex = Random.Range(0, blocklist.Length);
    }

    private void OnDrawGizmos()
    {
        if (bottomPlane != null)
        {
            //RESIZE BOTTOM PLANE
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeZ / 10);
            bottomPlane.transform.localScale = scaler;

            //REPOSITION / ? WHY NOT -
            bottomPlane.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                              transform.position.y,
                                                              transform.position.z + (float)gridSizeZ / 2);

            //RETILE MATERIAL  /  ? why = vector2
            bottomPlane.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeZ);
        }

        if (N != null)
        {
            //RESIZE BOTTOM PLANE / ? why Y not in the second position
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            N.transform.localScale = scaler;

            //REPOSITION / ? WHY NOT -
            N.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                  transform.position.y + (float)gridSizeY / 2,
                                                  transform.position.z + gridSizeZ);

            //RETILE MATERIAL  /  ? why = vector2
            N.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }

        if (S != null)
        {
            //RESIZE BOTTOM PLANE /
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            S.transform.localScale = scaler;

            //REPOSITION /
            S.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                 transform.position.y + (float)gridSizeY / 2,
                                                 transform.position.z);

            //RETILE MATERIAL  /
            //S.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }

        if (W != null)
        {
            //RESIZE BOTTOM PLANE /
            Vector3 scaler = new Vector3((float)gridSizeZ / 10, 1, (float)gridSizeY / 10);
            W.transform.localScale = scaler;

            //REPOSITION /
            W.transform.position = new Vector3(transform.position.x,
                                                 transform.position.y + (float)gridSizeY / 2,
                                                 transform.position.z + (float)gridSizeZ / 2);

            //RETILE MATERIAL  /
            W.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeZ, gridSizeY);
        }

        if (E != null)
        {
            //RESIZE BOTTOM PLANE / ? why gridsize Z
            Vector3 scaler = new Vector3((float)gridSizeZ / 10, 1, (float)gridSizeY / 10);
            E.transform.localScale = scaler;

            //REPOSITION /
            E.transform.position = new Vector3(transform.position.x + gridSizeX,
                                                  transform.position.y + (float)gridSizeY / 2,
                                                  transform.position.z + (float)gridSizeZ / 2);

            //RETILE MATERIAL  /
            //S.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }
    }

    /*   public static void UpdateAllNavMesh()
    //   {
    //       NavMeshSurface[] surfaces = FindObjectsOfType<NavMeshSurface>();
     //      foreach (NavMeshSurface surface in surfaces)
           {
               surface.BuildNavMesh();
           }*/
}