using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdvancedTileVariables", menuName = "Tiles/AdvancedVariables")]
public class AdvancedTileVariables_SO : ScriptableObject
{    
    public float latterHeight = 5;    
    public Vector3 latterHeightVector;
    
    public bool isForwardDirection = false;    
    public bool isBackwardDirection = false;    
    public bool isRightDirection = false;    
    public bool isLeftDirection = false; 
    
    public Vector3 direction;
}
