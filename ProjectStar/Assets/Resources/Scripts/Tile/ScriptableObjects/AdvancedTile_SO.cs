using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdvancedTile", menuName = "ScriptableVariables/Type: AdvancedTile")]
public class AdvancedTile_SO : ScriptableObject
{
    [TextArea(1,10)]
    public string description = "Variables to Handle the Methods for the Advanced Tile Class.";

    public float latterHeight = 5;    
    public Vector3 latterHeightVector;    
    public bool isForwardDirection = false;    
    public bool isBackwardDirection = false;    
    public bool isRightDirection = false;    
    public bool isLeftDirection = false;    
    public Vector3 direction;
}
