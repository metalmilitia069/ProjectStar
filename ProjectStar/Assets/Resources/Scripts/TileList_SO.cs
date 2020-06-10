using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllTilesList", menuName = "Tiles/TileList")]
public class TileList_SO : ScriptableObject
{
    public List<BasicTile_SO> ListOfAllTiles;
    //public List<GameObject> ListOfAllTiles;
    //public List<TileMozo> ListOfAllTiles;

    public void AddToAllTilesList(BasicTile_SO tile)//(GameObject tile)
    {
        ListOfAllTiles.Add(tile);
    }

    private void OnDisable()
    {
        ListOfAllTiles.Clear();
    }
}
