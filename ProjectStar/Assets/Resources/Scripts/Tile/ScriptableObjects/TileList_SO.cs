using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TilesList", menuName = "ScriptableLists/Type: Tiles")]
public class TileList_SO : ScriptableObject
{
    private List<AdvancedTile> listOfAllTiles;
    
    public void AddTile(AdvancedTile adTile)
    {
        listOfAllTiles.Add(adTile);
    }

    public void RemoveTile(AdvancedTile adTile)
    {
        listOfAllTiles.Remove(adTile);
    }

    public List<AdvancedTile> GetList()
    {
        return listOfAllTiles;
    }
}
