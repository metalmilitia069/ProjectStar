using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTile : MonoBehaviour
{
    [Header("BASIC TILE VARIABLES - INSTANCE : ")]
    public BasicTile_SO basicTileVariables;
    [Header("ADVANCED TILE VARIABLES - INSTANCE : ")]
    public AdvancedTile_SO advancedTileVariables;

    // Start is called before the first frame update
    void Start()
    {
        //ScanTiles
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTileState();
    }

    public void UpdateTileState()
    {
        Debug.DrawRay(transform.position, Vector3.up);

        if (basicTileVariables.isCurrent)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (basicTileVariables.isTarget)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (basicTileVariables.isSelectable)
        {
            GetComponent<Renderer>().material.color = Color.blue;

            if (basicTileVariables.isLatter)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (basicTileVariables.isCover)
            {
                GetComponent<Renderer>().material.color = Color.green;
            }

            //if (isAttacable)
            //{
            //    GetComponent<Renderer>().material.color = Color.cyan;
            //}
        }
        else if (basicTileVariables.isAttakable)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
