using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInjection : MonoBehaviour
{
    public TileList_SO ListOfAllTiles;

    private void OnEnable()
    {
        ListOfAllTiles.AddToRuntimeList(this.gameObject);

        this.GetComponent<BasicTile>().basicTileVariables = ScriptableObject.CreateInstance<BasicTileVariables_SO>();
        this.GetComponent<BasicTile>().basicTileVariables.name = "InstanceBasicTileVariables";

        this.GetComponent<AdvancedTile>().basicTileVariables = this.GetComponent<BasicTile>().basicTileVariables;
        this.GetComponent<AdvancedTile>().advancedTileVariables = ScriptableObject.CreateInstance<AdvancedTileVariables_SO>();
        this.GetComponent<AdvancedTile>().advancedTileVariables.name = "InstanceAdvancedTileVariables";
    }

    private void OnDisable()
    {
        ListOfAllTiles.RemoveFromRuntimeList(this.gameObject);
    }

    
}
