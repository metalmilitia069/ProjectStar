using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInjection : MonoBehaviour
{
    [Header("INSERT TILES LIST HERE:")]
    public TileList_SO allTilesList_SO;

    private void OnEnable()
    {        
        allTilesList_SO.AddTile(this.GetComponent<AdvancedTile>());

        GetComponent<BasicTile>().basicTileVariables = ScriptableObject.CreateInstance<BasicTile_SO>();
        GetComponent<BasicTile>().basicTileVariables.name = "InstanceBasicVar";

        GetComponent<AdvancedTile>().basicTileVariables = GetComponent<BasicTile>().basicTileVariables;
        GetComponent<AdvancedTile>().advancedTileVariables = ScriptableObject.CreateInstance<AdvancedTile_SO>();
        GetComponent<AdvancedTile>().advancedTileVariables.name = "InstanceAdvancedVar";


    }

    private void OnDisable()
    {
        //allTilesList_SO.listOfAllTiles.Remove(this.GetComponent<AdvancedTile>());
        allTilesList_SO.RemoveTile(this.GetComponent<AdvancedTile>());
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
