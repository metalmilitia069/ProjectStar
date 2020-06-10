using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicTile : MonoBehaviour
{
    [Header("BASIC TILE VARIABLES")]
    public BasicTile_SO basicTileVariables;

    public TileList_SO tileList_SO;

    //public TileMozo TileMozo;
    // Start is called before the first frame update
    void Start()
    {
        //tileList_SO.AddToAllTilesList(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            tileList_SO.AddToAllTilesList(basicTileVariables);
            //TileMozo.AddMozo();
        }
    }
}

//[System.Serializable]
//public class TileMozo
//{
//    public TileList_SO tileList_SO;
//    public void AddMozo()
//    {
//        //tileList_SO.AddToAllTilesList(this);
//    }
//}
