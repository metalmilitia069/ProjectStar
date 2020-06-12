using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileModifierVar", menuName = "Modifiers/Tile Modifier")]
public class TileModifier_SO : ScriptableObject
{    
    public bool isLatter;    
    public bool isCover;    
    public bool isHalfCover = true;    
    public bool isFullCover = false;    
    public float halfCoverPenalty = 0.20f;    
    public float fullCoverPenalty = 0.90f;


    
    public float latterHeight = 5;
    //[SerializeField]
    //private Vector3 _latterHeightVector;

    
    public bool isForwardDirection = false;    
    public bool isBackwardDirection = false;    
    public bool isRightDirection = false;    
    public bool isLeftDirection = false;    
    public Vector3 position;    
    public bool isRayUp = false;
    public Vector3 rayDirection;
    public bool isTypeChanged = false;
}
