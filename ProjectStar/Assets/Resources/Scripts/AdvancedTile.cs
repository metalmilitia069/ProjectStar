using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTile : MonoBehaviour
{
    [Header("BASIC TILE VARIABLES")]
    public BasicTileVariables_SO basicTileVariables;
    [Header("ADVANCED TILE VARIABLES")]
    public AdvancedTileVariables_SO advancedTileVariables;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<BasicTile>().ScanTiles();
    }

    // Update is called once per frame
    void Update()
    {
        SetTileStance();
    }

    public void SetTileStance()
    {
        if (this.basicTileVariables.isCurrent)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (this.basicTileVariables.isTarget)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (this.basicTileVariables.isSelectable)
        {
            GetComponent<Renderer>().material.color = Color.blue;

            if (this.basicTileVariables.isLatter)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (this.basicTileVariables.isCover)
            {
                GetComponent<Renderer>().material.color = Color.green;
            }

            //if (isAttacable)
            //{
            //    GetComponent<Renderer>().material.color = Color.cyan;
            //}
        }
        else if (this.basicTileVariables.isAttakable)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
