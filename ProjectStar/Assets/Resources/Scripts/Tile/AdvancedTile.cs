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
        //Debug.DrawRay(transform.position, Vector3.up);

        if (basicTileVariables.isCurrent && !basicTileVariables.isInOverwatch)
        {            
            GetComponent<MeshRenderer>().enabled = true;            
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (basicTileVariables.isTarget && !basicTileVariables.isInOverwatch)
        {            
            GetComponent<MeshRenderer>().enabled = true;            
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (basicTileVariables.isSelectable && !basicTileVariables.isInOverwatch)
        {            
            GetComponent<MeshRenderer>().enabled = true;            
            GetComponent<Renderer>().material.color = Color.blue;

            if (basicTileVariables.isLatter && !basicTileVariables.isInOverwatch)
            {                
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (basicTileVariables.isCover && !basicTileVariables.isInOverwatch)
            {                
                GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (basicTileVariables.isAttakable && !basicTileVariables.isInOverwatch)
        {            
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Renderer>().material.color = Color.cyan;
        }        
        else if (basicTileVariables.isInOverwatch)
        {            
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;            
            GetComponent<Renderer>().material.color = new Color(1,1,1,0);            
        }
    }
}
